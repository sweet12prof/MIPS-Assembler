using System;
using System.Collections.Generic;
using System.Text;

namespace MIPS_Assembler.Modules.InstructionModels
{
    class R_Instruction
    {
        private int outputFormat;
        private enum numBase
        {
            Binary = 2,
            Decimal = 10,
            Hexadecimal = 16
        }

        public int OutputFormat
        { get {
                return outputFormat;
            } set {  outputFormat = value; } }

        private string opCode;

        public string Opcode
        {
            get { return opCode; }

            set
            {
                opCode = Convert.ToString(Convert.ToInt32("0"), outputFormat).PadLeft(6, '0');
            }
        }

        private string rs;
        public string Rs
        {
            get
            {
                return rs;
            }

            set
            {
                rs = Convert.ToString(int.Parse(value), outputFormat).PadLeft(5, '0');
            }
        }

        private string rt;
        public string Rt
        {
            get
            {
                return rt;
            }

            set
            {
                rt = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(5, '0');
            }
        }

        private string rd;
        public string Rd
        {
            get
            {
                return rd;
            }

            set
            {
                rd = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(5, '0');
            }
        }

        private string shamt;
        public string Shamt
        {
            get
            {
                return shamt;
            }

            set
            {
                 shamt = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(5, '0');
            }
        }

        private string funct;
        public string Funct
        {
            get
            {
                return funct;
            }

            set
            {
                    funct = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(6, '0');
              
                    
            }
        }

        public string getMachineCode()
        {
            string machineCode;
                machineCode = this.opCode + this.rs + this.rt + this.rd + this.shamt + this.funct;
                return machineCode;
         
        }


        public R_Instruction(string rs, string rt, string rd, string shamt, string instr, int instrFormat)
        {
            
            OutputFormat = instrFormat;
            Opcode = "0";
            Rs = rs;
            Rt = rt;
            Rd = rd;
            Shamt = shamt;
            
            switch (instr.ToLower())
            {
                case "add":                   
                     Funct = "32";
                break;

                case "and":
                    Funct = "36";
                break;

                case "sub":
                    Funct = "34";
                    break;
                case "or":
                    Funct = "37";
                    break;

                case "xor":
                    Funct = "38";
                    break;
                case "slt":
                    Funct = "52";
                    break;
                case "sll":
                    Funct = "0";
                    break;
                case "srl":
                    Funct = "2";
                    break;
                case "jr":
                    Funct = "8";
                    break;
                default:
                    Funct = "-";
                    break;

                    
            }
        }
    }
}
