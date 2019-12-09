#99 = end
#1 = add two nums and store in pos 3
#2 = multiply instead of add
#position 0 = opcode, 1 = pos of first int, 2 = pos of second int, 3 = pos to place sum/product
#after processing each, move forward 4 positions to the next opcode
#after computer runs:
# before running the program replace position 1 with the value 12 and replace position 2 with the value 2
#return pos 0 after program halts

# new ops
# opcode 3 = take an input and put it in the position specified by the pos right after the opcode
# 3,13
# opcode 4 outputs the value of the position specified in the pos right after the opcode

# add support for parameter modes
# parameter mode 0 == position mode. Until now this was the only mode
# parameter mode 1 == immediate mode. Instead of position, the parameters following the opcode will be in a mode specied with the opcode.
# right 2 of opcode specify opcode. Others specify position or immediate modes (o or 1)
# 01002 = position, immediate, position, opcode 02 (multiply)
# ABCDE
# POSITIONS ARE READ FROM RIGHT TO LEFT
# parameters that an instruction writes to will never be in immediate mode
# instead of moving forward 4 instructions, move forward the number of values in the instruction

#start by requesting the ID of the system to test. In this case it will be 1, the ID for the ship's Air Conditioner
#for each test it will run an output instruction indicating how far away the result was from the expected value
#0 means successful

#finally the program will output a diagnostic code and immediately halt
#the diagnostic code followed immediately by a halt means the program finished. If all outputs but the final are 0, the program was successful

# 1002 4     3     50
# IRxO Int1  Int2  Pos2

# input = [102,4,3,3,1001,2,3,5,99]
# print(input)

# Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
# Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
# Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
# Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.

#Normally, after an instruction is finished, the instruction pointer increases by the number of values in that instruction. 
# However, if the instruction modifies the instruction pointer, that value is used and the instruction pointer is not automatically increased.

#00101
# print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")

codeinput = [3,225,1,225,6,6,1100,1,238,225,104,0,1101,32,43,225,101,68,192,224,1001,224,-160,224,4,224,102,8,223,223,1001,224,2,224,1,223,224,223,1001,118,77,224,1001,224,-87,224,4,224,102,8,223,223,1001,224,6,224,1,223,224,223,1102,5,19,225,1102,74,50,224,101,-3700,224,224,4,224,1002,223,8,223,1001,224,1,224,1,223,224,223,1102,89,18,225,1002,14,72,224,1001,224,-3096,224,4,224,102,8,223,223,101,5,224,224,1,223,224,223,1101,34,53,225,1102,54,10,225,1,113,61,224,101,-39,224,224,4,224,102,8,223,223,101,2,224,224,1,223,224,223,1101,31,61,224,101,-92,224,224,4,224,102,8,223,223,1001,224,4,224,1,223,224,223,1102,75,18,225,102,48,87,224,101,-4272,224,224,4,224,102,8,223,223,1001,224,7,224,1,224,223,223,1101,23,92,225,2,165,218,224,101,-3675,224,224,4,224,1002,223,8,223,101,1,224,224,1,223,224,223,1102,8,49,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1107,226,226,224,1002,223,2,223,1005,224,329,1001,223,1,223,1007,677,226,224,1002,223,2,223,1006,224,344,1001,223,1,223,108,677,226,224,102,2,223,223,1006,224,359,1001,223,1,223,7,226,226,224,1002,223,2,223,1005,224,374,101,1,223,223,107,677,677,224,1002,223,2,223,1006,224,389,1001,223,1,223,1007,677,677,224,1002,223,2,223,1006,224,404,1001,223,1,223,1107,677,226,224,1002,223,2,223,1005,224,419,1001,223,1,223,108,226,226,224,102,2,223,223,1006,224,434,1001,223,1,223,1108,226,677,224,1002,223,2,223,1006,224,449,1001,223,1,223,1108,677,226,224,102,2,223,223,1005,224,464,1001,223,1,223,107,226,226,224,102,2,223,223,1006,224,479,1001,223,1,223,1008,226,226,224,102,2,223,223,1005,224,494,101,1,223,223,7,677,226,224,1002,223,2,223,1005,224,509,101,1,223,223,8,226,677,224,1002,223,2,223,1006,224,524,1001,223,1,223,1007,226,226,224,1002,223,2,223,1006,224,539,101,1,223,223,1008,677,677,224,1002,223,2,223,1006,224,554,101,1,223,223,1108,677,677,224,102,2,223,223,1006,224,569,101,1,223,223,1107,226,677,224,102,2,223,223,1005,224,584,1001,223,1,223,8,677,226,224,1002,223,2,223,1006,224,599,101,1,223,223,1008,677,226,224,102,2,223,223,1006,224,614,1001,223,1,223,7,226,677,224,1002,223,2,223,1005,224,629,101,1,223,223,107,226,677,224,102,2,223,223,1005,224,644,101,1,223,223,8,677,677,224,102,2,223,223,1005,224,659,1001,223,1,223,108,677,677,224,1002,223,2,223,1005,224,674,101,1,223,223,4,223,99,226]
# codeinput = [1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,13,1,19,1,10,19,23,1,6,23,27,1,5,27,31,1,10,31,35,2,10,35,39,1,39,5,43,2,43,6,47,2,9,47,51,1,51,5,55,1,5,55,59,2,10,59,63,1,5,63,67,1,67,10,71,2,6,71,75,2,6,75,79,1,5,79,83,2,6,83,87,2,13,87,91,1,91,6,95,2,13,95,99,1,99,5,103,2,103,10,107,1,9,107,111,1,111,6,115,1,115,2,119,1,119,10,0,99,2,14,0,0]

