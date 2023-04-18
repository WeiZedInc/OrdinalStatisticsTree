namespace OrdinalStatisticsTree
{
    internal class Program
    {
        static void Main(string[] args) // entry point
        {
            RedBlackTree? tree;

            if (FillCollection(out tree))
                Console.Write("Enter the k-th element to find: ");
            else
                Console.WriteLine("Something went wrong");

            int element = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"{element}-th element: " + tree.OrderStatistic(element));
        }


        static bool FillCollection(out RedBlackTree? tree) // parsing input and filling tree
        {
            Console.WriteLine("Input values using whitespace separator");
            var cuttedValues = Console.ReadLine()?.Split(" ", StringSplitOptions.TrimEntries);
            if (cuttedValues == null || cuttedValues.Length == 0)
            {
                Console.WriteLine("Try to input values using whitespace separator");
                tree = null;
                return false;
            }


            tree = new RedBlackTree();
            float value = 0;
            foreach (var stringNum in cuttedValues)
            {
                if (float.TryParse(stringNum, out value))
                    tree.Insert(value);
                else 
                    Console.WriteLine(stringNum + " is not valid number.");
            }
            return true;
        }
    }
}