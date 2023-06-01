using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;


class BinaryTree<T> where T : IComparable
{
    public Node<T> root = null;
    public BinaryTree()
    {
        root = null;
    }
    public Node<T> Root { get { return root; } }
    public void Add(int key, T value)
    {
        //Если корня нет, создаем корень
        if (root == null)
        {
            root = new Node<T>(key, value);
            return;
        }
        Node<T> currentNode = SearchPlace(key);
        //Если ключ уже есть
        if (currentNode != null && currentNode.key == key)
            currentNode.value = value;
        //Вставка нового узла
        else
        {
            Node<T> newNode = new Node<T>(key, value);
            if (key > currentNode.key)
                (currentNode.right, newNode.parent) = (newNode, currentNode);
            else
                (currentNode.left, newNode.parent) = (newNode, currentNode);
        }
    }
    //Поиск по ключю
    public Node<T> SearchByKey(int key)
    {
        if (root == null) return null;
        Node<T> currentNode = root;
        while (true)
        {
            if (currentNode.key == key) return currentNode;
            else if (currentNode.key < key && currentNode.right != null) currentNode = currentNode.right;
            else if (currentNode.key > key && currentNode.left != null) currentNode = currentNode.left;
            else return null;
        }
    }
    //Поиск места для ключа
    private Node<T> SearchPlace(int key)
    {
        if (root == null) return null;
        Node<T> currentNode = root;
        while (true)
        {
            if (currentNode.key == key) return currentNode;
            else if (currentNode.key < key && currentNode.right != null) currentNode = currentNode.right;
            else if (currentNode.key > key && currentNode.left != null) currentNode = currentNode.left;
            else return currentNode;
        }
    }
    //Поиск минимального ключа
    public Node<T> NodeMin()
    {
        if (root == null) return null;
        Node<T> currentNode = root;
        while (currentNode.left != null) currentNode = currentNode.left;
        return currentNode;
    }
    //Поиск максимального ключа
    public Node<T> NodeMax()
    {
        if (root == null) return null;
        Node<T> currentNode = root;
        while (currentNode.right != null) currentNode = currentNode.right;
        return currentNode;
    }
    //Вывод дерева (3 разных вывода)
    public void View() => ViewTree(root, 0);
    public void View2() => PrintDepths(root, 0);
    public void View3() => PrintTree(root, 0);
    private void ViewTree(Node<T> currentNode, int level)
    {
        if (currentNode != null)
        {
            if (currentNode.left != null) ViewTree(currentNode.left, level + 1);
            Console.WriteLine(currentNode + " level=" + level);
            if (currentNode.right != null) ViewTree(currentNode.right, level + 1);
        }
    }
    private void PrintDepths(Node<T> currentNode, int level)
    {
        if (currentNode == null) return;

        Console.WriteLine(currentNode + " level=" + level);
        PrintDepths(currentNode.left, level + 1);
        PrintDepths(currentNode.right, level + 1);
    }
    private void PrintTree(Node<T> currentNode, int level)
    {
        if (currentNode == null) return;

        PrintTree(currentNode.right, level + 1);
        Console.Write(new string(' ', level * 4));
        Console.WriteLine(currentNode);
        PrintTree(currentNode.left, level + 1);
    }

    //Удаление узла
    public void DeleteNode(int key) 
    {
        Node<T> currentNode = SearchByKey(key); 
        if (currentNode == null) return;
        //Удаляемый узел не имеет детей
        if (currentNode.left == null && currentNode.right == null)
        {
            //Удаляемый узел - корень дерева
            if (currentNode.parent == null) root = null;
            //Удаляемый узел - лист дерева
            else
            {         
                if (currentNode.parent.left == currentNode) currentNode.parent.left = null;        
                else currentNode.parent.right = null;     
            } 
        }
        //Удаляемый узел имеет только одного ребенка
        else if (currentNode.left == null || currentNode.right == null) 
        {     
            Node<T> child = currentNode.left ?? currentNode.right;
            //Удаляемый узел - корень дерева
            if (currentNode.parent == null) (root, child.parent) = (child, null);
            //Подключаем ребенка к родителю удаляемого узла
            else
            {         
                if (currentNode.parent.left == currentNode) currentNode.parent.left = child;         
                else currentNode.parent.right = child;          
                child.parent = currentNode.parent;     
            } 
        } 
        else 
        {
            //Удаляемый узел имеет двух детей
            DeleteTwoChild(currentNode);
        } 
    }

