using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_QuanLySpa;
using DTO_QuanLySpa;

namespace GUI_QuanLySpa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            SpaGUI spaGUI = new SpaGUI();
            spaGUI.HienThiMenu();

            Console.WriteLine("Đã thoát chương trình.");
            Console.ReadKey();
        }

    }
}