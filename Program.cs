using System;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Cursova
{
    public static class Program
    {
        #region Greedy Method

        /// <summary>
        /// Жадібний метод вирішення проблеми комівояжера, приймає матрицю відстаней між містами та повертає масив, що представляє шлях комівояжера.
        /// </summary>
        public static int[] SolveGreedy(double[,] dist)
        {
            int n = dist.GetLength(0);
            var edges = new List<Edge>(n * (n - 1) / 2);
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                    edges.Add(new Edge(i, j, dist[i, j]));
            edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

            var uf = new UnionFind(n);
            int[] degree = new int[n];
            var adj = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();
            int chosen = 0;

            foreach (var e in edges)
            {
                bool wouldCloseHamiltonian = uf.Find(e.U) == uf.Find(e.V) && chosen == n - 1;
                if (degree[e.U] < 2 && degree[e.V] < 2 && (uf.Find(e.U) != uf.Find(e.V) || wouldCloseHamiltonian))
                {
                    adj[e.U].Add(e.V);
                    adj[e.V].Add(e.U);
                    degree[e.U]++;
                    degree[e.V]++;
                    uf.Union(e.U, e.V);
                    chosen++;
                    if (chosen == n) break;
                }
            }

            var tour = new List<int>(n + 1);
            int current = 0, previous = -1;
            for (int i = 0; i < n; i++)
            {
                tour.Add(current);
                int next = adj[current].First(x => x != previous);
                previous = current;
                current = next;
            }
            tour.Add(tour[0]);
            return tour.ToArray();
        }
        #endregion

        #region Nearest Neighbour Method

        /// <summary>
        /// вирішення проблеми комівояжера, методом найближчого сусіда, приймає матрицю відстаней між містами та повертає масив, що представляє шлях комівояжера.
        /// </summary>
        public static int[] SolveNearestNeighbor(double[,] dist, int start = 0)
        {
            int n = dist.GetLength(0);
            var unvisited = new List<int>(Enumerable.Range(0, n));
            unvisited.Remove(start);

            var tour = new List<int>(n + 1) { start };
            int current = start;

            while (unvisited.Count > 0)
            {
                int next = -1;
                double best = double.MaxValue;
                foreach (var v in unvisited)
                {
                    if (dist[current, v] < best)
                    {
                        best = dist[current, v];
                        next = v;
                    }
                }

                tour.Add(next);
                unvisited.Remove(next);
                current = next;
            }

            tour.Add(start);
            return tour.ToArray();
        }
        #endregion

        #region Simulated Annealing Method

        /// <summary>
        /// вирішення проблеми комівояжера методом локального пошуку з емуляцією відпалу, на основі методу найближчого сусіда,
        /// приймає матрицю відстаней між містами та повертає масив, що представляє шлях комівояжера.
        /// </summary>
        public static int[] SolveSimulatedAnnealing(
            double[,] dist,
            double initialTemp = 1000,
            double coolingRate = 0.995,
            int iterPerTemp = 100)
        {
            int n = dist.GetLength(0);
            var rand = new Random();
            int[] bestTour = SolveNearestNeighbor(dist);
            double bestCost = CalculateCost(bestTour, dist);
            int[] currentTour = (int[])bestTour.Clone();
            double currentCost = bestCost;
            double T = initialTemp;

            while (T > 1e-3)
            {
                for (int i = 0; i < iterPerTemp; i++)
                {
                    int[] candidate = TwoOptSwap(currentTour, rand);
                    double candidateCost = CalculateCost(candidate, dist);
                    double delta = candidateCost - currentCost;
                    if (delta < 0 || rand.NextDouble() < Math.Exp(-delta / T))
                    {
                        currentTour = candidate;
                        currentCost = candidateCost;
                        if (currentCost < bestCost)
                        {
                            bestCost = currentCost;
                            bestTour = (int[])currentTour.Clone();
                        }
                    }
                }
                T *= coolingRate;
            }
            return bestTour;
        }

        /// <summary>
        /// Метод для генерації випадкових векторів у діапазоні (0, 600), приймає кількість векторів та повертає список векторів.
        /// </summary>

        public static List<Vector2D> GenerateVectors(int count)
        {
            var list = new List<Vector2D>(count);
            var rand = new Random();
            for (int i = 0; i < count; i++)
            {
                double x = rand.NextDouble() * 600.0;
                double y = rand.NextDouble() * 600.0;
                list.Add(new Vector2D(x, y));
            }
            return list;
        }

        /// <summary>
        /// Метод що дозволяє випадковим чином модифікувати шлях. Використовується в методі Simulated Annealing. Приймає масив, що представляє шлях комівояжера та випадковий генератор,
        /// повертає новий масив, що представляє змінений шлях. Він має приймати випадковий генератор, бо в інакшому випадку генерувалися б одакові числа, через малу ріщницю в часі між викликами методу.
        /// </summary>

        private static int[] TwoOptSwap(int[] tour, Random rand)
        {
            int n = tour.Length - 1;
            int i = rand.Next(1, n - 1);
            int j = rand.Next(i + 1, n);
            var newTour = (int[])tour.Clone();
            Array.Reverse(newTour, i, j - i + 1);
            return newTour;
        }

        /// <summary>
        /// Метод що рахує довжину шляху, приймає масив, що представляє шлях комівояжера та матрицю відстаней між містами, повертає довжину шляху.
        /// </summary>

        public static double CalculateCost(int[] tour, double[,] dist)
        {
            double cost = 0;
            for (int i = 0; i < tour.Length - 1; i++)
                cost += dist[tour[i], tour[i + 1]];
            return cost;
        }
        #endregion

        #region Types

        /// <summary>
        /// структура, яка представляє ребро графа, що складається з двох вершин та ваги ребра. Потрібно для жадібного методу.
        /// </summary>
        private struct Edge
        {
            public readonly int U;
            public readonly int V;
            public readonly double Weight;

            public Edge(int u, int v, double w)
            {
                U = u;
                V = v;
                Weight = w;
            }
        }

        /// <summary>
        /// Клас, що допомагає перевіряти, чи шлях містить цикли, використовується в жадібному методі.
        /// </summary>

        private class UnionFind
        {
            private int[] parent;
            /// <summary>
            /// Створює новий екземпляр класу UnionFind з n елементами, кожен з яких спочатку є власним батьком.
            /// </summary>
            public UnionFind(int n)
            {
                parent = new int[n];
                for (int i = 0; i < n; i++) parent[i] = i;
            }
            /// <summary>
            /// допомагає знайти справжнього батька елемента х
            /// </summary>
            public int Find(int x)
            {
                if (parent[x] != x)
                    parent[x] = Find(parent[x]);
                return parent[x];
            }
            /// <summary>
            /// метод, що об'єднує два елементи a та b, якщо вони не мають спільного батька, встановлює батьком b батька a.
            /// </summary>
            public void Union(int a, int b)
            {
                int pa = Find(a), pb = Find(b);
                if (pa != pb) parent[pb] = pa;
            }
        }
        /// <summary>
        /// клас, що представляє двовимірний вектор, використовується для представлення координат міст у візульному варіанті задачі комівояжера.
        /// </summary>
        public class Vector2D
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Vector2D(double x, double y)
            {
                X = x;
                Y = y;
            }
            /// <summary>
            /// метод, що обчислює відстань між двома векторами, використовується для обчислення відстані між містами.
            /// </summary>
            public static double Distance(Vector2D a, Vector2D b)
            {
                double dx = a.X - b.X;
                double dy = a.Y - b.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }
        }
        #endregion

        #region File Reader
        /// <summary>
        /// Приймає масив рядків, що представляють матрицю, та повертає двовимірний масив, що представляє цю матрицю.
        /// </summary>
        public static double[,] ReadSquareMatrix(string[] lines)
        {
            int n = lines.Length - 1;
            var mat = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                var parts = lines[i+1].Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != n)
                {
                    Form.showWarning($"Matrix is not square, which is not allowed line ({i})");
                    return new double[0,0];
                }
                for (int j = 0; j < n; j++)
                {
                    double num;
                    try
                    {
                        num = double.Parse(parts[j]);
                    }
                    catch (FormatException)
                    {
                        Form.showWarning($"Invalid number format in matrix ({i}, {j})");
                        return new double[0, 0];
                    }
                    if (num < 0.0)
                    {
                        Form.showWarning($"Matrix contains negative numbers, which is not allowed ({i}, {j})");
                        return new double[0, 0];
                    }
                    mat[i, j] = num;
                }
            }
            return mat;
        }
        #endregion

        #region Read Vectors
        /// <summary>
        /// Приймає масив рядків, що представляють список векторів, та повертає двовимірний масив, що представляє цей список векторів.
        /// </summary>
        public static List<Vector2D> ReadVectors(string[] lines)
        {
            var result = new List<Vector2D>();
            int n = lines.Length - 1;
            for (int i = 0; i < n; i++)
            {
                var parts = lines[i+1].Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    Form.showWarning($"File does not contains valid 2d vector representation ({i})");
                    return new List<Vector2D>();
                }
                try
                {
                    double x = double.Parse(parts[0]);
                    double y = double.Parse(parts[1]);
                    if(x < 0.0 || y < 0.0 || x > 600.0 || y > 600.0)
                    {
                        Form.showWarning($"numbers should be range (0;600), line: ({i})");
                        return new List<Vector2D>();
                    }
                    result.Add(new Vector2D(x, y));
                }
                catch (FormatException)
                {
                    Form.showWarning($"Invalid number format in list ({i})");
                    return new List<Vector2D>();
                }
            }
            return result;
        }
        #endregion

        #region Create Distance Matrix
        /// <summary>
        /// Створює матрицю відстаней на основі векторів, приймає список векторів та повертає двовимірний масив, що представляє матрицю відстаней.
        /// </summary>
        public static double[,] CreateDistanceMatrix(List<Vector2D> vectors)
        {
            int n = vectors.Count;
            var dist = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                dist[i, i] = 0;
                for (int j = i + 1; j < n; j++)
                {
                    double d = Vector2D.Distance(vectors[i], vectors[j]);
                    dist[i, j] = d;
                    dist[j, i] = d;
                }
            }
            return dist;
        }
        #endregion

        /// <summary>
        /// Запишує список векторів у файл, приймає шлях до файлу та список векторів, які потрібно записати.
        /// </summary>
        public static void WriteVectors(string path, List<Vector2D> points)
        {
            using var writer = new StreamWriter(path);
            writer.WriteLine("Points");
            foreach (var p in points)
            {
                string xs = p.X.ToString("0.##");
                string ys = p.Y.ToString("0.##");
                writer.WriteLine($"{xs} {ys}");
            }
        }

        #region Draw Points
        /// <summary>
        /// Виводить список точок на екран, приймає список векторів та PictureBox, в який потрібно вивести точки.
        /// </summary>
        public static void DrawPoints(List<Vector2D> points, PictureBox pictureBox)
        {
            var bmp = new Bitmap(pictureBox.Width, pictureBox.Height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Beige);

                const int radius = 4;
                foreach (var p in points)
                {
                    float x = (float)p.X;
                    float y = (float)p.Y;

                    g.FillEllipse(Brushes.Black,
                                  x - radius, y - radius,
                                  radius * 2, radius * 2);
                }

            }

            pictureBox.Image?.Dispose();
            pictureBox.Image = bmp;
        }

        /// <summary>
        /// Малює рішення задачі комівояжера, приймає список векторів, масив порядку відвідування точок та PictureBox, в який потрібно вивести рішення.
        /// </summary>

        public static void DrawLines(List<Vector2D> points, int[] order, PictureBox pictureBox)
        {
            var bmp = new Bitmap(pictureBox.Width, pictureBox.Height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Beige);

                const int radius = 4;
                foreach (var p in points)
                {
                    float x = (float)p.X;
                    float y = (float)p.Y;

                    g.FillEllipse(Brushes.Black,
                                  x - radius, y - radius,
                                  radius * 2, radius * 2);
                }
                using (var pen = new Pen(Color.Blue, 2))
                {
                    for (int i = 0; i < order.Length - 1; i++)
                    {
                        var a = points[order[i]];
                        var b = points[order[i + 1]];
                        g.DrawLine(pen,
                                   (float)a.X, (float)a.Y,
                                   (float)b.X, (float)b.Y);
                    }
                }

            }

            pictureBox.Image?.Dispose();
            pictureBox.Image = bmp;
        }
        #endregion

        /// <summary>
        /// Записує результати вирішення задачі комівояжера у текстове поле, приймає масив з результатами та TextBox, в який потрібно вивести результати.
        /// </summary>
        public static void outputRes(int[] res, TextBox textbox)
        {
            if (res == null || res.Length == 0)
            {
                textbox.Text = "";
                return;
            }
            string output = "";
            for (int i = 0; i < res.Length; i++)
            {
                output += ($"{res[i]} ");
            }
            textbox.Text = output;
        }

        /// <summary>
        /// Запускає програму та зберігає головну форму у статичному полі Form, щоб мати до неї доступ з інших класів.
        /// </summary>

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Form = new Form1();
            Application.Run(Form);
        }
        public static Form1 Form;
        
    }
}