using System;
using System.Collections.Generic;
using System.Linq;

namespace VrsteSortiranjaNizova
{
    internal class Program
    {
        static void Main()
        {
            int[] niz = { 5, 3, 3, 1, 2, 7, 4, 6 };

            Console.WriteLine("Izaberite vrstu sortiranja: ");
            Console.WriteLine("1. Bubble sort");
            Console.WriteLine("2. Quick sort");
            Console.WriteLine("3. Random quick sort");
            Console.WriteLine("4. Merge sort");
            Console.WriteLine("5. Selection sort");
            Console.WriteLine("6. Counting sort");
            Console.WriteLine();
            Console.Write("Vas izbor: ");

            int izbor = int.Parse(Console.ReadLine());

            bool validanUnos = true;

            Console.WriteLine();

            switch (izbor)
            {
                case 1:
                    niz = BubbleSort(niz);

                    break;
                case 2:
                    niz = QuickSort(niz);

                    break;
                case 3:
                    niz = RandomQuickSort(niz, 0, niz.Length - 1);

                    break;
                case 4:
                    niz = MergeSort(niz);

                    break;
                case 5:
                    niz = SelectionSort(niz);

                    break;
                case 6:
                    niz = CountingSort(niz);

                    break;
                default:
                    Console.WriteLine("Niste izabrali nijednu od ponudjenih opcija.");
                    Console.WriteLine();

                    validanUnos = false;

                    break;
            }

            if (validanUnos) IspisivanjeNiza(niz);
        }

        static void IspisivanjeNiza(int[] niz)
        {
            Console.WriteLine("Sortiran niz:");
            
            Console.Write("[");

            for (int i = 0; i < niz.Length; i++)
            {
                if (i == niz.Length - 1)
                {
                    Console.Write(niz[i]);
                }
                else
                {
                    Console.Write(niz[i] + ", ");
                }
            }

            Console.WriteLine("]");
            Console.WriteLine();
        }   

        static int[] BubbleSort(int[] niz)
        {
            for (int i = 0; i < niz.Length; i++)
            {
                for (int j = 0; j < niz.Length - 1; j++)
                {
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
                niz[k++] = leviDeo[i++];
            }

            while (j < desniDeo.Length)
            {
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
                brojac[num - min]++;
            }

            for (int i = 1; i < range; i++)
            {
                brojac[i] += brojac[i - 1];
            }

            for (int i = niz.Length - 1; i >= 0; i--)
            {
                rezultat[brojac[niz[i] - min] - 1] = niz[i];
                brojac[niz[i] - min]--;
            }

            return rezultat;
        }
    }
}