    //Балансировка дерева
    public void BalanceTree() => BalanceNodes(root);
    //Балансировка с удалением
    private void DeleteTwoChild(Node<T> currentNode)
    {
        List<int> listKey = new List<int>();
        ListKeyNodes(currentNode, listKey);
        listKey.Remove(currentNode.key);
        SortListKey(listKey);
        List<int> newListKey = BalanceList(listKey);

        BinaryTree<T> newTree = new BinaryTree<T>();
        foreach (int key in newListKey)
        {
            Node<T> tmp = SearchByKey(key);
            newTree.Add(tmp.key, tmp.value);
        }
        if (currentNode.parent == null) { root = newTree.root; }
        else if (currentNode.parent.left == currentNode) { currentNode.parent.left = newTree.root; }
        else if (currentNode.parent.right == currentNode) { currentNode.parent.right = newTree.root; };
    }
    //Балансировка узла
    private void BalanceNodes(Node<T> currentNode)
    {
        List<int> listKey = new List<int>(); // Создаем список для хранения ключей
        ListKeyNodes(currentNode, listKey); 
        SortListKey(listKey); // Вызываем функции для заполнения списка и его сортировки в порядке возрастания
        /* Создаем новый список и заполняем его ключами в таком порядке, 
         * чтобы при вызове ключей по порядку и заполнения дерева, дерево получалось сбалансированным*/
        List<int> newListKey = BalanceList(listKey);
        //Создание нового дерева и его заполнение

        BinaryTree<T> newTree = new BinaryTree<T>();
        foreach (int key in newListKey)
        {
            Node<T> tmp = SearchByKey(key);
            newTree.Add(tmp.key, tmp.value);
        }
        // Вставка нового дерева в узел
        if (currentNode.parent == null) { root = newTree.root; }
        else if (currentNode.parent.left == currentNode) { currentNode.parent.left = newTree.root; }
        else if (currentNode.parent.right == currentNode) { currentNode.parent.right = newTree.root; };
    }
    //Создание сбалансированного списка ключей
    private List<int> BalanceList(List<int> listKey)
    {
        // Создаем список и вставляем ключ из середины входящего списка
        List<int> newListKey = new List<int>();
        newListKey.Add(listKey[listKey.Count/2]);
        listKey.RemoveAt(listKey.Count/2);
        // Разбиение списка на 2 части - правая и левая
        List<int> listLeft = new List<int>();
        for (int i = 0; i < listKey.Count/2; i++)
        {
            listLeft.Add(listKey[0]);
            listKey.RemoveAt(0);
        }
        // Выполняется сбалансировка ключей
        BalanceListKey<T>(newListKey, listLeft, listKey);
        return newListKey;
    }
    //Сбалансирование ключей
    private void BalanceListKey<T>(List<int> newListKey, List<int> listLeft, List<int> listRight)
    {
        // Если левый список не пустой, то вытаскиваем ключ и добавляем в сбалансированный список
        if (listLeft.Count > 0)
        {
            int n = listLeft.Count / 2;
            newListKey.Add(listLeft[n]);
            listLeft.RemoveAt(n);
        }
        // Если правый список не пустой, то вытаскиваем ключ и добавляем в сбалансированный список
        if (listRight.Count > 0)
        {
            int m = listRight.Count / 2;
            newListKey.Add(listRight[m]);
            listRight.RemoveAt(m);
        }
        // Пока списки правых и левых частей не пусты, будет выполнятся сбалансирование ключей
        if (listLeft.Count > 0 || listRight.Count > 0)
            BalanceListKey<T>(newListKey, listLeft, listRight);
    }
    //Сортировка списка ключей
    private List<int> SortListKey(List<int> list)
    {
        int[] listKey = new int[list.Count];
        for (int i = 0; i < list.Count; i++) listKey[i] = list[i];        
        Sort.MergeSort(listKey, 0, listKey.Length - 1);
        for (int i = 0; i < listKey.Length; i++) list[i] = listKey[i];
        return list;
    }
    //Создает список ключей и заполняет его
    private void ListKeyNodes(Node<T> currentNode, List<int> listKey)
    {
        if (listKey == null) listKey = new List<int>();
        listKey.Add(currentNode.key);
        if (currentNode.right != null) { ListKeyNodes(currentNode.right, listKey);}
        if (currentNode.left != null) { ListKeyNodes(currentNode.left, listKey);}
    }

    //Очистить дерево
    public void Clean()
    {
        root = new Node<T>();
    }
}
