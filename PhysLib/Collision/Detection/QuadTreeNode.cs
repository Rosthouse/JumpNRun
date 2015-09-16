using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SimplePhysicsAndCollision.Collision.Detection
{
    /// <summary>
    /// A node in a QuadTree
    /// </summary>
    /// <typeparam name="T">The type of the QuadTree's items' parents</typeparam>
    class QuadTreeNode<T>
    {
        #region Delegates

        /// <summary>
        /// World resize delegate
        /// </summary>
        /// <param name="newSize">The new world size</param>
        public delegate void ResizeDelegate(FRect newSize);

        #endregion

        #region Properties

        /// <summary>
        /// The rectangle of this node
        /// </summary>
        protected FRect rect;

        /// <summary>
        /// Gets the rectangle of this node
        /// </summary>
        public FRect Rect
        {
            get { return rect; }
            protected set { rect = value; }
        }

        /// <summary>
        /// The maximum number of items in this node before partitioning
        /// </summary>
        protected int MaxItems;

        /// <summary>
        /// Whether or not this node has been partitioned
        /// </summary>
        protected bool IsPartitioned;

        /// <summary>
        /// The parent node
        /// </summary>
        protected QuadTreeNode<T> ParentNode;

        /// <summary>
        /// The top left node
        /// </summary>
        protected QuadTreeNode<T> TopLeftNode;

        /// <summary>
        /// The top right node
        /// </summary>
        protected QuadTreeNode<T> TopRightNode;

        /// <summary>
        /// The bottom left node
        /// </summary>
        protected QuadTreeNode<T> BottomLeftNode;

        /// <summary>
        /// The bottom right node
        /// </summary>
        protected QuadTreeNode<T> BottomRightNode;

        /// <summary>
        /// The items in this node
        /// </summary>
        protected List<QuadTreePositionItem<T>> Items;

        /// <summary>
        /// Resize the world
        /// </summary>
        /// <param name="newSize">The new world size</param>
        protected ResizeDelegate WorldResize;

        #endregion

        #region Initialization

        /// <summary>
        /// QuadTreeNode constructor
        /// </summary>
        /// <param name="parentNode">The parent node of this QuadTreeNode</param>
        /// <param name="rect">The rectangle of the QuadTreeNode</param>
        /// <param name="maxItems">Maximum number of items in the QuadTreeNode before partitioning</param>
        public QuadTreeNode(QuadTreeNode<T> parentNode, FRect rect, int maxItems)
        {
            ParentNode = parentNode;
            Rect = rect;
            MaxItems = maxItems;
            IsPartitioned = false;
            Items = new List<QuadTreePositionItem<T>>();
        }

        /// <summary>
        /// QuadTreeNode constructor
        /// </summary>
        /// <param name="rect">The rectangle of the QuadTreeNode</param>
        /// <param name="maxItems">Maximum number of items in the QuadTreeNode before partitioning</param>
        /// <param name="worldResize">The function to return the size</param>
        public QuadTreeNode(FRect rect, int maxItems, ResizeDelegate worldResize)
        {
            ParentNode = null;
            Rect = rect;
            MaxItems = maxItems;
            WorldResize = worldResize;
            IsPartitioned = false;
            Items = new List<QuadTreePositionItem<T>>();
        }

        #endregion

        #region Insertion methods

        /// <summary>
        /// Insert an item in this node
        /// </summary>
        /// <param name="item">The item to insert</param>
        public void Insert(QuadTreePositionItem<T> item)
        {
            // If partitioned, try to find child node to add to
            if (!InsertInChild(item))
            {
                item.Destroy += new QuadTreePositionItem<T>.DestroyHandler(ItemDestroy);
                item.Move += new QuadTreePositionItem<T>.MoveHandler(ItemMove);
                Items.Add(item);

                // Check if this node needs to be partitioned
                if (!IsPartitioned && Items.Count >= MaxItems)
                {
                    Partition();
                }
            }
        }

        /// <summary>
        /// Inserts an item into one of this node's children
        /// </summary>
        /// <param name="item">The item to insert in a child</param>
        /// <returns>Whether or not the insert succeeded</returns>
        protected bool InsertInChild(QuadTreePositionItem<T> item)
        {
            if (!IsPartitioned) return false;

            if (TopLeftNode.ContainsRect(item.Rect))
                TopLeftNode.Insert(item);
            else if (TopRightNode.ContainsRect(item.Rect))
                TopRightNode.Insert(item);
            else if (BottomLeftNode.ContainsRect(item.Rect))
                BottomLeftNode.Insert(item);
            else if (BottomRightNode.ContainsRect(item.Rect))
                BottomRightNode.Insert(item);

            else return false; // insert in child failed

            return true;
        }

        /// <summary>
        /// Pushes an item down to one of this node's children
        /// </summary>
        /// <param name="i">The index of the item to push down</param>
        /// <returns>Whether or not the push was successful</returns>
        public bool PushItemDown(int i)
        {
            if (InsertInChild(Items[i]))
            {
                RemoveItem(i);
                return true;
            }

            else return false;
        }

        /// <summary>
        /// Push an item up to this node's parent
        /// </summary>
        /// <param name="i">The index of the item to push up</param>
        public void PushItemUp(int i)
        {
            QuadTreePositionItem<T> m = Items[i];

            RemoveItem(i);
            ParentNode.Insert(m);
        }

        /// <summary>
        /// Repartitions this node
        /// </summary>
        protected void Partition()
        {
            // Create the nodes
            Vector2 MidPoint = Vector2.Divide(Vector2.Add(Rect.TopLeft, Rect.BottomRight), 2.0f);

            TopLeftNode = new QuadTreeNode<T>(this,new FRect(Rect.TopLeft, MidPoint), MaxItems);
            TopRightNode = new QuadTreeNode<T>(this, new FRect(new Vector2(MidPoint.X, Rect.Top), new Vector2(Rect.Right, MidPoint.Y)), MaxItems);
            BottomLeftNode = new QuadTreeNode<T>(this, new FRect(new Vector2(Rect.Left, MidPoint.Y), new Vector2(MidPoint.X, Rect.Bottom)), MaxItems);
            BottomRightNode = new QuadTreeNode<T>(this, new FRect(MidPoint, Rect.BottomRight), MaxItems);

            IsPartitioned = true;

            // Try to push items down to child nodes
            int i = 0;
            while (i < Items.Count)
            {
                if (!PushItemDown(i))
                {
                    i++;
                }
            }
        }

        #endregion

        #region Query methods

        /// <summary>
        /// Gets a list of items containing a specified point
        /// </summary>
        /// <param name="Point">The point</param>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        /// <remarks>ItemsFound is assumed to be initialized, and will not be cleared</remarks>
        public void GetItems(Vector2 Point, ref List<QuadTreePositionItem<T>> ItemsFound)
        {
            // test the point against this node
            if (Rect.Contains(Point))
            {
                // test the point in each item
                foreach (QuadTreePositionItem<T> Item in Items)
                {
                    if (Item.Rect.Contains(Point)) ItemsFound.Add(Item);
                }

                // query all subtrees
                if (IsPartitioned)
                {
                    TopLeftNode.GetItems(Point, ref ItemsFound);
                    TopRightNode.GetItems(Point, ref ItemsFound);
                    BottomLeftNode.GetItems(Point, ref ItemsFound);
                    BottomRightNode.GetItems(Point, ref ItemsFound);
                }
            }
        }


        /// <summary>
        /// Gets a list of items of type A containing a specified point
        /// </summary>
        /// <typeparam name="A">The type we want</typeparam>
        /// <param name="Point">The point</param>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        /// <remarks>ItemsFound is assumed to be initialized, and will not be cleared</remarks>
        public void GetItems<A>(Vector2 Point, ref List<QuadTreePositionItem<T>> ItemsFound)
        {
            // test the point against this node
            if (Rect.Contains(Point))
            {
                // test the point in each item
                foreach (QuadTreePositionItem<T> Item in Items)
                {
                    if (Item.Rect.Contains(Point) && Item.Parent is A) 
                        ItemsFound.Add(Item);
                }

                // query all subtrees
                if (IsPartitioned)
                {
                    TopLeftNode.GetItems<A>(Point, ref ItemsFound);
                    TopRightNode.GetItems<A>(Point, ref ItemsFound);
                    BottomLeftNode.GetItems<A>(Point, ref ItemsFound);
                    BottomRightNode.GetItems<A>(Point, ref ItemsFound);
                }
            }
        }

        /// <summary>
        /// Gets a list of items intersecting a specified rectangle
        /// </summary>
        /// <param name="Rect">The rectangle</param>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        /// <remarks>ItemsFound is assumed to be initialized, and will not be cleared</remarks>
        public void GetItems(FRect Rect, ref List<QuadTreePositionItem<T>> ItemsFound)
        {
            // test the point against this node
            if (Rect.Intersects(Rect))
            {
                // test the point in each item
                foreach (QuadTreePositionItem<T> Item in Items)
                {
                    if (Item.Rect.Intersects(Rect))
                    {
                        ItemsFound.Add(Item);
                    }
                }

                // query all subtrees
                if (IsPartitioned)
                {
                    TopLeftNode.GetItems(Rect, ref ItemsFound);
                    TopRightNode.GetItems(Rect, ref ItemsFound);
                    BottomLeftNode.GetItems(Rect, ref ItemsFound);
                    BottomRightNode.GetItems(Rect, ref ItemsFound);
                }
            }
        }

        /// <summary>
        /// Gets a list of items, of type A, intersecting a specified rectangle
        /// </summary>
        /// <typeparam name="A">The rectangle</typeparam>
        /// <param name="Rect">The rectangle in wich we lik</param>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        /// <remarks>ItemsFound is assumed to be initialized, and will not be cleared</remarks>
        public void GetItems<A>(FRect Rect, ref List<QuadTreePositionItem<T>> ItemsFound)
        {
            // test the point against this node
            if (Rect.Intersects(Rect))
            {
                // test the point in each item
                foreach (QuadTreePositionItem<T> Item in Items)
                {
                    if (Item.Rect.Intersects(Rect) && Item.Parent is A)
                    {
                        ItemsFound.Add(Item);
                    }
                }

                // query all subtrees
                if (IsPartitioned)
                {
                    TopLeftNode.GetItems<A>(Rect, ref ItemsFound);
                    TopRightNode.GetItems<A>(Rect, ref ItemsFound);
                    BottomLeftNode.GetItems<A>(Rect, ref ItemsFound);
                    BottomRightNode.GetItems<A>(Rect, ref ItemsFound);
                }
            }
        }

        /// <summary>
        /// Gets a list of all items within this node
        /// </summary>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        /// <remarks>ItemsFound is assumed to be initialized, and will not be cleared</remarks>
        public void GetAllItems(ref List<QuadTreePositionItem<T>> ItemsFound)
        {
            ItemsFound.AddRange(Items);

            // query all subtrees
            if (IsPartitioned)
            {
                TopLeftNode.GetAllItems(ref ItemsFound);
                TopRightNode.GetAllItems(ref ItemsFound);
                BottomLeftNode.GetAllItems(ref ItemsFound);
                BottomRightNode.GetAllItems(ref ItemsFound);
            }
        }


        /// <summary>
        /// Gets a list of all items of type A within this node
        /// </summary>
        /// <typeparam name="A">The type</typeparam>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        /// <remarks>ItemsFound is assumed to be initialized, and will not be cleared</remarks>
        
        public void GetAllItems<A>(ref List<QuadTreePositionItem<T>> ItemsFound)
        {
            foreach (QuadTreePositionItem<T> quadTreePositionItem in Items)
            {
                if(quadTreePositionItem.Parent is A)
                {
                    ItemsFound.Add(quadTreePositionItem);
                }
            }

            // query all subtrees
            if (IsPartitioned)
            {
                TopLeftNode.GetAllItems<A>(ref ItemsFound);
                TopRightNode.GetAllItems<A>(ref ItemsFound);
                BottomLeftNode.GetAllItems<A>(ref ItemsFound);
                BottomRightNode.GetAllItems<A>(ref ItemsFound);
            }
        }

        /// <summary>
        /// Finds the node containing a specified item
        /// </summary>
        /// <param name="Item">The item to find</param>
        /// <returns>The node containing the item</returns>
        public QuadTreeNode<T> FindItemNode(QuadTreePositionItem<T> Item)
        {
            if (Items.Contains(Item)) return this;

            else if (IsPartitioned)
            {
                QuadTreeNode<T> n = null;

                // Check the nodes that could contain the item
                if (TopLeftNode.ContainsRect(Item.Rect))
                {
                    n = TopLeftNode.FindItemNode(Item);
                }
                if (n == null &&
                    TopRightNode.ContainsRect(Item.Rect))
                {
                    n = TopRightNode.FindItemNode(Item);
                }
                if (n == null &&
                    BottomLeftNode.ContainsRect(Item.Rect))
                {
                    n = BottomLeftNode.FindItemNode(Item);
                }
                if (n == null &&
                    BottomRightNode.ContainsRect(Item.Rect))
                {
                    n = BottomRightNode.FindItemNode(Item);
                }
                
                return n;
            }

            else return null;
        }

        #endregion

        #region Destruction

        /// <summary>
        /// Destroys this node
        /// </summary>
        public void Destroy()
        {
            // Destroy all child nodes
            if (IsPartitioned)
            {
                TopLeftNode.Destroy();
                TopRightNode.Destroy();
                BottomLeftNode.Destroy();
                BottomRightNode.Destroy();

                TopLeftNode = null;
                TopRightNode = null;
                BottomLeftNode = null;
                BottomRightNode = null;
            }

            // Remove all items
            while (Items.Count > 0)
            {
                RemoveItem(0);
            }
        }

        /// <summary>
        /// Removes an item from this node
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void RemoveItem(QuadTreePositionItem<T> item)
        {
            // Find and remove the item
            if (Items.Contains(item))
            {
                item.Move -= new QuadTreePositionItem<T>.MoveHandler(ItemMove);
                item.Destroy -= new QuadTreePositionItem<T>.DestroyHandler(ItemDestroy);
                Items.Remove(item);
            }
        }

        /// <summary>
        /// Removes an item from this node at a specific index
        /// </summary>
        /// <param name="i">the index of the item to remove</param>
        protected void RemoveItem(int i)
        {
            if (i < Items.Count)
            {
                Items[i].Move -= new QuadTreePositionItem<T>.MoveHandler(ItemMove);
                Items[i].Destroy -= new QuadTreePositionItem<T>.DestroyHandler(ItemDestroy);
                Items.RemoveAt(i);
            }
        }

        #endregion

        #region Observer methods

        /// <summary>
        /// Handles item movement
        /// </summary>
        /// <param name="item">The item that moved</param>
        public void ItemMove(QuadTreePositionItem<T> item)
        {
            // Find the item
            if (Items.Contains(item))
            {
                int i = Items.IndexOf(item);

                // Try to push the item down to the child
                if (!PushItemDown(i))
                {
                    // otherwise, if not root, push up
                    if (ParentNode != null)
                    {
                        PushItemUp(i);
                    }
                    else if (!ContainsRect(item.Rect))
                    {
                        WorldResize(new FRect(
                                        Vector2.Min(Rect.TopLeft, item.Rect.TopLeft) * 2,
                                        Vector2.Max(Rect.BottomRight, item.Rect.BottomRight) * 2));
                    }

                }
            }
            else
            {
                // this node doesn't contain that item, stop notifying it about it
                item.Move -= new QuadTreePositionItem<T>.MoveHandler(ItemMove);
            }
        }

        /// <summary>
        /// Handles item destruction
        /// </summary>
        /// <param name="item">The item that is being destroyed</param>
        public void ItemDestroy(QuadTreePositionItem<T> item)
        {
            RemoveItem(item);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Tests whether this node contains a rectangle
        /// </summary>
        /// <param name="rect">The rectangle to test</param>
        /// <returns>Whether or not this node contains the specified rectangle</returns>
        public bool ContainsRect(FRect rect)
        {
            return (rect.TopLeft.X >= Rect.TopLeft.X &&
                    rect.TopLeft.Y >= Rect.TopLeft.Y &&
                    rect.BottomRight.X <= Rect.BottomRight.X &&
                    rect.BottomRight.Y <= Rect.BottomRight.Y);
        }

        #endregion
    }
}