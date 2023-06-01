using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class doubleNode<K, T>
{
    private doubleNode<K, T> next;  // Ссылка на следующий узел
    private doubleNode<K, T> prev; // Ссылка на предыдущий узел
    private T value;
    private K key;
    public doubleNode<K, T> Next { set { next = value; } get { return next; } }
    public doubleNode<K, T> Prev { set { prev = value; } get { return prev; } }
    public K Key { set { key = value; } get { return key; } }
    public T Value { set { this.Value = value; } get { return this.value; } }
    // Конструкторы
    public doubleNode(K key, T value)
    { this.key = key; this.value = value; next = null; prev = null; }
    public doubleNode(K key, T value, doubleNode<K, T> pr)
    { this.key = key; this.value = value; next = null; prev = pr; }
    public override string ToString()
    {
        return string.Format("(key={0},Value={1})", Key, Value);
    }
}
class doubleLinkedList<K, T> where K : IComparable where T : IComparable
{
    private doubleNode<K, T> first = null; // Ссылка на первый узел
    private doubleNode<K, T> last = null; // Ссылка на последний узел
    private int pos = 0;
    public doubleNode<K, T> First { get { return first; } }
    public doubleNode<K, T> Last { get { return last; } }
    public doubleLinkedList() { first = null; last = null; pos = 0; }

    public int Count { get { return pos; } } // Свойство

    // Добавить в начало
    public int AddBegin(K key, T value)
    {
        doubleNode<K, T> t = new doubleNode<K, T>(key, value);
        if (this.first != null)
        {
            t.Next = this.first; this.first = t;
            this.first.Next.Prev = t;
        }
        else
        {
            this.first = t;
            this.last = t;
        }
        //doubleNode<K, T> node = new doubleNode<K, T>(key, value);
        //doubleNode<K, T> temp = first;
        //node.Next = temp;
        //this.first = node;
        //if (pos == 0)
        //    this.last = this.first;
        //else
        //    temp.Prev = node;
        return this.pos++;
    }
    public int AddEnd(K key, T value)
    {
        if (this.last != null)
        {
            doubleNode<K, T> t = new doubleNode<K, T>(key, value, this.last);
            this.last.Next = t;
            this.last = t;
        }
        else
        {
            doubleNode<K, T> t = new doubleNode<K, T>(key, value, null);
            this.first = t;
            this.last = t;
        }
        return this.pos++;
    }
    public void Clear()
    {
        this.first = null;
        this.last = null;
        this.pos = 0;
    }
    // Проверка на значение
    public bool ContainsValue(T value)
    {
        if (this.first != null)
        {
            doubleNode<K, T> e = this.first;
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
            doubleNode<K, T> e = this.first;
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
    public doubleNode<K, T> getNode(int k)
    {
        if (this.first == null || k >= Count) { return null; }
        doubleNode<K, T> e = this.first;
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
        doubleNode<K, T> e = new doubleNode<K, T>(newKey, Value);
        doubleNode<K, T> currentNode = first;

        if (this.ContainsKey(key))
        {
            while (currentNode.Key.CompareTo(key) != 0)
                currentNode = currentNode.Next;
            if (currentNode != null)
            {
                if (this.first == null) { AddBegin(newKey, Value); return this.pos; }
                else
                {
                    (e.Next, currentNode.Next) = (currentNode.Next, e);
                    (e.Prev, currentNode.Next.Next.Prev) = (currentNode, e);
                    return this.pos++;
                }
            }
        }
        else
            AddEnd(newKey, Value);
        return this.pos;
    }

    // Вставка перед заданным узлом
    public int InsertBeforeNode(K key, K newKey, T Value)
    {
        doubleNode<K, T> e = new doubleNode<K, T>(newKey, Value);
        doubleNode<K, T> currentNode = first;
        doubleNode<K, T> currentNodePr = first;

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
                if (this.first == null) { AddBegin(newKey, Value); }
                else
                {
                    (e.Next, currentNodePr.Next) = (currentNodePr.Next, e);
                    (e.Prev, currentNodePr.Next.Next.Prev) = (currentNodePr, e);
                }
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
        first.Prev = null;
        this.pos--;
    }
    // Удаление конечного узла
    public void RemoveLastNode()
    {
        if (first == null)
        {
            return;
        }
        else
        {
            this.last = this.last.Prev;
            this.last.Next = null;
        }
        this.pos--;
    }
    //удаление узла по номеру
    public void RemoveAt(int index)
    {
        if (index < 0)
            return;
        doubleNode<K, T> currentNode = first;
        for (int i = 0; i < index - 1; i++)
        {
            if (currentNode.Next == null)
                return;
            currentNode = currentNode.Next;
        }
        if (currentNode.Next != null)
        {
            currentNode.Next = currentNode.Next.Next;
            currentNode.Next.Prev = currentNode;
            this.pos--;
        }
    }

    //удаление узла по значению
    public void Remove(K key)
    {
        if (first == last && first.Key.Equals(key))
        {
            first = null;
            last = null;
        }

        if (first.Key.Equals(key)) RemoveFirstNode();
        if (last.Key.Equals(key)) RemoveLastNode();

        doubleNode<K, T> currentNode = first;
        while (currentNode.Next != null)
        {
            if (currentNode.Next.Key.Equals(key))
            {
                if (currentNode.Next.Next != null)
                {
                    currentNode.Next = currentNode.Next.Next;
                    currentNode.Next.Prev = currentNode;
                    break;
                }
                else
                    RemoveLastNode();
            }
            currentNode = currentNode.Next;
        }
        this.pos--;
    }
    // Вывод списка с начала
    public void PrintDoubleNodeNext()
    {
        Console.WriteLine("Double List:");
        var current = first;
        while (current != null)
        {
            Console.Write(" {0} =>", current);
            current = current.Next;
        }
        Console.WriteLine();
    }
    // Вывод списка с конца
    public void PrintDoubleNodePrev()
    {
        Console.WriteLine("Double List:");
        var current = last;
        while (current != null)
        {
            Console.Write(" {0} <=", current);
            current = current.Prev;
        }
        Console.WriteLine();
    }
}
