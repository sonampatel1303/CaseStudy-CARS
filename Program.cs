using CaseStudy.main;
using CaseStudy.Model;
using CaseStudy.Repository;

namespace CaseStudy
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            MainModule mainModule = new MainModule();
            mainModule.DisplayMenu();
        }
    }
}
