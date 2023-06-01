using System;

class Print
{
    //Вывод на экран
    public static void PrintCount<T>(T[] count)
    {
        foreach (var c in count) Console.Write("{0}\t", c);
        Console.WriteLine();
    }
    public static void PrintList<T>(List<T> count)
    {
        foreach (var c in count) { Console.Write("{0} ", c);  }
        Console.WriteLine();
    }
}
class Test
{
    //Тестирование множеств
    public static void TestSets()
    {
        Console.WriteLine("Test");
        Set<int> s1 = new Set<int>(4);
        s1.Add(5); s1.Add(4); s1.Add(8);
        Set<int> s2 = new Set<int>(6);
        s2.Add(7); s2.Add(8); s2.Add(1);

        Console.WriteLine("s1{0} + s2{1} = {2}", s1, s2, s1 + s2);
        Console.WriteLine("s1{0} * s2{1} = {2}", s1, s2, s1 * s2);
        Console.WriteLine("s1{0} - s2{1} = {2}", s1, s2, s1 - s2);

        Console.WriteLine("All subsets {0}", s1);
        List<Set<int>> ss = s1.subSets();
        Print.PrintList(ss);

        Console.WriteLine("All Permitions {0}", s2);
        List<Set<int>> ss2 = s2.Permutations();
        Print.PrintList(ss2);

        Console.WriteLine("All Combinations {0}", s2);
        List<Set<int>> ss3 = s2.Combinations(2);
        Print.PrintList(ss3);
    }
    //Тестирование сортировок
    public static void TestSort()
    {
        Console.WriteLine("Bubble Sorting");
        int[] ab = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Print.PrintCount(ab);
        

        Console.WriteLine("\nSort:");
        Sort.BubbleSort<int>(ab);
        Print.PrintCount(ab);
        Console.WriteLine("\n----------");

        Console.WriteLine("MaxElement Sorting");
        int[] an = { 4, -5, 18, 0, 8, 3, -9, -14 };
        Print.PrintCount(an);
        
        Console.WriteLine("\nSort:");
        Sort.SearchMaxElement<int>(an);
        Print.PrintCount(an);
        Console.WriteLine("\n----------");


        Console.WriteLine("Insertion Sorting");
        int[] eee = { 22, 4, -5, 18, 0, 8, 3, -9, -14 };
        Print.PrintCount(eee);
        
        Console.WriteLine("\nSort:");
        Sort.InsertionSort<int>(eee);
        Print.PrintCount(eee);
        Console.WriteLine("\n----------");


        Console.WriteLine("Shell Sorting");
        int[] eelr = { 22, 4, -5, 18, 0, 8, 3, -9, -14 };
        Print.PrintCount(eelr);
        
        Console.WriteLine("\nSort:");
        Sort.ShellSort<int>(eelr);
        Print.PrintCount(eelr);
        Console.WriteLine("\n----------");


        Console.WriteLine("Radix Sorting");
        int[] lr = { 222, 400, 115, 18, 90, 998, 63, 59, 4 };
        Print.PrintCount(lr);
        
        Console.WriteLine("\nSort:");
        Sort.RadixSort(lr);
        Print.PrintCount(lr);
        Console.WriteLine("\n----------");

        Console.WriteLine("Merge Sorting");
        int[] arrayMerge = { 9222, 400, -115, 18, 90, 998, -63, 59, 4, 637, -23, 0, -999 };
        Print.PrintCount(arrayMerge);
        
        Console.WriteLine("\nSort:");
        Sort.MergeSort(arrayMerge, 0, arrayMerge.Length-1);
        Print.PrintCount(arrayMerge);
        Console.WriteLine("\n----------");

        Console.WriteLine("Merge Sorting without recursion");
        int[] arrayMergeNR = { 9222, 400, -115, 18, 90, 998, -63, 59, 4, 637, -23, 0, -999 };
        Print.PrintCount(arrayMergeNR);

        Console.WriteLine("\nSort:");
        Sort.MergeSortNR<int>(arrayMergeNR);
        Print.PrintCount(arrayMergeNR);
        Console.WriteLine("\n----------");

        Console.WriteLine("Quick Sort");
        int[] quickSort = { 9222, 400, -115, 18, 90, 998, -63, 59, 4, 637, -23, 0, -999 };
        Print.PrintCount(quickSort);

        Console.WriteLine("\nSort:");
        Sort.QuickSort(quickSort, 0, quickSort.Length - 1);
        Print.PrintCount(quickSort);
        Console.WriteLine("\n----------");
    }
    //Тестирование односвязного списка
    public static void TestSingleLinkedList()
    {
        // Пример использования односвязного списка
        Console.WriteLine("Test Linked Data");
        Item<int, string>[] sns = { new Item<int, string>(1, "fff"), new Item<int, string>(5, "fi"), new Item<int, string>(10, "tttt") };
        var sLL = new singleLinkedList<int, string>();
        sLL.AddBegin(2, "Second");
        int CountSL = sLL.Count;
        for (int i = 0; i < sns.Length; i++) sLL.AddBegin(sns[i].Key, sns[i].Value);
        sLL.AddEnd(7, "Seven"); sLL.AddEnd(1345, "Sevкпen"); sLL.AddEnd(21, "Sуцамуeven");
        for (int k = 0; k < sLL.Count; k++)
        {
            Console.Write("  {0}", sLL.getNode(k));
        }
        Console.WriteLine("----------");
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine("----------");
        Console.WriteLine("Добавить в начало:");
        sLL.AddBegin(100, "dcwd");
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine("----------");
        Console.WriteLine("Добавить в конец:");
        sLL.AddEnd(124, "rgbervr");
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine("----------");
        Console.WriteLine("Удаление начального узла");
        sLL.RemoveFirstNode();
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine("----------");
        Console.WriteLine("Удаление конечного узла");
        sLL.RemoveLastNode();
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine("----------");
        Console.WriteLine("Удаление узла по номеру(2)");
        sLL.RemoveAt(2);
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine("----------");
        Console.WriteLine("Удаление узла по значению(21)");
        sLL.Remove(21);
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine("----------");
        Console.WriteLine("Вставка после:");
        sLL.InsertAfterNode(1000, 999, "ketkat");
        sLL.InsertAfterNode(21, 990, "sdvtkat");
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine("----------");
        Console.WriteLine("Вставка до:");
        sLL.InsertBeforeNode(10, 910, "katkot");
        sLL.InsertBeforeNode(990, 1121, "tokkokat");
        sLL.PrintList();
        Console.WriteLine("Текущее число эоементов - {0}", sLL.Count);

        Console.WriteLine();
        
    }
    //Тестирование двухсвязного списка
    public static void TestDoubleLinkedList()
    {
        Console.WriteLine();
        var dLL = new doubleLinkedList<int, string>();

        
        dLL.AddBegin(10, "fdg");
        dLL.AddEnd(20, "ouy");
        dLL.AddEnd(21, "dse");
        dLL.AddBegin(11, "qwe");
        dLL.AddBegin(13, "t1ry");

        Console.WriteLine("Вывод с начала:");
        dLL.PrintDoubleNodeNext();
        Console.WriteLine("----------\nВывод с конца:");
        dLL.PrintDoubleNodePrev();
        Console.WriteLine("Текущее число эоементов - {0}", dLL.Count);

        Console.WriteLine("----------\nПроверка на ключ:");
        Console.WriteLine("10 - {0}",dLL.ContainsKey(10));
        Console.WriteLine("100 - {0}", dLL.ContainsKey(100));

        Console.WriteLine("----------\nПроверка на значение:");
        Console.WriteLine("ouy - {0}", dLL.ContainsValue("ouy"));
        Console.WriteLine("ttt - {0}", dLL.ContainsValue("ttt"));

        Console.WriteLine("----------\nПроверка getNode:");
        for (int k = 0; k < dLL.Count; k++)
        {
            Console.Write("  {0}", dLL.getNode(k));
        }

        Console.WriteLine("\n----------\nВставка после узла:");
        dLL.InsertAfterNode(10, 26, "ggg");
        dLL.PrintDoubleNodeNext();
        Console.WriteLine("----------");
        dLL.PrintDoubleNodePrev();
        Console.WriteLine("Текущее число эоементов - {0}", dLL.Count);

        Console.WriteLine("\n----------\nВставка до узла:");
        dLL.InsertBeforeNode(11, 100, "bls");
        dLL.PrintDoubleNodeNext();
        Console.WriteLine("----------");
        dLL.PrintDoubleNodePrev();
        Console.WriteLine("Текущее число эоементов - {0}", dLL.Count);

        Console.WriteLine("\n----------\nУдаление первого узла:");
        dLL.RemoveFirstNode();
        dLL.PrintDoubleNodeNext();
        Console.WriteLine("----------");
        dLL.PrintDoubleNodePrev();
        Console.WriteLine("Текущее число эоементов - {0}", dLL.Count);

        Console.WriteLine("\n----------\nУдаление последнего узла:");
        dLL.RemoveLastNode();
        dLL.PrintDoubleNodeNext();
        Console.WriteLine("----------");
        dLL.PrintDoubleNodePrev();
        Console.WriteLine("Текущее число эоементов - {0}", dLL.Count);

        Console.WriteLine("\n----------\nУдаление узла по номеру(3):");
        dLL.RemoveAt(3);
        dLL.PrintDoubleNodeNext();
        Console.WriteLine("----------");
        dLL.PrintDoubleNodePrev();
        Console.WriteLine("Текущее число эоементов - {0}", dLL.Count);

        Console.WriteLine("\n----------\nУдаление узла по индуксу(10):");
        dLL.Remove(10);
        dLL.PrintDoubleNodeNext();
        Console.WriteLine("----------");
        dLL.PrintDoubleNodePrev();
        Console.WriteLine("Текущее число элементов - {0}", dLL.Count);

    }
    //Тестирование Хэш-таблицы
    public static void TestHashTableList()
    {
        Console.WriteLine("Тестирование хэш-листа");
        HashTableList<int, string> hashTableList = new HashTableList<int, string>(5);
        hashTableList.Add(8, "ggg"); hashTableList.Add(2, "aaa"); hashTableList.Add(3, "kkk"); hashTableList.Add(4, "nvf"); hashTableList.Add(5, "lll");
        hashTableList.Add(14, "oir"); hashTableList.Add(15, "poo"); hashTableList.Add(14, "asf"); hashTableList.Add(15, "her");
        hashTableList.Print();
        Console.WriteLine("Рaзмер - {0}:{1}", hashTableList.Count, hashTableList.Size);

        hashTableList.DeleteByKey(3);
        Console.WriteLine("Удаление ключа 3:");
        hashTableList.Print();
        Console.WriteLine("Рaзмер - {0}:{1}", hashTableList.Count, hashTableList.Size);

        Console.WriteLine("Поиск по ключю 14: {0}", hashTableList.SearchByItem(14));
    }
    public static void TestHashTableArray()
    {
        Console.WriteLine("\n\nТестирование хэш-таблицы");
        HashTableArray<int, string> hashTableArray = new HashTableArray<int, string>(5);
        hashTableArray.Add(8, "ggg"); hashTableArray.Add(2, "aaa"); hashTableArray.Add(3, "kkk"); hashTableArray.Add(4, "nvf"); hashTableArray.Add(5, "lll");
        hashTableArray.Add(14, "oir"); hashTableArray.Add(15, "poo"); hashTableArray.Add(14, "asf"); hashTableArray.Add(15, "her"); hashTableArray.Add(1, "bhf");
        hashTableArray.Print();
        Console.WriteLine("Рaзмер - {0}:{1}", hashTableArray.Count, hashTableArray.Size);


        hashTableArray.DeleteByKey(3);
        Console.WriteLine("Удаление ключа 3:");
        hashTableArray.Print();
        Console.WriteLine("Рaзмер - {0}:{1}", hashTableArray.Count, hashTableArray.Size);

        Console.WriteLine("Поиск по ключю 14: {0}", hashTableArray.SearchByKey(14));
    }

