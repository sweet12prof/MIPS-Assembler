using System;
using System.Collections.Generic;
using System.Text;
using MIPS32_Assembler.AssemblerLibrary;

namespace MIPS32_Assembler.AssemblerLibrary
{
    class SymbolTable
    {
        string[] registerNames = new string[32];
        // Max entries in Symbol table is 2048
        private const int maxEntries = 2048;
        private int tableEntryCounter = 0;
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
                registerNames[tableEntryCounter] = "$" + tableEntryCounter.ToString();
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

        public void printEntries() {
            foreach (var item in symbolTable) {
                Console.WriteLine(item.Symbol + "\t\t\t" +  item.Address);
            }
        }

    }
}
