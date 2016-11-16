using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keyboard.Services;

namespace KeyboardConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = args[0];

            IPathService pathService = new PathService();

            var allTopics = File.ReadAllLines(filePath);

            foreach (var topic in allTopics)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(topic);

                var path = pathService.GetPath(topic);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(path);
            }

            Console.ReadKey();
        }
    }
}
