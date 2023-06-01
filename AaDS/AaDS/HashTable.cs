using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


class HashTableArray<K, T> where K : IComparable where T : IComparable
{
    Item<K, T>[] list;
    int size;
    int count;

    public HashTableArray(int size)
    {
        this.size = size;
        list = new Item<K, T>[size];
        for (int i = 0; i < size; i++)
            list[i] = null;
        count = 0;
    }
    public int Count { get { return count; } }
    public int Size { get { return size; } }
    public int HashCode(K key)
    {
        return key.GetHashCode();
    }
    public int GetIndex(K key)
    {
        return key.GetHashCode() % size;
    }
    public int SearchByKey(K key)
    {
        int index = GetIndex(key);
        for (int i = index; i < size; i++)
        {
            if (list[i] == null || list[i].Key.CompareTo(default(K)) == 0) return -1;
            if (list[i].Key.CompareTo(key) == 0) return i;
        }

        for (int i = 0; i < index; i++)
        {
            if (list[i] == null || list[i].Key.CompareTo(default(K)) == 0) return -1;
            if (list[i].Key.CompareTo(key) == 0) return i;
        }
        return -1;
    }
    public Item<K, T> SearchItemByKey(K key)
    {
        int index = SearchByKey(key);
        if (index != -1) return list[index];
        return null;
    }
    public int Add(K key, T value)
    {
        if (count == size) Resize(size * 2);
        int index = SearchByKey(key);
        if (index != -1)
        {
            list[index].Value = value;
            return count;
        }
        index = GetIndex(key);
        if (list[index] == null || list[index].Key.CompareTo(default(K)) == 0)
        {
            list[index] = new Item<K, T>(key, value);
            return count++;
        }
        for (int i = index + 1; i < size; i++)
            if (list[i] == null || list[index].Key.CompareTo(default(K)) == 0)
            {
                list[i] = new Item<K, T>(key, value);
                return count++;
            }
        for (int i = 0; i < index; i++)
            if (list[i] == null || list[index].Key.CompareTo(default(K)) == 0)
            {
                list[i] = new Item<K, T>(key, value);
                return count++;
            }
        return count;
    }
    public Item<K, T>[] ToArray()
    {
        Item<K, T>[] array = new Item<K, T>[count];
        int i = 0;
        foreach (Item<K, T> elem in list)
            if (elem != null && elem.Key.CompareTo(default(K)) != 0)
                array[i++] = elem;
        return array;
    }
    public void Resize(int newsize)
    {
        if (newsize >= size)
        {
            Item<K, T>[] copy = this.ToArray();
            list = new Item<K, T>[newsize];
            this.size = newsize;
            count = 0;
            foreach (Item<K, T> elem in copy) this.Add(elem.Key, elem.Value);
        }
    }
    public bool DeleteByKey(K key)
    {
        int index = SearchByKey(key);
        if (index == -1) return false;
        list[index].Key = default(K);
        list[index].Value = default(T);
        count--;
        return true;
    }
    public void Print()
    {
        for (int i = 0; i < size; i++)
            if (list[i] != null && list[i].Key.CompareTo(default(K)) != 0)
                Console.WriteLine($"Index = {i}  {list[i]}");
    }
}

class HashTableList<K, T> where K : IComparable where T : IComparable
{
    // Массив списков
    List<Item<K, T>>[] lists;
    int size; // Размер таблицы
    int count; // Количество элементов
    Item<K, T> curItem = new Item<K, T>(default(K), default(T));
    public int Count { get { return count; } }
    public int Size { get { return size; } }
    // Конструктор
    public HashTableList(int size)
    {
        K kk = curItem.Key;
        T vv = curItem.Value;
        this.size = size;
        lists = new List<Item<K, T>>[size];
        for (int i = 0; i < size; i++)
            lists[i] = new List<Item<K, T>>();
        count = 0;
    }
    public int HashCode(K key)
    {
        return key.GetHashCode();
    }
    public int GetIndex(K key)
    {
        return key.GetHashCode() % size;
    }
    public void Add(K key, T value)//добавление пары ключ-значение
    {
        int index = GetIndex(key);
        bool flag = false;
        foreach (Item<K, T> item in lists[index])     // если ключ уже существует
            if (key.CompareTo(item.Key) == 0 || item.Key.CompareTo(default(K)) == 0)
            {
                item.Key = key;
                item.Value = value;
                flag = true; break;
            }
        if (!flag) { lists[index].Add(new Item<K, T>(key, value)); };
        count++;
    }
    //удаление элемента по ключу
    public void DeleteByKey(K key)
    {
        int index = GetIndex(key);
        if (index != -1)
            foreach (Item<K, T> elem in lists[index])
                if (elem.Key.CompareTo(key) == 0) { lists[index].Remove(elem); count--; break; }
    }
    public Item<K, T> SearchByItem(K key)
    {
        int index = GetIndex(key);
        if (index != -1)
            foreach (Item<K, T> elem in lists[index])
                if (elem.Key.CompareTo(key) == 0) return elem;
        return null;
    }
    public Item<K, T>[] ToArray()
    {
        Item<K, T>[] array = new Item<K, T>[count];
        int i = 0;
        foreach (List<Item<K, T>> list in lists)
            foreach (Item<K, T> elem in list)
                array[i++] = elem;
        return array;
    }
    public void Print()
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write("Index = {0}  ", i);
            foreach (Item<K, T> elem in lists[i])
                Console.Write("{0} \t", elem);
            Console.WriteLine();
        }
    }
    public void Resize(int newsize)
    {
        Item<K, T>[] copy = this.ToArray();
        this.size = newsize;
        lists = new List<Item<K, T>>[size];
        for (int i = 0; i < size; i++)
            lists[i] = new List<Item<K, T>>();
        count = 0;
        foreach (var elem in copy) this.Add(elem.Key, elem.Value);
    }
}