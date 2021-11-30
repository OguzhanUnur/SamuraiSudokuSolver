using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace sudokusolver
{

    public partial class Form1 : Form
    {

        static readonly string boxText = "C:/Users/Oğuzhan/source/repos/sudokusolver/sudokusolver/sudokutxt/sudoku.TXT";
        static string file_thread10 = @"C:/Users/Oğuzhan/source/repos/sudokusolver/sudokusolver/sudokutxt/thread10.TXT";
        static FileStream fs = new FileStream(file_thread10, FileMode.OpenOrCreate, FileAccess.Write);
        static StreamWriter sw = new StreamWriter(fs);
        int boxWidth = 20;
        int boxHeight = 20;
        static List<Data> graphic = new List<Data>();

        static int tmp;
        static int temp1 = 0;

        static Stopwatch Time5 = new Stopwatch();
        TextBox[,] textList = new TextBox[21, 21];
        static char[,] sudoku0 = new char[9, 9];
        static char[,] sudoku1 = new char[9, 9];
        static char[,] sudoku2 = new char[9, 9];
        static char[,] sudoku3 = new char[9, 9];
        static char[,] sudoku4 = new char[9, 9];
        public Form1()
        {
            InitializeComponent();


        }

        //txt den oku ve ekrana yazdır.
        public void ReadboxText()
        {

            string[] xLine = File.ReadAllLines(boxText);

            for (int i = 0; i < 21; i++)
            {
                var x = xLine[i].ToCharArray();
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
        //sudokularını samurai haline getir
        public static void solveSudoku2(char[,] board, int index )
        {
            if (board == null || board.Length == 0)
                return;
            solve2(board,index);


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
                                    temp1++;

                                    Time5.Stop();
                                    TimeSpan HesaplananZaman = Time5.Elapsed;
                                    string Result = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    HesaplananZaman.Hours, HesaplananZaman.Minutes, HesaplananZaman.Seconds, HesaplananZaman.Milliseconds).Substring(9);

                                    Data v = new Data()
                                    {
                                        Boxnumber = temp1,
                                        Ms = Result
                                    };
                                    graphic.Add(v);
                                    sw.WriteLine(index.ToString()+". Sudokunun " + i.ToString() + "." + j.ToString() + "seçilen değer:" + c.ToString());
                                    //sw.WriteLine(v.Ms + " " + v.Boxnumber);
                                    sw.Flush();
                                    Time5.Start();
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

        private static bool solve2(char[,] board,int index)
        {
            for (int i = board.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = board.GetLength(1) - 1; j >= 0; j--)
                {
                    if (board[i, j] == '.')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValid(board, i, j, c))
                            {
                                board[i, j] = c;


                                if (solve2(board,index))
                                {
                                    temp1++;

                                    Time5.Stop();
                                    TimeSpan HesaplananZaman = Time5.Elapsed;
                                    string Result = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    HesaplananZaman.Hours, HesaplananZaman.Minutes, HesaplananZaman.Seconds, HesaplananZaman.Milliseconds).Substring(9);

                                    Data v = new Data()
                                    {
                                        Boxnumber = temp1,
                                        Ms = Result
                                    };
                                    //sw.WriteLine(v.Ms + " " + v.Boxnumber);
                                    sw.WriteLine(index.ToString()+". Sudokunun " + i.ToString() + "." + j.ToString() + "seçilen değer:" + c.ToString());
                                    sw.Flush();
                                    graphic.Add(v);
                                    Time5.Start();
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

        private void Form1_Load(object sender, EventArgs e)
        {

            ReadboxText();
            run();
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
            //Thread.Sleep(1000);


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

        public void method12()
        {
            solveSudoku2(sudoku0,1);
        }
        public void method22()
        {
            solveSudoku2(sudoku1,2);
        }
        public void method32()
        {
            solveSudoku2(sudoku2,3);
        }
        public void method42()
        {
            solveSudoku2(sudoku3,4);
        }
        public void method52()
        {
            solveSudoku2(sudoku4,5);
        }


        public void Thread10()
        {
            Console.WriteLine("10 thread");
            Thread th20 = new Thread(method1);
            Thread th25 = new Thread(method12);
            Thread th21 = new Thread(method2);
            Thread th26 = new Thread(method22);

            Thread th22 = new Thread(method3);
            Thread th27 = new Thread(method32);
            Thread th23 = new Thread(method4);
            Thread th28 = new Thread(method42);
            Thread th24 = new Thread(method5);
            Thread th29 = new Thread(method52);
            Time5.Start();

            th25.Start();
            th20.Start();




            th26.Start();
            th21.Start();




            th27.Start();
            th22.Start();




            th28.Start();
            th23.Start();




            th29.Start();
            th24.Start();






            th20.Join();
            th25.Join();
            th21.Join();
            th26.Join();
            th22.Join();
            th27.Join();

            th23.Join();
            th28.Join();
            th24.Join();
            th29.Join();

        }

        //çözdürme butonu
        private void button1_Click(object sender, EventArgs e)
        {


            Thread10();

            show();

            foreach (var i in graphic)
            {
                // Console.WriteLine(i.Ms+" "+i.Boxnumber);
                chart1.Series["zamanKare"].Points.AddXY(i.Ms, i.Boxnumber);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form2 frm = new Form2();
            frm.Show();

        }
    }
}
