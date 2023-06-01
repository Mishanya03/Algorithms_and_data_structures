using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;


class Set<T> where T : IComparable
{
    int size;
    int n;
    T[] data;
    bool done;
    //   int[] v;
    public Set(int n)
    {
        this.n = 0;
        size = 0;
        if (n > 0)
        {
            data = new T[n];
            this.n = n;
        }
    }
    public int GetSize() { return n; }
    public int GetCount() { return size; }
    public int Count { get { return size; } } // Количество элементов в множестве

    public int GetIndex(T value)
    {
        int i;
        int select = -1;
        for (i = 0; i < size; i++)
            // if (data[i] == value) { select = i; break; }
            if (data[i].Equals(value)) { select = i; break; }
        return select;
    }
    public bool IsContains(T element)
    {
        for (int i = 0; i < size; i++) if (data[i].CompareTo(element) == 0) return true;
        return false;
    }

    public T this[int index]
    {
        get { if (index >= 0 && index < size) return data[index]; else return default(T); }
    }
    public bool Exists(T value)
    {
        if (GetIndex(value) >= 0) return true;
        else return false;
    }
    public void Add(T value)
    {
        int i;
        int select = GetIndex(value);
        if (select == -1)
        {
            if (size < n) { data[size] = value; size++; }
            else
            {
                T[] temp = new T[n];
                for (i = 0; i < n; i++) temp[i] = data[i];
                data = new T[n * 2];
                for (i = 0; i < n; i++) data[i] = temp[i];
                n = 2 * n;
                data[size] = value; size++;

            }
        }
    }
    public void RemoveAt(int index)
    {
        if (index >= 0 && index < size)
        {
            int i = 0;
            for (i = index; i < size - 1; i++)
                data[i] = data[i + 1];
            data[size] = default(T); size--;
        }
    }
    public void Remove(T value)
    {
        int index = GetIndex(value);
        RemoveAt(index);
    }
    public T GetValue(int index)
    {
        if (index >= 0 && index < size) return data[index];
        return default(T);
    }
    public override string ToString()
    {
        string ss = "{";
        int i;
        for (i = 0; i < size; i++)
            if (i < (size - 1)) ss = ss + string.Format("{0},", data[i]);
            else ss = ss + string.Format("{0}", data[i]);
        ss = ss + "}";

        return ss;
    }
    //public override string ToString()
    //{
    //    T[] temp = new T[count];
    //    for (int i = 0; i < count; i++) temp[i] = data[i];
    //    return "{" + string.Join(";", temp) + "}";
    //}

