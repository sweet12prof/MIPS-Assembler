using System;
using System.Collections.Generic;
using System.Text;

namespace MIPS_Assembler.Modules.InstructionModels
{
    class I_Instruction
    {
        private int outputFormat;

        public int OutputFormat
        {
            get { return outputFormat;  }
            set { outputFormat = value; }
        }
        private enum numBase
        {
            Binary = 2,
            Decimal = 10,
            Hexadecimal = 16
        }

        private string opCode;
        public string OpCode {
            get {
                return opCode;
            }
            set {
               opCode = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(6, '0');
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
                rs = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(5, '0');
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


        private string immAddress;
        public string ImmAddress
        {
            get
            {
                return immAddress;
            }

            set
            {
               immAddress = Convert.ToString(Convert.ToInt32(value), outputFormat).PadLeft(16, '0');
            }
        }

        public string getMachineCode()
        {
            string machineCode;
            machineCode = this.opCode + this.rs + this.rt + this.immAddress;
            if (machineCode.Length == 32)
                return machineCode;
            else
                return "---";
        }

        public I_Instruction(string instr, string rs, string rt, string immAddress, int Base)
        {
            OutputFormat = Base;
            switch (instr.ToLower())
            {
                case "addi":
                    OpCode = "8";
                    break;
                case "lw":
                    OpCode = "35";
                    break;
                case "sw":
                    OpCode = "43";
                    break;
                case "beq":
                    OpCode = "4";
                    break;
                default:
                    OpCode = "-";
                    break;
            }

            Rs = rs;
            Rt = rt;
            ImmAddress = immAddress;
        }

    }
}
