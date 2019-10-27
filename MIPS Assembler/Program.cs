using System;
using MIPS32_Assembler.AssemblerLibrary;
namespace MIPS_Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblerPass1 newAP1 = new AssemblerPass1();
            Console.WriteLine("Enter fileLocation: ");
            string fileLocation = Console.ReadLine();

            newAP1.FileLocation = fileLocation;

            newAP1.InitialiseAssemblerPass1();
            newAP1.printrfInstruction();
        }
    }
}