    public Set<T> Union(Set<T> ss)
    {
        int nn = this.n + ss.GetSize();
        Set<T> rez = new Set<T>(nn);
        int i;
        for (i = 0; i < size; i++)
            rez.Add(data[i]);
        for (i = 0; i < ss.GetCount(); i++)
            rez.Add(ss.GetValue(i));
        return rez;
    }
    public Set<T> Intersection(Set<T> ss)
    {
        int nn = this.n;
        Set<T> rez = new Set<T>(nn);
        int i;
        for (i = 0; i < ss.size; i++)
            if (this.IsContains(ss[i]))
            {
                rez.Add(ss[i]);
            }
        return rez;
    }
    public Set<T> Addition(Set<T> ss)
    {
        int nn = this.n + ss.size;
        Set<T> rez = new Set<T>(nn);
        int i;
        for (i = 0; i < ss.size; i++)
            if (!this.IsContains(ss[i]))
            {
                rez.Add(ss[i]);
            }
        return rez;
    }
    public static Set<T> operator +(Set<T> s1, Set<T> s2) => s1.Union(s2);
    public static Set<T> operator *(Set<T> s1, Set<T> s2) => s1.Intersection(s2);
    public static Set<T> operator -(Set<T> s1, Set<T> s2) => s2.Addition(s1);
    //Множество всех множеств
    public List<Set<T>> subSets()
    {
        List<Set<T>> LL = new List<Set<T>>();
        for (int i = 1; i <= size; i++)
        {
            Set<T> L = new Set<T>(i);
            for (int j = 0; j < i; j++)
            {
                L.Add(this.data[j]);
            }
            LL.Add(L);
        }
        return LL;
    }
    //Все перестановки множества
    public List<Set<T>> Permutations() 
    {
        int[] nums = new int[this.size];
        int maxPermutatuons = Factorial(this.size);
        for (int i = 0; i < this.size; i++)
            nums[i] = i;
        List<List<int>> listIndex = Permute(nums);
        List<Set<T>> result = new List<Set<T>>(maxPermutatuons);
        
        for (int j = 0; j < maxPermutatuons; j++) 
        {
            Set<T> set = new Set<T>(this.size);
            foreach (int index in listIndex[j])
                set.Add(this.data[index]);
            result.Add(set);
        }
        return result;
    }
    //Список всех перестановок индексов
    private static List<List<int>> Permute(int[] nums)
    {
        List<List<int>> result = new List<List<int>>();
        // Обрабатываем пустой массив
        if (nums.Length == 0)
        {
            result.Add(new List<int>());
            return result;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            // Получаем элемент
            int currentNum = nums[i];

            // Создаем массив без текущего элемента
            int[] remainingNums = new int[nums.Length - 1];
            for (int j = 0; j < i; j++)
            {
                remainingNums[j] = nums[j];
            }

            for (int j = i + 1; j < nums.Length; j++)
            {
                remainingNums[j - 1] = nums[j];
            }

            // Рекурсивно получаем все перестановки элементов
            List<List<int>> remainingPermutations = Permute(remainingNums);

            // Добавляем текущий элемент к каждой перестановке
            foreach (List<int> remainingPermutation in remainingPermutations)
            {
                remainingPermutation.Insert(0, currentNum);
            }
            // Добавляем все перестановки в результат
            result.AddRange(remainingPermutations);
        }
        return result;
    }
    //Факториал числа n
    private int Factorial(int n)
    {
        int factorial = 1;
        if (n != 0)
            for (int i = 1; i <= n; i++)
                factorial *= i;
        return factorial;
    }
    //Все перестановки множества2 
    public List<Set<T>> Permutations2()
    {
        int n = this.size;
        int factorial = Factorial(n);
        List<int> listIndex = new List<int>(n);
        List<Set<T>> list = new List<Set<T>>(factorial);
        for (int i = n; i > 0; i--)
            listIndex.Add(i);
        for (int i = 0; i < factorial; i++)
        {
            Set<T> set = new Set<T>(n);
            foreach (int index in listIndex)
                set.Add(this.data[index - 1]);
            list.Add(set);
            GetListIndex(listIndex, n);
        }
        return list;
    }
    private List<int> GetListIndex(List<int> listIndex, int n)
    {
        int pastNum = listIndex[n - 1];
        listIndex[n - 1] -= 1;
        if (listIndex[n - 1] == 0) listIndex[n - 1] = n;
        for (int i = 0; i < n - 1; i++)
            if (listIndex[i] == listIndex[n - 1])
                listIndex[i] = pastNum;
        return listIndex;
    }
    //Сочетание
    public List<Set<T>> Combinations(int m)
    {
        List<Set<T>> LL = new List<Set<T>>();
        int[] listIndex = new int[size];

        for (int i = 0; i < size; i++)
        {
            listIndex[i] = i + 1;
        }
        if (size >= m)
        {
            Set<T> rez = new Set<T>(size);

            for (int i = 0; i < m; i++)
            {
                rez.Add(data[listIndex[i] - 1]);
            }
            LL.Add(rez);
            while (CombinationNext(listIndex, m))
            {
                Set<T> rez1 = new Set<T>(size);
                for (int i = 0; i < m; i++)
                {
                    rez1.Add(data[listIndex[i] - 1]);
                }
                LL.Add(rez1);
            }
        }
        return LL;
    }
    public bool CombinationNext(int[] listIndex, int m)
    {
        int k = m;
        for (int i = k - 1; i >= 0; i--)
        {
            if (listIndex[i] < size - k + i + 1)
            {
                listIndex[i]++;
                for (int j = i + 1; j < k; j++)
                {
                    listIndex[j] = listIndex[j - 1] + 1;
                }
                return true;
            }

        }
        return false;
    }  
}