namespace OrdinalStatisticsTree
{
    enum Color { Red, Black };

    class Node
    {
        public float data;
        public Color color;
        public Node? left;
        public Node? right;
        public Node? parent;

        public Node(float data)
        {
            this.data = data;
            this.color = Color.Red;
            this.left = null;
            this.right = null;
            this.parent = null;
        }
    }

    internal class RedBlackTree
    {
        private Node? root;

        public RedBlackTree() => root = null;

        // Вставка в дерево
        public void Insert(float data) // O(log n)
        {
            Node? newNode = new Node(data);
            if (root == null)
            {
                root = newNode;
                newNode.color = Color.Black;
                return;
            }

            Node? current = root;
            Node? parent = null;
            while (true)
            {
                parent = current;
                if (data < current.data)
                {
                    current = current.left;
                    if (current == null)
                    {
                        parent.left = newNode;
                        newNode.parent = parent;
                        break;
                    }
                }
                else
                {
                    current = current.right;
                    if (current == null)
                    {
                        parent.right = newNode;
                        newNode.parent = parent;
                        break;
                    }
                }
            }

            FixTreeAfterInsert(newNode);
        }

        // Фіксація дерева після вставки
        private void FixTreeAfterInsert(Node node) // O(log n)
        {
            while (node.parent != null && node.parent.color == Color.Red)
            {
                Node? grandParent = node.parent.parent;

                if (node.parent == grandParent.left)
                {
                    Node? uncle = grandParent.right;
                    if (uncle != null && uncle.color == Color.Red)
                    {
                        grandParent.color = Color.Red;
                        node.parent.color = Color.Black;
                        uncle.color = Color.Black;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == node.parent.right)
                        {
                            node = node.parent;
                            RotateLeft(node);
                        }

                        node.parent.color = Color.Black;
                        grandParent.color = Color.Red;
                        RotateRight(grandParent);
                    }
                }
                else
                {
                    Node? uncle = grandParent.left;
                    if (uncle != null && uncle.color == Color.Red)
                    {
                        grandParent.color = Color.Red;
                        node.parent.color = Color.Black;
                        uncle.color = Color.Black;
                        node = grandParent;
                    }
                    else
                    {
                        if (node == node.parent.left)
                        {
                            node = node.parent;
                            RotateRight(node);
                        }

                        node.parent.color = Color.Black;
                        grandParent.color = Color.Red;
                        RotateLeft(grandParent);
                    }
                }
            }

            root.color = Color.Black;
        }

        // Обертання вліво
        private void RotateLeft(Node node) // O(log n)
        {
            Node? pivot = node.right;
            node.right = pivot.left;
            if (pivot.left != null)
                pivot.left.parent = node;

            pivot.parent = node.parent;
            if (node.parent == null)
                root = pivot;
            else if (node == node.parent.left)
                node.parent.left = pivot;
            else
                node.parent.right = pivot;

            pivot.left = node;
            node.parent = pivot;
        }

        // Обертання вправо
        private void RotateRight(Node node) // O(log n)
        {
            Node? pivot = node.left;
            node.left = pivot.right;
            if (pivot.right != null)
                pivot.right.parent = node;

            pivot.parent = node.parent;
            if (node.parent == null)
                root = pivot;
            else if (node == node.parent.right)
                node.parent.right = pivot;
            else
                node.parent.left = pivot;

            pivot.right = node;
            node.parent = pivot;
        }

        // Пошук порядкової статистики
        public float OrderStatistic(int k) // O(log n)
        {
            Node? node = root;
            while (node != null)
            {
                int leftSize = Size(node.left);

                if (k == leftSize + 1) return node.data;
                else if (k <= leftSize) node = node.left;
                else
                {
                    k = k - leftSize - 1;
                    node = node.right;
                }
            }

            Console.WriteLine("Order statistic not found");
            return 0;
        }

        // Обчислення розміру піддерева
        private int Size(Node? node)
        {
            if (node == null)
                return 0;

            return Size(node.left) + Size(node.right) + 1;
        }
    }
}
