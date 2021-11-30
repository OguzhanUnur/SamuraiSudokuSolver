using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudokusolver
{
    class Global
    {
        public static TextBox[,] text { get; set; } = new TextBox[21, 21];
        public static Stopwatch stopWatch = new Stopwatch();
        public static Stopwatch stopWatch2 = new Stopwatch();
        static readonly string txtThread10 = "C:/Users/Oğuzhan/source/repos/sudokusolver/sudokusolver/sudokutxt/thread10.TXT";
        public static FileStream fileStream = new FileStream(txtThread10, FileMode.OpenOrCreate, FileAccess.Write);
        public static StreamWriter streamWriter = new StreamWriter(fileStream);
        static readonly string txtThread5 = "C:/Users/Oğuzhan/source/repos/sudokusolver/sudokusolver/sudokutxt/thread5.TXT";
        public static FileStream fileStream2 = new FileStream(txtThread5, FileMode.OpenOrCreate, FileAccess.Write);
        public static StreamWriter streamWriter2 = new StreamWriter(fileStream2);

    }
}