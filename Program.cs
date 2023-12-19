
using System;
using System.IO;
using System.Collections;
namespace Console_Godrok
{
    class Godrok
    {
        public static void Main(string[] args)
        {
            List<int> dephts = new();
            using (StreamReader sr = new("./melyseg.txt"))
            {
                while (!sr.EndOfStream)
                {
                    dephts.Add(int.Parse(sr.ReadLine()!));
                }
            }

            Console.WriteLine($" 1. feladat\n A fájl adatainak száma: {dephts.Count}\n");

            
            Console.Write("Adjon meg egy távolságértéket! ");
            int index = int.Parse(Console.ReadLine()!);
            Console.WriteLine($"\n2. feladat\nEzen a helyen a felszín {dephts[index - 1]} méter mélyen van.\n");

            int flat = dephts.Count(x => x == 0);
            Console.WriteLine($"3. feladat\nAz érintetlen terület aránya {100.0 * flat / dephts.Count:0.00}%.\n");

            //4. feladat
            List<string> egysor = new List<string>();
            List<List<string>> sorok = new List<List<string>>();

            using (StreamWriter sw = new StreamWriter("godrok.txt"))
            {
                int prev = 0;


                foreach (int depth in dephts)
                {
                    if (depth > 0)
                    {
                        egysor.Add(depth.ToString());
                    }

                    if (depth == 0 && prev > 0)
                    {
                        sorok.Add(new List<string>(egysor));
                        egysor.Clear();
                    }

                    prev = depth;
                }

                sorok.ForEach(x => sw.WriteLine(string.Join(" ", x)));
            }

            Console.WriteLine($"5. feladat\nA gödrök száma: {sorok.Count}\n");

            Console.WriteLine("6. feladat");
            if (dephts[index - 1] > 0)
            {
                int pos = index - 1;
                while (dephts[pos] > 0)
                {
                    pos--;
                }
                int start = pos + 2;
                pos = index;
                while (dephts[pos] > 0)
                {
                    pos++;
                }
                int end = pos;
                Console.WriteLine($"a)\nA gödör kezdete: {start} méter, a gödör vége: {end} méter.");

                pos = start;
                while (dephts[pos] >= dephts[pos - 1] && pos <= end)
                {
                    pos++;
                }
                while (dephts[pos] <= dephts[pos - 1] && pos <= end)
                {
                    pos++;
                }
                if (pos > end)
                {
                    Console.WriteLine("b)\nFolyamatosan mélyül.");
                }
                else
                {
                    Console.WriteLine("b)\nNem mélyül folyamatosan.");
                }

                Console.WriteLine($"c)\nA legnagyobb mélysége {dephts.Skip(start - 1).Take(end - start + 1).Max()} méter.");

                double volume = 10 * dephts.Skip(start - 1).Take(end - start + 1).Sum();
                Console.WriteLine($"d)\nA térfogata {volume} m^3.");

                double result = volume - 10 * (end - start + 1);
                Console.WriteLine($"e)\nA vízmennyiség {result} m^3.");
            }
            else
            {
                Console.WriteLine("Az adott helyen nincs gödör.");
            }
        }
    }
}