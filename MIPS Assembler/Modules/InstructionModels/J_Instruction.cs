using System;
using System.Collections.Generic;
using System.Text;

namespace MIPS_Assembler.Modules.InstructionModels
{
    class J_Instruction
    {
        private int outputFormat;

        public int OutputFormat
        {
            get { return outputFormat; }
            set { outputFormat = value; }
        }
        private enum numBase
        {
            Binary = 2,
            Decimal = 10,
            Hexadecimal = 16
        }


        private string opCode;
        public string OpCode
        {
            get
            {
                return opCode;
            }
            set
            {
                opCode = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(6, '0');
            }
        }

        private string  jumpAddress;
        public string JumpAddress
        {
            get
            {
                return jumpAddress;
            }

            set
            {
                jumpAddress = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(26, '0');
            }
        }

        public string getMachineCode()
        {
            string machineCode = OpCode + JumpAddress;
            if (machineCode.Length == 32)
                return machineCode;
            else
                return "---";
        }

        public J_Instruction(string instr,  string jumpAddress, int numBase)
        {
            OutputFormat = numBase;
            switch (instr.ToLower())
            {
                case "j":
                    OpCode = "2";
                break;

                case "jal":
                    OpCode = "3";
                    break;
                default:
                    OpCode = "-";
                    break;

            }

            JumpAddress = jumpAddress;
        }
    }
}
