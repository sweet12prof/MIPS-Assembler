﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MIPS_Assembler.Modules.InstructionModels;
namespace MIPS32_Assembler.AssemblerLibrary
{
    class AssemblerPass1
    {
        private List<string> decommentedInstruction = new List<string>();
        private List<string> deWhiteSpacednstruction = new List<string>();
        private List<string> delabelledInstruction = new List<string>();
        private List<string> replaceLabelInstr = new List<string>();
        private List<string> machineCode = new List<string>();
        private string fileLocation;
        private SymbolTableEntry newEntry = new SymbolTableEntry();
        private SymbolTable newTable = new SymbolTable();
         
        

        private List<string> Rinstructions = new List<string>
        {
            "add",
            "sub",
            "and",
            "or",
            "xor",
            "slt",
            "sll",
            "srl",
            "jr"
        };


        private List<string> Iinstructions = new List<string>
        {
            "addi",
            "lw",
            "sw",
            "beq"
        };

        private List<string> Jinstructions = new List<string>
        {
            "j",
            "jal",
        };


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
                replaceLabels();
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
            string filepath2 = Path.GetDirectoryName(fileLocation) + @"\" + Path.GetFileNameWithoutExtension(fileLocation) + ".inters";
            //string filepath3 = Path.GetDirectoryName(fileLocation) + @"\" + Path.GetFileNameWithoutExtension(fileLocation) + ".mac";
            File.WriteAllLines(filepath, replaceLabelInstr);
            File.WriteAllLines(filepath2, delabelledInstruction);
            //File.WriteAllLines(filepath3, delabelledInstruction);

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

        public void replaceLabels()
        {
            string newString2;
            int address;
            foreach(var instr in delabelledInstruction)
            {
                string [] newString = instr.Split(" ");
               
                if(Rinstructions.Contains(newString[0]))
                {
                    newString2 = instr.Replace("$", "");
                    newString2 = newString2.Replace(",", "");
                    replaceLabelInstr.Add(newString2);

                    //string[] newString3 = newString2.Split(" ");
                    //if(newString3[0] == "add" || newString3[0] == "sub" || newString3[0] == "and"  || newString3[0] == "or"  || newString3[0] == "xor"  || newString3[0] == "slt" )
                    //{
                    //    R_Instruction newR = new R_Instruction(newString3[1], newString3[2], newString3[3], "0", newString3[0], "2");
                    //    machineCode.Add(newR.getMachineCode());


                    //}
            
                }
                    
                else if (Iinstructions.Contains(newString[0]))
                {
                    switch (newString[0].ToLower())
                    {
                        case "addi":
                            newString2 = instr.Replace("$", "");
                            newString2 = newString2.Replace(",", "");
                            replaceLabelInstr.Add(newString2);
                            break;
                        case "beq":
                            {
                                address = newTable.findAddress(newString[3]);
                                if (address != -1)
                                {
                                    newString2 = instr.Replace(newString[3], (address -1).ToString());
                                    newString2 = newString2.Replace("$", "");
                                    newString2 = newString2.Replace(",", "");
                                    replaceLabelInstr.Add(newString2);
                                }
                                
                                break;
                            }
                            
                        case "lw":
                        case "sw":
                            newString2 = instr.Replace("$", "");
                            newString2 = newString2.Replace(",", "");
                            replaceLabelInstr.Add(newString2);
                            break;

                        default:
                            break;
                    }
                    
                }
                else if (Jinstructions.Contains(newString[0]))
                {
                    if(newString[0].ToLower() == "j" || newString[0].ToLower() == "jal")
                    {
                        address = newTable.findAddress(newString[1]);
                        if (address != -1)
                        {
                            newString2 = instr.Replace(newString[1], (address - 1).ToString());
                            newString2 = newString2.Replace("$", "");
                            newString2 = newString2.Replace(",", "");
                            replaceLabelInstr.Add(newString2);
                        }
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
