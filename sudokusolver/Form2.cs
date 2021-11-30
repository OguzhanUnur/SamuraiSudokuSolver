using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudokusolver
{
    public partial class Form2 : Form
    {
        static readonly string boxText = "C:/Users/Oğuzhan/source/repos/sudokusolver/sudokusolver/sudokutxt/sudoku.TXT";
        static string file_thread5 = @"C:/Users/Oğuzhan/source/repos/sudokusolver/sudokusolver/sudokutxt/thread5.TXT";
        static FileStream fs = new FileStream(file_thread5, FileMode.OpenOrCreate, FileAccess.Write);
        static StreamWriter sw = new StreamWriter(fs);

        int boxWidth = 20;
        int boxHeight = 20;
        static List<Data> graphic = new List<Data>();
        static int tmp;
        static int temp2 = 0;

        static Stopwatch Time10 = new Stopwatch();
        TextBox[,] textList = new TextBox[21, 21];
        static char[,] sudoku0 = new char[9, 9];
        static char[,] sudoku1 = new char[9, 9];
        static char[,] sudoku2 = new char[9, 9];
        static char[,] sudoku3 = new char[9, 9];
        static char[,] sudoku4 = new char[9, 9];

        public Form2()
        {
            InitializeComponent();
        }

        public void ReadText()
        {

            string[] lines = File.ReadAllLines(boxText);

            for (int i = 0; i < 21; i++)
            {
                var x = lines[i].ToCharArray();
                for (int j = 0, y = 0, z = 0; j < 21; j++, y++, z++)
                {
                    if (x.Length == 18)
                    {

                        if (j == 9 || j == 10 || j == 11)
                        {
                            y--;
                        }
                        else
                        {
                            if (x[y] == '*')
                            {
                                textList[i, j] = new TextBox();
                                textList[i, j].SetBounds(20 + j * boxWidth, 20 + i * boxHeight, boxWidth, boxHeight);
                                samuraiSudoku.Controls.Add(textList[i, j]);

                            }
                            else
                            {

                                textList[i, j] = new TextBox();
                                textList[i, j].SetBounds(20 + j * boxWidth, 20 + i * boxHeight, boxWidth, boxHeight);


                                textList[i, j].Text = x[y].ToString();
                                samuraiSudoku.Controls.Add(textList[i, j]);
                            }
                        }
                    }

                    if (x.Length == 21)
                    {

                        if (x[j] == '*')
                        {
                            textList[i, j] = new TextBox();
                            textList[i, j].SetBounds(20 + j * boxWidth, 20 + i * boxHeight, boxWidth, boxHeight);

                            samuraiSudoku.Controls.Add(textList[i, j]);
                        }
                        else
                        {

                            textList[i, j] = new TextBox();
                            textList[i, j].SetBounds(20 + j * boxWidth, 20 + i * boxHeight, boxWidth, boxHeight);


                            textList[i, j].Text = x[j].ToString();
                            samuraiSudoku.Controls.Add(textList[i, j]);
                        }
                    }
                    if (x.Length == 9)
                    {

                        if (j == 0 || j == 1 || j == 2 || j == 3 || j == 4 || j == 5 || j == 15 || j == 16 || j == 17 || j == 18 || j == 19 || j == 20)
                        {
                            z--;
                        }
                        else
                        {
                            if (x[z] == '*')
                            {
                                textList[i, j] = new TextBox();
                                textList[i, j].SetBounds(20 + j * boxWidth, 20 + i * boxHeight, boxWidth, boxHeight);
                                samuraiSudoku.Controls.Add(textList[i, j]);

                            }
                            else
                            {

                                textList[i, j] = new TextBox();
                                textList[i, j].SetBounds(20 + j * boxWidth, 20 + i * boxHeight, boxWidth, boxHeight);


                                textList[i, j].Text = x[z].ToString();
                                samuraiSudoku.Controls.Add(textList[i, j]);

                            }
                        }

                    }

                }
            }

        }
        //sudoku oluşturma (5 adet)
        public void separateSamurai(char[,] sudoku, int str1, int str2, int stn1, int stn2)
        {
            for (int i = str1, x = 0; i < str2; i++, x++)
            {
                for (int j = stn1, y = 0; j < stn2; j++, y++)
                {
                    if (textList[i, j].Text.Length != 0)
                    {
                        sudoku[x, y] = char.Parse(textList[i, j].Text);
                    }
                    else
                    {
                        sudoku[x, y] = '.';
                    }

                }

            }

        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            Thread5();

            show();

            foreach (var i in graphic)
            {
                Console.WriteLine(i.Ms + " " + i.Boxnumber);
                chart1.Series["zamanKare"].Points.AddXY(i.Ms, i.Boxnumber);

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ReadText();
            run();
        }


        public static void solveSudoku(char[,] board,int index)
        {
            if (board == null || board.Length == 0)
                return;
            solve(board,index);


        }
        private static bool solve(char[,] board,int index)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '.')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValid(board, i, j, c))
                            {
                                board[i, j] = c;


                                if (solve(board,index))
                                {
                                    temp2++;
                                    Console.WriteLine(temp2);
                                    Time10.Stop();
                                    TimeSpan HesaplananZaman = Time10.Elapsed;
                                    string Result = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    HesaplananZaman.Hours, HesaplananZaman.Minutes, HesaplananZaman.Seconds, HesaplananZaman.Milliseconds).Substring(9);
                                    Console.WriteLine(Result);
                                    Data v = new Data()
                                    {
                                        Boxnumber = temp2,
                                        Ms = Result
                                    };
                                    sw.WriteLine(index.ToString()+". Sudokunun " + i.ToString() + "." + j.ToString() + "seçilen değer:" + c.ToString());
                                    //sw.WriteLine(v.Ms+" "+ v.Boxnumber);
                                    sw.Flush();
                                    graphic.Add(v);

                                    Time10.Start();
                                    return true;
                                }

                                else
                                    board[i, j] = '.';
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool isValid(char[,] board, int row, int col, char c)
        {
            for (int i = 0; i < 9; i++)
            {

                if (board[i, col] != '.' && board[i, col] == c)
                    return false;

                if (board[row, i] != '.' && board[row, i] == c)
                    return false;

                if (board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] != '.' && board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == c)
                    return false;
            }
            return true;
        }


        public void draw(char[,] sudoku, int str1, int str2, int stn1, int stn2)
        {
            for (int i = str1, a = 0; i < str2; i++, a++)
            {
                for (int j = stn1, b = 0; j < stn2; j++, b++)
                {
                    if (textList[i, j].Text.Length == 0)
                    {
                        textList[i, j].Text = sudoku[a, b].ToString();
                        textList[i, j].ForeColor = Color.Blue;
                    }

                }

            }

        }

        public void run()
        {
            separateSamurai(sudoku0, 0, 9, 0, 9);
            separateSamurai(sudoku1, 12, 21, 0, 9);
            separateSamurai(sudoku4, 6, 15, 6, 15);
            separateSamurai(sudoku2, 0, 9, 12, 21);
            separateSamurai(sudoku3, 12, 21, 12, 21);
        }

        public void show()
        {
            draw(sudoku0, 0, 9, 0, 9);
            draw(sudoku1, 12, 21, 0, 9);
            draw(sudoku4, 6, 15, 6, 15);
            draw(sudoku2, 0, 9, 12, 21);
            draw(sudoku3, 12, 21, 12, 21);
        }

        public void method1()
        {
            solveSudoku(sudoku0,1);
        }
        public void method2()
        {
            solveSudoku(sudoku1,2);
        }
        public void method3()
        {
            solveSudoku(sudoku2,3);
        }
        public void method4()
        {
            solveSudoku(sudoku3,4);
        }
        public void method5()
        {
            solveSudoku(sudoku4,5);
        }
        public void Thread5()
        {
            Console.WriteLine("5 thread");

            Time10.Start();
            Thread th = new Thread(method1);
            th.Start();




            Thread th1 = new Thread(method2);
            th1.Start();


            Thread th2 = new Thread(method3);
            th2.Start();

            Thread th3 = new Thread(method4);
            th3.Start();

            Thread th4 = new Thread(method5);
            th4.Start();

            th.Join();
            th1.Join();
            th2.Join();
            th3.Join();
            th4.Join();
        

        }


    }
}
