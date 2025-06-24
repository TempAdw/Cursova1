using static Cursova.Program;
using System.Windows.Forms;
using System.Diagnostics;

namespace Cursova
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// VisualMode - відповідає за те, чи працює програма в режимі візуалізації (додавання/видалення точок)
        /// IsAddingPoint - вказує, чи активний режим додавання точок
        /// IsDeletePoint - вказує, чи активний режим видалення точок
        /// dist - матриця відстаней між точками
        /// cost - вартість маршруту, розрахована на основі розв'язку
        /// Points - список точок, які представляють граф у візуальному режимі
        /// Res - масив, що зберігає маршрут, розрахований алгоритмами вирішення задачі комівояжера
        /// </summary>
        private bool VisualMode = true;
        private bool IsAddingPoint = false;
        private bool IsDeletePoint = false;
        double[,] dist = new double[0, 0];
        double cost = 0.0;
        public static List<Vector2D> _points = new List<Vector2D>();
        private int[] _res = new int[0];

        public List<Vector2D> Points
        {
            get => _points;
            set
            {
                _points = value;
                Res = [];
            }
        }
        public int[] Res
        {
            get => _res;
            set
            {
                _res = value;
                outputRes(_res, Program.Form.TextBoxReuslt);
                cost = CalculateCost(_res, dist);
                Program.Form.TextBoxReuslt2.Text = $"{cost}";
            }
        }
        /// <summary>
        /// Запускає вікно програми та ініціалізує компоненти.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метоод для виведення помилок та попереджень у вигляді повідомлень замість винятків у консоль.
        /// </summary>
        public void showWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Метод, який викликається при натисканні на кнопку видалення точок.
        /// </summary>
        private void buttonDeletePoint_Click(object sender, EventArgs e)
        {
            if (!VisualMode)
            {
                MessageBox.Show("You are not in visual mode. Please load point based graph or clear the canvas", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IsDeletePoint = !IsDeletePoint;
            if (IsDeletePoint)
            {
                IsAddingPoint = false;
                ButtonAddPoint.UseVisualStyleBackColor = true;
                ButtonDeletePoint.UseVisualStyleBackColor = false;
                Program.Form.pictureBox1.Cursor = Cursors.Cross;
            }
            else
            {
                ButtonDeletePoint.UseVisualStyleBackColor = true;
                Program.Form.pictureBox1.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Метод, який викликається при натисканні на кнопку додавання точок.
        /// </summary>
        private void buttonAddPoint_Click(object sender, EventArgs e)
        {
            if (!VisualMode)
            {
                MessageBox.Show("You are not in visual mode. Please load point based graph or clear the canvas", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IsAddingPoint = !IsAddingPoint;
            if (IsAddingPoint)
            {
                IsDeletePoint = false;
                ButtonDeletePoint.UseVisualStyleBackColor = true;
                ButtonAddPoint.UseVisualStyleBackColor = false;
                Program.Form.pictureBox1.Cursor = Cursors.Cross;
            }
            else
            {
                ButtonAddPoint.UseVisualStyleBackColor = true;
                Program.Form.pictureBox1.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Метод, який викликається при натисканні на завантаження збереженого графа. Завантажує матрицю відстаней або список точок з файлу, в залежності від формату збереження.
        /// </summary>

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string saveFilePath = "save.txt";
            if (!File.Exists(saveFilePath))
            {
                MessageBox.Show("File doesn't exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var lines = File.ReadAllLines(saveFilePath)
                            .Where(l => !string.IsNullOrWhiteSpace(l))
                            .ToArray();
            if (lines[0].Trim() == "Matrix")
            {
                VisualMode = false;
                Points.Clear();
                Res = [];
                dist = ReadSquareMatrix(lines);

                IsAddingPoint = false;
                ButtonAddPoint.UseVisualStyleBackColor = true;
                IsDeletePoint = false;
                ButtonDeletePoint.UseVisualStyleBackColor = true;
                Program.Form.pictureBox1.Cursor = Cursors.Default;
            }
            else if (lines[0].Trim() == "Points")
            {
                VisualMode = true;
                Points = ReadVectors(lines);
                dist = CreateDistanceMatrix(Points);
            }
            else
            {
                MessageBox.Show("Invalid file format. Program supports only Points and Matrix format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DrawPoints(Points, Program.Form.pictureBox1);
        }

        /// <summary>
        /// Метод, що викликається при натисканні на кнопку вирішення задачі комівояжера за допомогою жадібного алгоритму.
        /// </summary>
        private void buttonSolveGreedy(object sender, EventArgs e)
        {
            if (dist.Length < 5)
            {
                MessageBox.Show("Please, load poper graph first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            DrawPoints(Points, Program.Form.pictureBox1);
            Res = SolveGreedy(dist);
            stopwatch.Stop();
            string logMessage = $"[{DateTime.Now}] Execution Time: {stopwatch.ElapsedMilliseconds} ms";
            string logPath = "log.txt";
            File.AppendAllText(logPath, logMessage + Environment.NewLine);
        }

        /// <summary>
        /// Метод, що викликається при натисканні на кнопку вирішення задачі комівояжера за допомогою методу найближчого сусіда.
        /// </summary>
        private void buttonSolveNearest(object sender, EventArgs e)
        {
            if (dist.Length < 5)
            {
                MessageBox.Show("Please, load poper graph first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            DrawPoints(Points, Program.Form.pictureBox1);
            Res = SolveNearestNeighbor(dist);
            stopwatch.Stop();
            string logMessage = $"[{DateTime.Now}] Execution Time: {stopwatch.ElapsedMilliseconds} ms";
            string logPath = "log.txt";
            File.AppendAllText(logPath, logMessage + Environment.NewLine);
        }

        /// <summary>
        /// Метод, що викликається при натисканні на кнопку вирішення задачі комівояжера за допомогою методу локального пошуку з емуляцією відпалу.
        /// </summary>
        private void buttonSolveSim(object sender, EventArgs e)
        {
            if (dist.Length < 5)
            {
                MessageBox.Show("Please, load poper graph first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            DrawPoints(Points, Program.Form.pictureBox1);
            Res = SolveSimulatedAnnealing(dist);
            stopwatch.Stop();
            string logMessage = $"[{DateTime.Now}] Execution Time: {stopwatch.ElapsedMilliseconds} ms";
            string logPath = "log.txt";
            File.AppendAllText(logPath, logMessage + Environment.NewLine);
        }

        /// <summary>
        /// Метод, що дозволяю користувачу додавати або видаляти точки на графіку, в залежності від активного режиму.
        /// </summary>
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsDeletePoint)
            {
                Points.RemoveAll(p =>
                    Vector2D.Distance(p, new Vector2D(e.X, e.Y)) <= 5f
                );
                dist = CreateDistanceMatrix(Points);
                DrawPoints(Points, Program.Form.pictureBox1);
                Res = [];
                return;
            }
            if (IsAddingPoint)
            {
                var p = new Vector2D(e.X, e.Y);
                Points.Add(p);
                dist = CreateDistanceMatrix(Points);
                DrawPoints(Points, Program.Form.pictureBox1);
                Res = [];
                return;
            }
        }

        /// <summary>
        /// Метод, що дозволяє відобразити розв'язок задачі комівояжера на графіку, якщо програма працює в режимі візуалізації.
        /// </summary>
        private void buttonDrawLines(object sender, EventArgs e)
        {
            if (!VisualMode)
            {
                MessageBox.Show("You are not in visual mode. Please load point based graph or clear the canvas", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Res.Length == 0)
            {
                MessageBox.Show("Please, calculate solution first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DrawLines(Points, Res, Program.Form.pictureBox1);
        }

        /// <summary>
        /// Метод, що дозволяє очистити робочу область програми від усіх точок та ребер, повертаючи її до початкового стану.
        /// </summary>
        private void buttonClear(object sender, EventArgs e)
        {
            Points.Clear();
            DrawPoints(Points, Program.Form.pictureBox1);
            Res = [];
            dist = new double[0, 0];
            VisualMode = true;
        }

        /// <summary>
        /// Метод, що зберігає поточні точки у файл, при натисканні кнопки. Лише матриця відстаней не зберігається, оскільки вона не може бути змінена програмою.
        /// </summary>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (!VisualMode)
            {
                MessageBox.Show("This function works only in visual mode. Please load point based graph or clear the canvas", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WriteVectors("save.txt", Points);
        }

        /// <summary>
        /// Метод, що дозволяє при натисканні кнопки згенерувати випадкові точки на графіку, за заданою кількістю, введеною користувачем у текстовому полі.
        /// </summary>
        private void GeneratePoints_Click(object sender, EventArgs e)
        {
            int amount;
            try
            {
                // Attempt to parse the user’s input:
                amount = int.Parse(Program.Form.PointsAmountBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter the integer number value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Entered number is either too large", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (amount < 1 || amount > 10000)
            {
                MessageBox.Show("Please enter a number between 1 and 10000", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Points = GenerateVectors(amount);
            dist = CreateDistanceMatrix(Points);
            DrawPoints(Points, Program.Form.pictureBox1);
            Res = [];
        }

    }
}
