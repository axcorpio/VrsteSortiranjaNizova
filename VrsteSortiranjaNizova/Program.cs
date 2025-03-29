using System;
using System.Collections.Generic;
using System.Linq;

namespace VrsteSortiranjaNizova
{
    internal class Program
    {
        static int iteracije = 0;

        static void Main()
        {
            int[] niz = { 8, 17, 3, 3, 5, 10, 19, 12, 1, 6 };

            IspisivanjeNiza("1. Bubble sort", BubbleSort(niz));
            IspisivanjeNiza("2. Quick sort", QuickSort(niz));
            IspisivanjeNiza("3. Random quick sort", RandomQuickSort(niz, 0, niz.Length - 1));
            IspisivanjeNiza("4. Merge sort", MergeSort(niz));
            IspisivanjeNiza("5. Selection sort", SelectionSort(niz));
            IspisivanjeNiza("6. Counting sort", CountingSort(niz));

            Console.ReadKey();
        }

        static void IspisivanjeNiza(string naslov, int[] niz)
        {
            Console.WriteLine(naslov);
            Console.WriteLine("Sortiran niz: [" + string.Join(", ", niz) + "]");
            Console.WriteLine("Broj iteracija: " + iteracije); 
            Console.WriteLine();

            iteracije = 0;
        }   

        static int[] BubbleSort(int[] niz)
        {
            for (int i = 0; i < niz.Length; i++)
            {
                for (int j = 0; j < niz.Length - 1; j++)
                {
                    iteracije++;

                    if (niz[j] > niz[j + 1])
                    {
                        (niz[j + 1], niz[j]) = (niz[j], niz[j + 1]);
                    }
                }   


            }

            return niz;
        }

        static int[] QuickSort(int[] niz)
        {
            if (niz.Length > 1)
            {
                int prvi = niz[0];

                List<int> leviDeo = new List<int>();
                List<int> desniDeo = new List<int>();

                for (int i = 1; i < niz.Length; i++)
                {
                    iteracije++;

                    if (niz[i] < prvi)
                    {
                        leviDeo.Add(niz[i]);
                    }
                    else
                    {
                        desniDeo.Add(niz[i]);
                    }
                }

                int[] leviDeoKaoNiz = QuickSort(leviDeo.ToArray());
                int[] desniDeoKaoNiz = QuickSort(desniDeo.ToArray());

                return leviDeoKaoNiz.Concat(new int[] { prvi }).Concat(desniDeoKaoNiz).ToArray();
            }

            return niz;
        }

        static int[] RandomQuickSort(int[] niz, int levo, int desno)
        {
            if (levo < desno)
            {
                Random random = new Random();

                int pivotIndeks = random.Next(levo, desno + 1);

                (niz[pivotIndeks], niz[desno]) = (niz[desno], niz[pivotIndeks]);

                int pivot = niz[desno];
                int i = levo - 1;

                for (int j = levo; j < desno; j++)
                {
                    iteracije++;

                    if (niz[j] <= pivot)
                    {
                        i++;

                        (niz[i], niz[j]) = (niz[j], niz[i]);
                    }
                }

                (niz[i + 1], niz[desno]) = (niz[desno], niz[i + 1]);
                
                int pivotNoviIndeks = i + 1;

                RandomQuickSort(niz, levo, pivotNoviIndeks - 1);
                RandomQuickSort(niz, pivotNoviIndeks + 1, desno);
            }

            return niz;
        }

        static int[] MergeSort(int[] niz)
        {
            if (niz.Length <= 1) return niz;

            int sredina = niz.Length / 2;
            int[] leviDeo = new int[sredina];
            int[] desniDeo = new int[niz.Length - sredina];

            Array.Copy(niz, 0, leviDeo, 0, sredina);
            Array.Copy(niz, sredina, desniDeo, 0, niz.Length - sredina);

            MergeSort(leviDeo);
            MergeSort(desniDeo);

            int i = 0, j = 0, k = 0;

            while (i < leviDeo.Length && j < desniDeo.Length)
            {
                iteracije++;

                if (leviDeo[i] <= desniDeo[j])
                {
                    niz[k++] = leviDeo[i++];
                }
                else
                {
                    niz[k++] = desniDeo[j++];
                } 
            }

            while (i < leviDeo.Length)
            {
                iteracije++;

                niz[k++] = leviDeo[i++];
            }

            while (j < desniDeo.Length)
            {
                iteracije++;

                niz[k++] = desniDeo[j++];
            }

            return niz;
        }

        static int[] SelectionSort(int[] niz)
        {
            int n = niz.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < n; j++)
                {
                    iteracije++;

                    if (niz[j] < niz[min])
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    (niz[min], niz[i]) = (niz[i], niz[min]);
                }
            }

            return niz;
        }

        static int[] CountingSort(int[] niz)
        {
            if (niz.Length == 0) return niz;

            int min = niz.Min();
            int max = niz.Max();

            int range = max - min + 1;
            int[] brojac = new int[range];
            int[] rezultat = new int[niz.Length];

            foreach (int num in niz)
            {
                iteracije++;

                brojac[num - min]++;
            }

            for (int i = 1; i < range; i++)
            {
                iteracije++;

                brojac[i] += brojac[i - 1];
            }

            for (int i = niz.Length - 1; i >= 0; i--)
            {
                iteracije++;

                rezultat[brojac[niz[i] - min] - 1] = niz[i];
                brojac[niz[i] - min]--;
            }

            return rezultat;
        }
    }
}