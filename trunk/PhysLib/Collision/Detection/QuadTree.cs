// // // // // // // // // // // // //
// QuadTree and supporting code
// by Kyle Schouviller
// http://www.kyleschouviller.com
//
// December 2006: Original version
// May 06, 2007:  Updated for XNA Framework 1.0
//                and public release.
//
// You may use and modify this code however you
// wish, under the following condition:
// *) I must be credited
// A little line in the credits is all I ask -
// to show your appreciation.
// 
// If you have any questions, please use the
// contact form on my website.
//
// Now get back to making great games!
// // // // // // // // // // // // //

#region Using declarations

using System.Collections.Generic;
using Microsoft.Xna.Framework;

#endregion

namespace SimplePhysicsAndCollision.Collision.Detection
{
    /// <summary>
    /// A QuadTree for partitioning a space into rectangles
    /// </summary>
    /// <typeparam name="T">The type of the QuadTree's items' parents</typeparam>
    /// <remarks>This QuadTree automatically resizes as needed</remarks>
    class QuadTree<T>
    {
        #region Properties

        /// <summary>
        /// The head node of the QuadTree
        /// </summary>
        protected QuadTreeNode<T> headNode;

        /// <summary>
        /// Gets the world rectangle
        /// </summary>
        public FRect WorldRect
        {
            get { return headNode.Rect; }
        }

        /// <summary>
        /// The maximum number of items in any node before partitioning
        /// </summary>
        protected int maxItems;

        #endregion

        #region Initialization

        /// <summary>
        /// QuadTree constructor
        /// </summary>
        /// <param name="worldRect">The world rectangle for this QuadTree (a rectangle containing all items at all times)</param>
        /// <param name="maxItems">Maximum number of items in any cell of the QuadTree before partitioning</param>
        public QuadTree(FRect worldRect, int maxItems)
        {
            this.headNode = new QuadTreeNode<T>(worldRect, maxItems, Resize);
            this.maxItems = maxItems;
        }

        /// <summary>
        /// QuadTree constructor
        /// </summary>
        /// <param name="size">The size of the QuadTree (i.e. the bottom-right with a top-left of (0,0))</param>
        /// <param name="maxItems">Maximum number of items in any cell of the QuadTree before partitioning</param>
        /// <remarks>This constructor is for ease of use</remarks>
        public QuadTree(Vector2 size, int maxItems)
            : this(new FRect(Vector2.Zero, size), maxItems)
        {
            // Nothing extra to initialize
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts an item into the QuadTree
        /// </summary>
        /// <param name="item">The item to insert</param>
        /// <remarks>Checks to see if the world needs resizing and does so if needed</remarks>
        public void Insert(QuadTreePositionItem<T> item)
        {
            // check if the world needs resizing
            if (!headNode.ContainsRect(item.Rect))
            {
                Resize(new FRect(
                    Vector2.Min(headNode.Rect.TopLeft, item.Rect.TopLeft)*2,
                    Vector2.Max(headNode.Rect.BottomRight, item.Rect.BottomRight)*2));
            }

            headNode.Insert(item);
        }

        /// <summary>
        /// Inserts an item into the QuadTree
        /// </summary>
        /// <param name="parent">The parent of the new position item</param>
        /// <param name="position">The position of the new position item</param>
        /// <param name="size">The size of the new position item</param>
        /// <returns>A new position item</returns>
        /// <remarks>Checks to see if the world needs resizing and does so if needed</remarks>
        public QuadTreePositionItem<T> Insert(T parent, Vector2 position, Vector2 size)
        {
            QuadTreePositionItem<T> item = new QuadTreePositionItem<T>(parent, position, size);

            // check if the world needs resizing
            if (!headNode.ContainsRect(item.Rect))
            {
                Resize(new FRect(
                    Vector2.Min(headNode.Rect.TopLeft, item.Rect.TopLeft) * 2,
                    Vector2.Max(headNode.Rect.BottomRight, item.Rect.BottomRight) * 2));
            }

            headNode.Insert(item);

            return item;
        }

        public void Resize(Vector2 newSize)
        {
            Resize(new FRect(Vector2.Zero, newSize));
        }

        /// <summary>
        /// Resizes the Quadtree field
        /// </summary>
        /// <param name="newWorld">The new field</param>
        /// <remarks>This is an expensive operation, so try to initialize the world to a big enough size</remarks>
        public void Resize(FRect newWorld)
        {
            // Get all of the items in the tree
            List<QuadTreePositionItem<T>> Components = new List<QuadTreePositionItem<T>>();
            GetAllItems(ref Components);

            // Destroy the head node
            headNode.Destroy();
            headNode = null;
            
            // Create a new head
            headNode = new QuadTreeNode<T>(newWorld, maxItems, Resize);

            // Reinsert the items
            foreach (QuadTreePositionItem<T> m in Components)
            {
                headNode.Insert(m);
            }
        }

        #endregion

        #region Query methods

        /// <summary>
        /// Gets a list of items containing a specified point
        /// </summary>
        /// <param name="Point">The point</param>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        public void GetItems(Vector2 Point, ref List<QuadTreePositionItem<T>> ItemsList)
        {
            if (ItemsList != null)
            {
                headNode.GetItems(Point, ref ItemsList);
            }
        }

        /// <summary>
        /// Gets a list of items intersecting a specified rectangle
        /// </summary>
        /// <param name="Rect">The rectangle</param>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        public void GetItems(FRect Rect, ref List<QuadTreePositionItem<T>> ItemsList)
        {
            if (ItemsList != null)
            {
                headNode.GetItems(Rect, ref ItemsList);
            }
        }

        /// <summary>
        /// Get a list of all items in the quadtree
        /// </summary>
        /// <param name="ItemsFound">The list to add found items to (list will not be cleared first)</param>
        public void GetAllItems(ref List<QuadTreePositionItem<T>> ItemsList)
        {
            if (ItemsList != null)
            {
                headNode.GetAllItems(ref ItemsList);
            }
        }

        #endregion
    }
}
