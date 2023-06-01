using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

class Sort
{
    //Сортировка пузырьком
    public static void BubbleSort<T>(T[] list) where T : IComparable
    {
        int N = list.Length;
        int numberOfPairs = N;
        bool swappedElements = true;
        while (swappedElements)
        {
            numberOfPairs--;  // numberOfPairs = numberOfPairs - l;
            swappedElements = false;
            for (int i = 0; i < numberOfPairs; i++)
            {
                if (list[i].CompareTo(list[i + 1]) > 0)   // if(list[i]>list[i+1])
                {
                    T tmp; tmp = list[i]; list[i] = list[i + 1]; list[i + 1] = tmp; //Swap(list[i],list[i+1])
                    swappedElements = true;
                }
            }
        }
    }
    //Сортировка - Поиск максимального элемента
    public static void SearchMaxElement<T>(T[] list) where T : IComparable
    {
        int N = list.Length;
        int numberOfPairs = N;
        for (int k = 0; k < N; k++)
        {
            numberOfPairs--;  // numberOfPairs = numberOfPairs - l;
            var maxElement = list[0];
            int maxElementIndex = 0;
            for (int i = numberOfPairs; i > 0; i--)
            {
                if (maxElement.CompareTo(list[i]) < 0)
                {
                    maxElement = list[i];
                    maxElementIndex = i;
                }
            }

            if (maxElementIndex != numberOfPairs)
            {
                var tmp = list[numberOfPairs];
                list[numberOfPairs] = maxElement;
                list[maxElementIndex] = tmp;
            }
        }
    }
    //Сортировка методом вставки
    public static void InsertionSort<T>(T[] list) where T : IComparable
    {
        for (int i = 1; i < list.Length; i++)
        {
            var tmp = list[i];
            int j = i - 1;
            while (j >= 0 && tmp.CompareTo(list[j]) < 0)
            {
                list[j + 1] = list[j];
                j--;
            }
            list[j + 1] = tmp;
        }
    }
    //Сортировка Shell
    public static void ShellSort<T>(T[] list) where T : IComparable
    {
        int step = list.Length / 2;
        while (step > 0)
        {
            for (int i = 0; i < (list.Length - step); i++)
            {
                int j = i;
                while (j >= 0 && list[j].CompareTo(list[j + step]) > 0)
                {
                    var tmp = list[j];
                    list[j] = list[j + step];
                    list[j + step] = tmp;
                    j--;
                }
            }
            step /= 2;
        }
    }
    //Сортировка Radix
    public static void RadixSort(int[] list)
    {
        int l = RankCheck(list);
        bool checkIf = false;
        for (int i = 0; i < list.Length; i++)
            if (list[i] < 0) { checkIf = true; }

        if (checkIf == false)
        {   
            int del = 1; //Делитель для определения цифры в числе
            for (int n = 1; n <= l; n++)
            {
                List<int>[] buckets = new List<int>[10];
                for (int i = 0; i < 10; i++)
                {
                    buckets[i] = new List<int>();
                    buckets[i].Clear();
                }
                
                for (int i = 0; i < list.Length; i++)
                {
                    //Определение текущей цифры
                    int indexBucket = list[i] / del; 
                    indexBucket %= 10;

                    buckets[indexBucket].Add(list[i]);
                }

                int k = 0;
                for (int i = 0; i < 10; i++)
                {
                   
                    foreach (int element in buckets[i])
                    {
                        list[k] = element;
                        k++;
                    }
                }
                del *= 10;
            }
        }
    }
    //Сортировка Counting
    public static void CountingSort(int[] list, int maxValue)
    {
        var size = list.Length;
        int[] array = new int[maxValue + 1];
        for (int i = 0; i < maxValue + 1; i++)
        {
            array[i] = 0;
        }
        for (int i = 0; i < size; i++)
        {
            array[list[i]]++;
        }

        for (int i = 0, j = 0; i <= maxValue; i++)
        {
            while (array[i] > 0)
            {
                list[j] = i;
                j++;
                array[i]--;
            }
        }
    }
    //Сортировка Radix версия с допущением отрицательных элементов
    public static void RadixSort2(int[] list)
    {
        int l = RankCheck(list); //Проверка максимальной разрядности
        bool checkIf = false;
        int delta = 0;
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] < 0 && list[i] < delta)
            {
                checkIf = true;
                delta = list[i];
            }
        }
        if (checkIf)
        {
            for (int i = 0; i < list.Length; i++)
                list[i] = list[i] - delta;
        }
        List<int> tmp = new List<int>(list.Length);
        int del = 1; //Делитель для определения цифры в числе
        for (int n = 1; n <= l; n++)
        {
            List<int>[] buckets = new List<int>[10];
            for (int i = 0; i < 10; i++)
            {
                buckets[i] = new List<int>();
                buckets[i].Clear();
            }

            for (int i = 0; i < list.Length; i++)
            {
                //Определение текущей цифры
                int indexBucket = list[i] / del;
                indexBucket %= 10;

                buckets[indexBucket].Add(list[i]);
            }

            int k = 0;
            for (int i = 0; i < 10; i++)
            {

                foreach (int element in buckets[i])
                {
                    list[k] = element;
                    k++;
                }
            }
            del *= 10;
        }
        for (int i = 0; i < list.Length; i++)
            list[i] = list[i] + delta;
    }
    //Проверка максимальной разрядности 
    private static int RankCheck(int[] list)
    {
        int maxElement = list.Max();
        int i = 0;
        while (maxElement > 0)
        {
            maxElement = maxElement / 10;
            i++;
        }
        return i;
    }


    //Сортировка слиянием
    public static void MergeSort<T>(T[] list, int left, int right) where T : IComparable
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            MergeSort<T>(list, left, middle);
            MergeSort<T>(list, middle + 1, right);
            MergeLists<T>(list, left, middle, right);
        }
    }
    //Сортировка слиянием без рекурсии
    public static void MergeSortNR<T>(T[] list) where T : IComparable
    {
        int n = list.Length;
        int middle;
        
        for (int currentSize = 1; currentSize <= n - 1; currentSize = 2 * currentSize)
        {
            for (int leftStart = 0; leftStart < n - 1; leftStart += 2 * currentSize)
            {
                middle = Math.Min(leftStart + currentSize - 1, n - 1);
                int rightEnd = Math.Min(leftStart + 2 * currentSize - 1, n - 1);
                MergeLists<T>(list, leftStart, middle, rightEnd);
            }
        }
    }

    private static void MergeLists<T>(T[] list, int left, int middle, int right) where T : IComparable
    {
        int lenghtLeft = middle - left + 1;
        T[] listLeft = new T[lenghtLeft];
        for (int n = 0; n < lenghtLeft; n++) { listLeft[n] = list[left + n]; }


        int lenghtRight = right - middle;
        T[] listRight = new T[lenghtRight];
        for (int n = 0; n < lenghtRight; n++) { listRight[n] = list[middle + 1 + n]; }


        int i = 0, j = 0, k = left;
        while (i < lenghtLeft &&  j < lenghtRight)
        {
            if (listLeft[i].CompareTo(listRight[j]) <= 0)
            {
                list[k] = listLeft[i];
                i++;
            }
            else
            {
                list[k] = listRight[j];
                j++;
            }
            k++;
        }

        while (i < lenghtLeft)
        {
            list[k] = listLeft[i];
            i++;
            k++;
        }
        while (j < lenghtRight)
        {
            list[k] = listRight[j];
            j++;
            k++;
        }
    }
    //Сортировка Quick Sort
    public static void QuickSort<T>(T[] list, int leftIndex, int rightIndex) where T : IComparable
    {
        var i = leftIndex;
        var j = rightIndex;
        var tmp = list[leftIndex];

        while (i <= j)
        {
            while (list[i].CompareTo(tmp) < 0)
            {
                i++;
            }

            while (list[j].CompareTo(tmp) > 0)
            {
                j--;
            }
            if (i <= j)
            {
                (list[i], list[j]) = (list[j], list[i]);
                i++;
                j--;
            }
        }

        if (leftIndex < j)
            QuickSort<T>(list, leftIndex, j);
        if (i < rightIndex)
            QuickSort<T>(list, i, rightIndex);
        
    }
}