using System;
using System.Collections.Generic;
using System.IO;
using Reflow.Core.API;
using Reflow.Models.Entity;

namespace Reflow.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string fileDir = @"C:\Users\Hentov\Desktop\testcount";

            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            Console.WriteLine(path);

            ReflowController app = new ReflowController();
            int testNumber = 1;

            Console.WriteLine($"[{testNumber++}] Get Dir()");
            Console.WriteLine(app.GetDir(null).Result);
            
            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Files In Directory()");
            Console.WriteLine(app.GetFilesInDirectory(fileDir).Result);
            
            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Files Count()");
            Console.WriteLine(app.GetFilesCount(null).Result);
            
            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Options()");
            Console.WriteLine(app.GetSettings(null).Result);
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Tags()");
            Console.WriteLine(app.GetTags(null).Result);
            
            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Progress()");
            Console.WriteLine(app.GetProgress(null).Result);


            //var res = app.UpdateTagsStructure(
              //  "[{\"Id\":1,\"TagType\":\"AutoIncrementTag\",\"Options\":[{\"Id\":1,\"TagType\":\"Has Leading Zero\",\"Type\":\"CheckBox\",\"Default\":true,\"props\":{\"name\":\"Has Leading Zero\",\"default\":true}},{\"Id\":2,\"TagType\":\"Start From\",\"Type\":\"NumericBox\",\"Default\":0,\"props\":{\"name\":\"Start From\",\"default\":0}},{\"Id\":3,\"TagType\":\"Skip\",\"Type\":\"NumericBox\",\"Default\":0,\"props\":{\"name\":\"Skip\",\"default\":0}}]}]");
            
            var res2 = app.GetFiles(null).Result.ToString();
            Console.WriteLine(res2);
        }
    }
}
