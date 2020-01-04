# MIPS-Assembler
Assembler for built Processors

Consists of back-end modules for the assembler middleware. 
The assembler translates up to 15 instructions they include:

## Instructions 
## R-Format
| Instructions | Meaning | Opcode | Rs | Rt | Rd | shamt | Funct|
|--------------|---------|--------|----|----|----|-------|------| 
| | | 6 bits| 5 bits | 5 bits | 5 bits | 5 bits | 6 bits|
| add rd, rs, rt| Rd = Rs + Rt | 000000| sssss| ttttt| ddddd| 00000| 100000| 
|sub rd, rs, rt| Rd = Rs -Rt|000000|sssss|ttttt|ddddd|00000| 100010|
|and rd, rs, rt|Rd = Rs and Rt |000000| sssss| ttttt | ddddd| 00000| 100100 |
|or rd, rs, rt|Rd = Rs or Rt |000000| sssss| ttttt | ddddd| 00000| 100101 |
|slt rd, rs,rt |rs == rt ? 1:0 |000000| sssss| ttttt | ddddd| 00000| 101010 |
|sll rd, rs, rx | rd =rs sll rx | 000000 | sssss | 00000 | 00000 | xxxxxx | 000000 |
| srl rd, rs, rx | rd = rs = srl rx | 000000 | sssss | 00000 | 00000 | xxxxxx | 000010 |  
| xor rd, rs, rt | rd = rs xor rt | 000000 | sssss | ttttt | ddddd | 00000 | 100110 |
|Jr rs  | Jump to Address in register(rs) : pc = $rs| 000000 | ssssss | 000000 | 000000 | 00000 | 001000 |  
#### I-Format
| Instructions | Meaning | Opcode | Rs | Rt | Address | 
|-|-|-|-|-|-|
|||6 bits| 5  bits | 5 bits | 16 bits |
|addi rs, rt, #**Imm**|rt = rs + Imm |001000| sssss | ttttt | Immediate(Imm)|
|lw rt, Offset(Address) | rt = MEM[Offset + Address] | 100011| sssss | ttttt | Address |
|sw rt, Offset(Address) | MEM[Offset + Address] = rt | 101011 | sssss| ttttt | Address |
|beq rs, rt, Address | rs == rt ? pc = Label + (pc + 4) : pc += 4 | 000100 | sssss | ttttt | Label | 

#### J-Format 
| Instructions | Meaning | Opccode | Jump Address |
|-|-|-|-|
|J Address | pc = Address | 000010 | Address |
| Jal Address | Jump and Link : $ra($31) = pc + 4 then pc = Address | 000011 | Address |  


#### Halt CPU
| Instructrion | Meaning | Opcode | Remaining bits |
|--------------|---------|--------|----------------|
| |  | 6 bits | 26 bits |
| halt         | halt the CPU | 11111 | (dont care) |