memory = codeinput.copy()

currpos = 0

def compute(opcode, parameters, codeinput, usercodeinput):
    global currpos
    print("Got a", opcode, codeinput[currpos:currpos+10])
    if opcode == 1 or opcode == 2:
        if parameters[-1] == 0:
            int1 = codeinput[codeinput[currpos+1]]
        elif parameters[-1] == 1:
            int1 = codeinput[currpos+1]
        if parameters[-2] == 0:
            int2 = codeinput[codeinput[currpos+2]]
        elif parameters[-2] == 1:
            int2 = codeinput[currpos+2]
        if parameters[-3] == 0:
            pos2 = codeinput[codeinput[currpos+3]]
        elif parameters[-3] == 1:
            pos2 = codeinput[currpos+3]
    if opcode == 4:
        if parameters[-1] == 0:
            int1 = codeinput[codeinput[currpos+1]]
        elif parameters[-1] == 1:
            int1 = codeinput[currpos+1]
    if opcode == 5:
        if parameters[-1] != 0:
            currpos = parameters[-2]
        else:
            currpos += 1
    if opcode == 6:
        if parameters[-1] == 0:
            currpos = parameters[-2]
        else:
            currpos += 1
    if opcode == 7:
        if parameters[-1] < parameters[-2]:
            codeinput[parameters[-3]] == 1
        else:
            codeinput[parameters[-3]] == 0
        currpos += 1
    if opcode == 8:
        if parameters[-1] == parameters[-2]:
            codeinput[parameters[-3]] == 1
        else:
            codeinput[parameters[-3]] == 0
        currpos += 1
    if opcode == 1:
        codeinput[codeinput[currpos+3]] = int1 + int2
        currpos += 4
    if opcode == 2:
        codeinput[codeinput[currpos+3]] = int1 * int2
        currpos += 4
    if opcode == 3:
        codeinput[codeinput[currpos+1]] = usercodeinput
        currpos += 2
    if opcode == 4:
        print(int1)
        currpos += 2
    return(codeinput)

def getopcode(firstitem):
    return(int(str(firstitem)[-1:]))

def getparameters(firstitem):
    firstitem = str(firstitem).rjust(5,"0")
    # print("firstitem", firstitem)
    if len(firstitem) > 2:
        params = [int(i) for i in firstitem[0:-2]]
        # print("params",params)
        return(params)
    else:
        return([])

while codeinput[currpos] != 99:
    opcode = getopcode(codeinput[currpos])
    parameters = getparameters(codeinput[currpos])
    compute(opcode, parameters, codeinput, 5)
    print(currpos)
    keepgoing = input("Keepgoing")
    print(codeinput[currpos:currpos+4])

    # for pos in range(0, len(input), 4):
    #     if input[pos] == 1:
    #         int1 = input[pos+1]
    #         int2 = input[pos+2]
    #         sumpos = input[pos+3]
    #         secondary[sumpos] = input[int1] + input[int2]
    #     elif input[pos] == 2:
    #         int1 = input[pos+1]
    #         int2 = input[pos+2]
    #         prodpos = input[pos+3]
    #         secondary[prodpos] = input[int1] * input[int2]
    #         # print("Mult %s * %s and put in %s" % (input[int1], input[int2], input[prodpos]))
    #     elif input[pos] == 99:
    #         break
    #     else:
    #         print("Problem, opcode incorrect")
    #     answer = input[0]
    #     input = secondary