    //Тест бинароного дерева
    public static void TestBinaryTree1()
    {
        BinaryTree<string> binaryTree = new BinaryTree<string>();
        binaryTree.Add(10, "efv");
        binaryTree.Add(15, "tge");
        binaryTree.Add(5, "ggg");
        binaryTree.Add(1, "asd");
        binaryTree.Add(7, "ert");
        binaryTree.Add(21, "jkg");
        Console.WriteLine("Корень = {0}", binaryTree.root);
        binaryTree.View();
        Console.WriteLine("\nМинимальный узел: {0}", binaryTree.NodeMin());
        Console.WriteLine("Максимальный узел: {0}", binaryTree.NodeMax());
        Console.WriteLine("\nУдаление по ключю 5");
        binaryTree.DeleteNode(10);
        binaryTree.View();
        binaryTree.Add(4, "a");
        binaryTree.Add(3, "e");
        binaryTree.Add(1, "j");
        Console.WriteLine("----------");
        binaryTree.View();
        Console.WriteLine("----------\nСбалансированное дерево:");
        binaryTree.BalanceTree();
        binaryTree.View();
        Console.WriteLine("----------");
        binaryTree.View2();
        Console.WriteLine("----------");
        binaryTree.View3();
    }
    public static void TestBinaryTree2()
    {
        BinaryTree<string> binaryTree= new BinaryTree<string>();
        binaryTree.Add(20, "root");
        binaryTree.Add(10, "");
        binaryTree.Add(30, "");
        binaryTree.Add(23, "");
        binaryTree.Add(24, "");
        binaryTree.Add(25, "");
        binaryTree.Add(5, "");
        binaryTree.Add(8, "");
        binaryTree.Add(6, "");
        binaryTree.View3();
        Console.WriteLine("----------\nСбалансированное дерево:");
        binaryTree.BalanceTree();
        binaryTree.View3();
    }
    //Тестирование графа
    public static void TestGraph()
    {
        Vertex A = new Vertex("A");
        Vertex B = new Vertex("B");
        Vertex C = new Vertex("C");
        Vertex D = new Vertex("D");
        Vertex E = new Vertex("E");
        Vertex F = new Vertex("F");

        // Создаем граф
        Graph G = new Graph();
        G.allVertexs.Add(A);
        G.allVertexs.Add(B);
        G.allVertexs.Add(C);
        G.allVertexs.Add(D);
        G.allVertexs.Add(E);
        G.allVertexs.Add(F);

        // Добавляем ребра
        G.AddEdge(A, B, 8);
        G.AddEdge(A, C, 2);
        G.AddEdge(B, C, 5);
        G.AddEdge(D, E, 4);

        // Вывод графа
        G.ViewGraph();

        // Кол-во путей
        Print.PrintList(G.Get_Path(A, C));


        // Определяем кол-во подграфов на основе BFS
        Console.WriteLine("\nКол-во подграфов: {0}", G.CountConnectedComponents());

        // запускаем BFS
        // выводим результаты BFS
        Console.WriteLine("\nBFS results (steps = {0}):", G.BFS(B));
        foreach (Vertex v in G.allVertexs)
        {
            Console.WriteLine("Vertex: {0}, Distance: {1}, Previous Vertex: {2}", v.label, v.sumdistance, v.prevvertex?.label ?? "null");
        }

        // запускаем DFS
        // выводим результаты DFS
        Console.WriteLine("\nDFS results (steps = {0}):", G.DFS(C));
        foreach (Vertex v in G.allVertexs)
        {
            Console.WriteLine("Vertex: {0}, Distance: {1}, Previous Vertex: {2}", v.label, v.sumdistance, v.prevvertex?.label ?? "null");
        }

    }
}
class Program
{
    public static void Main(string[] args)
    {
        //Test.TestSort();
        //Test.TestSets();
        //Test.TestSingleLinkedList();
        //Test.TestDoubleLinkedList();
        Test.TestHashTableList();
        Test.TestHashTableArray();
        //Test.TestBinaryTree1();
        //Test.TestBinaryTree2();
        //Test.TestGraph();

        Console.ReadKey();
    }
}