using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MIPS32_Assembler.AssemblerLibrary
{
    class AssemblerPass1
    {
        private List<string> decommentedInstruction = new List<string>();
        private List<string> deWhiteSpacednstruction = new List<string>();
        private string fileLocation;

        public string FileLocation {
            get {
                return fileLocation;
            }

            set {
                fileLocation = value;      
            }           

        }

        public void InitialiseAssemblerPass1() {
            bool check = checkFile();
            if (check == true)
            {
                removeComments();
                removeWhitespace();
                saveDecommentedFile();
            }
            else
                Console.WriteLine("Your file does not exist or is not an asm or mipsm file");
        }

        private bool checkFile()
        {
            return File.Exists(fileLocation);
           
        }

        private void removeComments() {
            string newString;
            int hashIndex;
            int slashIndex;

            foreach (string lines in File.ReadAllLines(fileLocation))
            {
                hashIndex = lines.IndexOf("#");
                slashIndex = lines.IndexOf("//");

                if (hashIndex > -1)
                {
                    newString = lines.Remove(hashIndex);
                    decommentedInstruction.Add(newString);
                }
                   
                else if (slashIndex > -1)
                {
                    newString = lines.Remove(slashIndex);
                    decommentedInstruction.Add(newString);
                }
                else
                {
                    decommentedInstruction.Add(lines);
                }
            }
              
        }


        private void removeWhitespace()
        {
            string newString;
            foreach(var line in decommentedInstruction)
            {
                if(line != string.Empty)
                {
                    newString = line.Trim();
                    deWhiteSpacednstruction.Add(newString);
                }

            }
        }

        public void printrfInstruction()
        {
            foreach(var term in deWhiteSpacednstruction)
                Console.WriteLine(term);
        }



        public void saveDecommentedFile()
        {
            string filepath = Path.GetDirectoryName(fileLocation) + @"\" + Path.GetFileNameWithoutExtension(fileLocation) + ".inter";
            File.WriteAllLines(filepath, deWhiteSpacednstruction);
        }


        //public string FileLocation
        //{
        //    get
        //    {
        //        return fileLocation;
        //    }

        //    set
        //    {
        //        fileLocation = value;
        //    }
        //}

        //public void openFile()
        //{
        //    int i = 0;
        //    if (!File.Exists(fileLocation))
        //        Console.WriteLine("Assembly File Does not exist");

        //    else if (Path.GetExtension(fileLocation) == ".asm" || Path.GetExtension(fileLocation) == ".mipsm" || Path.GetExtension(fileLocation) == ".txt")
        //    {

        //        //File.Create(Path.GetDirectoryName(fileLocation) + @"\" + Path.GetFileNameWithoutExtension(fileLocation) + ".inter");
        //        // Console.WriteLine(curDir.FullName);
        //        //Console.WriteLine("File Attribute is: " + File.GetAttributes(fileLocation) + Path.GetExtension(fileLocation));
        //        foreach (string line in File.ReadAllLines(fileLocation))
        //        {
        //            Console.WriteLine("Line" + i + ": " + line);
        //            i++;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Provide an asm, mipsm or txt assembly file");
        //    }


        //}




        //public void removeWhiteSpace()
        //{
        //    foreach (string line in File.ReadAllLines(fileLocation))
        //    {
        //        removeComments(line);

        //        String TrimString = line.Trim();
        //        //string charTOremove = " ";
        //        // TrimString = TrimString.Replace(charTOremove, string.Empty);
        //        Console.WriteLine("\n\n\n\n\n" + TrimString);

        //        //Console.WriteLine("First slash Index: "+slashCount + "\n" + "First hash Index: " + hashCount);
        //    }
        //}

        //public v removeComments(string Ts)
        //{
        //    int slashCount = Ts.IndexOf("//");
        //    int hashCount = Ts.IndexOf("#");

        //    if (slashCount > 1)
        //        Ts = Ts.Remove(slashCount);
        //    else if (hashCount > 1)
        //        Ts = Ts.Remove(hashCount);

        //    //Console.WriteLine(Ts);

        //    if (Ts != string.Empty)
        //        rfInstruction.Add(Ts);

        //    Console.WriteLine("\n\n\n\n\n\n\n");

        //    foreach (var line in rfInstruction)
        //    {
        //        Console.WriteLine(line);
        //    }

        //    saveRefinedCode();
        //}


        //public void saveRefinedCode()
        //{
        //    string filepath = Path.GetDirectoryName(fileLocation) + @"\" + Path.GetFileNameWithoutExtension(fileLocation) + ".inter";
        //    File.WriteAllLines(filepath, rfInstruction);
        //}


    }
}
