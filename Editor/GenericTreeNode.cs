using System.Windows.Forms;

namespace Editor
{
    public class TreeNode<T>:TreeNode
    {
        private T item;

        public TreeNode(T item)
        {
            this.item = item;
        }

        public TreeNode(T item, string text):base(text)
        {
            this.item = item;
        }

        public TreeNode(T item, string text, int imageIndex, int selectedImageIndex): base(text, imageIndex, selectedImageIndex)
        {
            this.item = item;   
        }

        public TreeNode(T item, string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
            : base(text, imageIndex, selectedImageIndex, children)
        {
            this.item = item;
        }

        public TreeNode(T item, string text, TreeNode[] children): base(text, children)
        {
            this.item = item;
        }

        public T Item
        {
            get { return item; }
            set { item = value; }
        }
    }
}