using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

class singleNode<K, T> : Item<K, T>
{
    // Класс основан на классе Item<K,T>, который является информационной частью
    // узла односвязного списка 
    private singleNode<K, T> next; // Ссылка на следующий узел
                                   // Конструкторы узла
    public singleNode(K key, T value) : base(key, value)
    {
        this.next = null;
    }
    public singleNode() : base()
    {
        this.next = null;
    }
    public singleNode<K, T> Next // Свойство
    {
        get { return this.next; }
        set { this.next = value; }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
class singleLinkedList<K, T>  where K : IComparable where T : IComparable
{
    private singleNode<K, T> first = null;   // Ссылка на начальный узел
    private int pos = 0;
    public singleNode<K, T> First { get { return first; } } // Свойство
                                                            
    //Конструктор
    public singleLinkedList() { first = null; pos = 0; }
    public int Count { get { return pos; } } // Свойство
                                             
    // Добавить в начало
    public int AddBegin(K key, T value)
    {
        singleNode<K, T> s = new singleNode<K, T>(key, value);
        s.Next = first; first = s;
        return this.pos++;
    }

    // Добавить в конец
    public int AddEnd(K key, T value)
    {
        singleNode<K, T> s = new singleNode<K, T>(key, value);
        // Если список пустой    
        if (this.first == null) { this.first = s; return this.pos++; }
        // Поиск последнего узла
        singleNode<K, T> e = this.first;
        while (e.Next != null)
        {
            e = e.Next;
        }
        // Добавление в конец
        e.Next = s;
        return this.pos++;
    }
    // Очистка списка
    public void Clear()
    {
        this.first = null;
        this.pos = 0;
    }
    // Проверка на значение
    public bool ContainsValue(T value)
    {
        if (this.first != null)
        {
            singleNode<K, T> e = this.first;
            do
            {
                if (e.Value.CompareTo(value) == 0)
                {
                    return true;
                }
                e = e.Next;
            } while (e != null);
        }
        return false;
    }
    // Проверка на ключ
    public bool ContainsKey(K key)
    {
        if (this.first != null)
        {
            singleNode<K, T> e = this.first;
            do
            {
                if (e.Key.CompareTo(key) == 0)
                {
                    return true;
                }
                e = e.Next;
            } while (e != null);
        }
        return false;
    }
    //Получение узла по индексу
    public singleNode<K, T> getNode(int k)
    {
        if (this.first == null || k >= Count) { return null; }
        singleNode<K, T> e = this.first;
        int i = 0;
        while (i < k)
        {
            e = e.Next;
            i++;
        }
        return e;
    }
    // Вставка после заданного узла
    public int InsertAfterNode(K key, K newKey, T Value)
    {
        singleNode<K, T> e = new singleNode<K, T> { Key = newKey, Value = Value };
        singleNode<K, T> currentNode = first;

        if (this.ContainsKey(key))
        {
            while (currentNode.Key.CompareTo(key) != 0)
                currentNode = currentNode.Next;
            if (currentNode != null)
            {
                (e.Next, currentNode.Next) = (currentNode.Next, e);
                return this.pos++;
            }
        }
        else
            AddEnd(newKey, Value);
        return this.pos;
    }

    // Вставка перед заданным узлом
    public int InsertBeforeNode(K key, K newKey, T Value)
    {
        singleNode<K, T> e = new singleNode<K, T> { Key = newKey, Value = Value };
        singleNode<K, T> currentNode = first;
        singleNode<K, T> currentNodePr = first;

        if (this.ContainsKey(key))
        {
            if (currentNode.Key.CompareTo(key) == 0)
            {
                AddBegin(newKey, Value);
                return this.pos;
            }
            while (currentNode.Key.CompareTo(key) != 0)
            {
                currentNodePr = currentNode;
                currentNode = currentNode.Next;
            }
            if (currentNode != null)
            {
                (e.Next, currentNodePr.Next) = (currentNodePr.Next, e);
                return this.pos++;
            }
        }
        else
            AddEnd(newKey, Value);
        return this.pos;
    }

    // Удаление начального узла
    public void RemoveFirstNode()
    {
        if (first == null)
            return;
        first = first.Next;
        this.pos--;
    }
    // Удаление конечного узла
    public void RemoveLastNode()
    {
        if (first == null)
        {
            return;
        }
        if (first.Next == null)
        {
            first = null;
            return;
        }
        singleNode<K, T> currentNode = first;
        while (currentNode.Next.Next != null)
        {
            currentNode = currentNode.Next;
        }
        currentNode.Next = null;
        this.pos--;
    }
    //удаление узла по номеру
    public void RemoveAt(int index)
    {
        if (index < 0)
            return;
        singleNode<K, T> currentNode = first;
        for (int i = 0; i < index - 1; i++)
        {
            if (currentNode.Next == null)
                return;
            currentNode = currentNode.Next;
        }
        if (currentNode.Next != null)
        {
            currentNode.Next = currentNode.Next.Next;
            this.pos--;
        }
    }

    //удаление узла по значению
    public void Remove(K key)
    {
        singleNode<K, T> currentNode = first;
        if (currentNode.Key.Equals(key)) RemoveFirstNode();

        while (currentNode.Next != null)
        {
            if (currentNode.Next.Key.Equals(key))
            {
                if (currentNode.Next.Next != null)
                {
                    currentNode.Next = currentNode.Next.Next;
                    this.pos--;
                    break;
                }
                else
                {
                    RemoveLastNode();
                    break;
                }
            }
            currentNode = currentNode.Next;
        }
    }
    // Вывод списка
    public void PrintList()
    {
        Console.WriteLine("Single List:");
        var current = first;
        while (current != null)
        {
            Console.Write(" {0} => ", current);
            current = current.Next;
        }
        Console.WriteLine();
    }
}