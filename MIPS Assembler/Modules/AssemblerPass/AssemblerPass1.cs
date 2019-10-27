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
        private List<string> delabelledInstruction = new List<string>();
        private string fileLocation;
        private SymbolTableEntry newEntry = new SymbolTableEntry();
        private SymbolTable newTable = new SymbolTable();

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
                findandstorelabels();
                printCurrentSymbolTable();
                removeLabels();
                savedelabeeledFile();
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



        public void savedelabeeledFile()
        {
            string filepath = Path.GetDirectoryName(fileLocation) + @"\" + Path.GetFileNameWithoutExtension(fileLocation) + ".inter";
            File.WriteAllLines(filepath, delabelledInstruction);
        }

        public void findandstorelabels()
        {
           // string newString;
            int colonindex;
            int address = 1;
            foreach (var line in deWhiteSpacednstruction)
            {
                colonindex = line.IndexOf(":");
                if(colonindex < 0)
                    address++;
                else
                {
                    if(line.Substring(colonindex, (line.Length - colonindex -1)) == String.Empty)
                    {
                        newEntry.Symbol = line.Substring(0, colonindex).Trim();
                        newEntry.Address = address + 1;
                        newTable.addEntry(newEntry);
                        address++;
                    }
                    else
                    {
                        newEntry.Symbol = line.Substring(0, colonindex).Trim();
                        newEntry.Address = address;
                        newTable.addEntry(newEntry);
                        address++;
                    }
                }
                
            }
        }

        public void removeLabels()
        {
            string newString;
            int colonindex;
            foreach (var line in deWhiteSpacednstruction)
            {
                colonindex = line.IndexOf(":");
                if (colonindex < 0)
                    delabelledInstruction.Add(line);
                else
                {
                    if(line.Substring(colonindex, (line.Length - colonindex - 1)) != String.Empty)
                    {
                        newString = line.Replace(line.Substring(0, colonindex + 1), "");
                        newString = newString.Trim();
                        delabelledInstruction.Add(newString);
                    }
                }
                     
            }
        }

        public void printCurrentSymbolTable()
        {
            newTable.printEntries();
        }

       
    }
}
