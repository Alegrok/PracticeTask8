using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PracticeTask8
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в приложение по нахождению блоков в графе по матрице инциденций!");

            // Ввод пути к файлу с графом
            Console.WriteLine("Ввод пути к файлу");
            Console.WriteLine("Пример пути файла: C:/Users/Aleksandr/source/repos/Practice8/PracticeTask8/Graph.txt");
            Console.Write("\nВведите путь к файлу: ");
            string path = Console.ReadLine();
            Console.WriteLine();

            // Чтение графа
            Graph graph = Graph.ReadGraph(path);

            // Если граф существует, то выделяем блоки графа
            if (graph != null)
            {
                graph.Block();
            }

            Console.WriteLine("\nЗавершение работы в приложении по нахождению блоков в графе по матрице инциденций");

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        public class Graph
        {
            int rows; // количество вершин графа
            int columns; // количество ребер 
            byte[,] incidenceMatrix; // граф задается матрицей инциденций
            int blocks; // количество блоков
            int checkedCount; // количество обследованных вершин
            int[] check; // порядковые номера обхода вершин
            Stack<int> edgesStack; // стек с номерами ребер, составляющими блок
            List<int> usedEdges; // список уже использованных ребер

            // Конструктор
            public Graph(int rows, int columns, byte[,] matrix)
            {
                this.rows = rows;
                this.columns = columns;
                incidenceMatrix = matrix;
                blocks = 0;
                checkedCount = 0;
                edgesStack = new Stack<int>(columns);
                check = new int[rows];
                for (int i = 0; i < rows; i++)
                {
                    check[i] = 0;
                }
                usedEdges = new List<int>(columns);
            }

            // Чтение графа из файла
            public static Graph ReadGraph(string path)
            {
                try
                {
                    FileStream file = new FileStream(path, FileMode.Open);
                    StreamReader sr = new StreamReader(file);

                    int size = int.Parse(sr.ReadLine());
                    int edges = int.Parse(sr.ReadLine());
                    byte[,] matrix = new byte[size, edges];

                    for (int i = 0; i < size; i++)
                    {
                        string vals = sr.ReadLine();
                        if (vals.Length > edges * 2 - 1)
                            vals = vals.Remove(edges * 2 - 1);
                        // Чтение строки матрицы
                        byte[] row = vals.Split(' ').Select(n => byte.Parse(n)).ToArray();
                        for (int j = 0; j < edges; j++)
                        {
                            if (row[j] != 0 && row[j] != 1)
                            {
                                throw new Exception();
                            }
                            matrix[i, j] = row[j];
                        }
                    }

                    sr.Close();
                    file.Close();

                    return new Graph(size, edges, matrix);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Не удается открыть файл, проверьте его наличие и правильность пути.");
                    return null;
                }
                catch
                {
                    Console.WriteLine("В файле содержатся некорректные данные.");
                    return null;
                }
            }

            // Выделение блоков графа методом поиска в глубину
            public void Block()
            {
                checkedCount = 0;
                DeepSearch(0, -1);
            }

            // Поиск в глубину
            public int DeepSearch(int pos, int parent)
            {
                check[pos] = ++checkedCount;
                int min = check[pos]; // минимальное расстояние от Pos до входа
                // Перебор всех ребер, входящих или исходящих из вершины Pos
                for (int edge = 0; edge < columns; edge++)
                {
                    if (incidenceMatrix[pos, edge] == 1)
                    {
                        int nextVerit = 0;
                        while (nextVerit < rows && incidenceMatrix[nextVerit, edge] == 0 || nextVerit == pos)
                            nextVerit++;
                        {
                            if (nextVerit != parent)
                            {
                                int t, curSize = edgesStack.Count;
                                // Если этого ребра еще нет в стеке
                                if (!usedEdges.Contains(edge))
                                {
                                    usedEdges.Add(edge);
                                    edgesStack.Push(edge);
                                }

                                //Если вершина еще не посещена
                                if (check[nextVerit] == 0)
                                {
                                    // Продолжаем обход из этой вершины
                                    t = DeepSearch(nextVerit, pos);
                                    if (t >= check[pos])
                                    {
                                        Console.Write("Блоку {0} принадлежат ребра: ", ++blocks);
                                        while (edgesStack.Count != curSize)
                                        {
                                            Console.Write("{0}, ", edgesStack.Pop());
                                        }
                                        Console.WriteLine();
                                    }
                                }
                                else
                                    t = check[nextVerit];
                                min = Math.Min(min, t);
                            }
                        }
                    }
                }
                return min;
            }

            public override string ToString()
            {
                return rows + " " + columns + " " + incidenceMatrix.ToString();
            }
        }
    }
}
