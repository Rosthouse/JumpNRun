using Microsoft.Xna.Framework;

namespace SimplePhysicsAndCollision.Collision.Detection
{
    /// <summary>
    /// A position item in a quadtree
    /// </summary>
    /// <typeparam name="T">The type of the QuadTree item's parent</typeparam>
    public class QuadTreePositionItem<T>
    {
        #region Events and Event Handlers

        /// <summary>
        /// A movement handler delegate
        /// </summary>
        /// <param name="positionItem">The item that fired the event</param>
        public delegate void MoveHandler(QuadTreePositionItem<T> positionItem);

        /// <summary>
        /// A destruction handler delegate - fired when the item is destroyed
        /// </summary>
        /// <param name="positionItem">The item that fired the event</param>
        public delegate void DestroyHandler(QuadTreePositionItem<T> positionItem);

        /// <summary>
        /// Event handler for the move action
        /// </summary>
        public event MoveHandler Move;

        /// <summary>
        /// Event handler for the destroy action
        /// </summary>
        public event DestroyHandler Destroy;

        /// <summary>
        /// Handles the move event
        /// </summary>
        protected void OnMove()
        {
            // Update rectangles
            rect.TopLeft = position - (size * .5f);
            rect.BottomRight = position + (size * .5f);

            // Call event handler
            if (Move != null) Move(this);
        }

        /// <summary>
        /// Handles the destroy event
        /// </summary>
        protected void OnDestroy()
        {
            if (Destroy != null) Destroy(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The center position of this item
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// Gets or sets the center position of this item
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                OnMove();
            }
        }

        /// <summary>
        /// The size of this item
        /// </summary>
        private Vector2 size;

        /// <summary>
        /// Gets or sets the size of this item
        /// </summary>
        public Vector2 Size
        {
            get { return size; }
            set
            {
                size = value;
                rect.TopLeft = position - (size / 2f);
                rect.BottomRight = position + (size / 2f);
                OnMove();
            }
        }

        /// <summary>
        /// The rectangle containing this item
        /// </summary>
        private FRect rect;

        /// <summary>
        /// Gets a rectangle containing this item
        /// </summary>
        public FRect Rect
        {
            get { return rect; }
        }

        /// <summary>
        /// The parent of this item
        /// </summary>
        /// <remarks>The Parent accessor is used to gain access to the item controlling this position item</remarks>
        private T parent;

        /// <summary>
        /// Gets the parent of this item
        /// </summary>
        public T Parent
        {
            get { return parent; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a position item in a QuadTree
        /// </summary>
        /// <param name="parent">The parent of this item</param>
        /// <param name="position">The position of this item</param>
        /// <param name="size">The size of this item</param>
        public QuadTreePositionItem(T parent, Vector2 position, Vector2 size)
        {
            this.rect = new FRect(0f, 0f, 1f, 1f);

            this.parent = parent;
            this.position = position;
            this.size = size;
            OnMove();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Destroys this item and removes it from the QuadTree
        /// </summary>
        public void Delete()
        {
            OnDestroy();
        }

        #endregion
    }
}