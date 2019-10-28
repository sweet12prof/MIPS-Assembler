using System;
using System.Collections.Generic;
using System.Text;
using MIPS32_Assembler.AssemblerLibrary;

namespace MIPS32_Assembler.AssemblerLibrary
{
    class SymbolTable
    {
        //string[] registerNames = new string[32];
        // Max entries in Symbol table is 2048
        private const int maxEntries = 2048;
        private int tableEntryCounter = 0;

        private string [] registerNames = {
            "zero",
            "at",
            "v0", "v1",
            "a0", "a1", "a2", "a3",
            "t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7",
            "s0","s1", "s2", "s3", "s4", "s5", "s6", "s7",
            "t8", "t9",
            "k0", "k1",
            "gp", "sp", "fp", "ra"
        };
        // Creating Symbol Table off Size max Entries
        SymbolTableEntry[] symbolTable = new SymbolTableEntry[maxEntries];

        // Initialise Symbol Table Function

        public SymbolTable()
        {
            InitialiseSymbolTable();
        }
        public void InitialiseSymbolTable() 
        {
            //Initialise Register names
            InitialiseRegisternames();
            for(int j= tableEntryCounter; j<maxEntries; j++)
            {
                symbolTable[j] = new SymbolTableEntry();
                symbolTable[j].Symbol = null;
                symbolTable[j].Address = -1;
            }

        }

        public void InitialiseRegisternames() {
          
            foreach (var term in registerNames)
            {
                registerNames[tableEntryCounter] =  term;
                symbolTable[tableEntryCounter]  = new SymbolTableEntry();
                symbolTable[tableEntryCounter].Symbol = registerNames[tableEntryCounter];
                symbolTable[tableEntryCounter].Address = tableEntryCounter;
                tableEntryCounter++;
            }
        }


        public void addEntry(SymbolTableEntry entryVar) {
            int i = 0;
            while ((symbolTable[i].Symbol != entryVar.Symbol) && (symbolTable[i].Symbol != null) && (i < maxEntries)) 
                    i++;
            if (symbolTable[i].Symbol == null)
            {
                symbolTable[i].Symbol = entryVar.Symbol;
                symbolTable[i].Address = entryVar.Address;
            }

            else if ((symbolTable[i].Symbol == entryVar.Symbol) && (symbolTable[i].Address == -1))
            {
                symbolTable[i].Address = entryVar.Address;
            }

            else if (i > maxEntries) {
                Console.WriteLine("\nSymbol table is full, Increase maxEntry and recompile");
            }
              
            
        }


        public int findAddress(string label)
        {
            foreach(var Entry in symbolTable)
            {
                if (Entry.Symbol == label)
                    return Entry.Address; 

            }
            return -1;
        }
        public void printEntries() {
            foreach (var item in symbolTable) {
                Console.WriteLine(item.Symbol + "\t\t\t" +  item.Address);
            }
        }

    }
}
