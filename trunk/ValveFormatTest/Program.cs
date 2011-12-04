using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ValveFormat;

namespace ValveFormatTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new ValveFormatParser {Path = @"C:\VFP.txt"};
            parser.LoadFile();
            /*parser.Nodes.ForEach(delegate(DataNode n)
                                     {
                                         Console.WriteLine(n.Key + " = " + n.Value);
                                         n.SubNodes.ForEach(delegate(DataNode no)
                                                                {
                                                                    Console.WriteLine("\t" + no.Key + " = " + no.Value);
                                                                    no.SubNodes.ForEach(delegate(DataNode nod)
                                                                                            {
                                                                                                Console.WriteLine("\t\t" + nod.Key + " = " + nod.Value);
                                                                                            });
                                                                });
                                     });*/
            Console.WriteLine("Saving tree...");
            File.WriteAllText(@"C:\VFP_out.txt", parser.RootNode.ToString());
            Console.WriteLine("Saving output...");
            parser.SaveFile(@"C:\VFP_save.txt");
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
