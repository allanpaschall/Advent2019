#99 = end
#1 = add two nums and store in pos 3
#2 = multiply instead of add
#position 0 = opcode, 1 = pos of first int, 2 = pos of second int, 3 = pos to place sum/product
#after processing each, move forward 4 positions to the next opcode
#after computer runs:
# before running the program replace position 1 with the value 12 and replace position 2 with the value 2
#return pos 0 after program halts

input = [1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,13,1,19,1,10,19,23,1,6,23,27,1,5,27,31,1,10,31,35,2,10,35,39,1,39,5,43,2,43,6,47,2,9,47,51,1,51,5,55,1,5,55,59,2,10,59,63,1,5,63,67,1,67,10,71,2,6,71,75,2,6,75,79,1,5,79,83,2,6,83,87,2,13,87,91,1,91,6,95,2,13,95,99,1,99,5,103,2,103,10,107,1,9,107,111,1,111,6,115,1,115,2,119,1,119,10,0,99,2,14,0,0]
memory = [1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,13,1,19,1,10,19,23,1,6,23,27,1,5,27,31,1,10,31,35,2,10,35,39,1,39,5,43,2,43,6,47,2,9,47,51,1,51,5,55,1,5,55,59,2,10,59,63,1,5,63,67,1,67,10,71,2,6,71,75,2,6,75,79,1,5,79,83,2,6,83,87,2,13,87,91,1,91,6,95,2,13,95,99,1,99,5,103,2,103,10,107,1,9,107,111,1,111,6,115,1,115,2,119,1,119,10,0,99,2,14,0,0]

def compute(input):
    secondary = [i for i in input]
    for pos in range(0, len(input), 4):
        if input[pos] == 1:
            int1 = input[pos+1]
            int2 = input[pos+2]
            sumpos = input[pos+3]
            secondary[sumpos] = input[int1] + input[int2]
        elif input[pos] == 2:
            int1 = input[pos+1]
            int2 = input[pos+2]
            prodpos = input[pos+3]
            secondary[prodpos] = input[int1] * input[int2]
            # print("Mult %s * %s and put in %s" % (input[int1], input[int2], input[prodpos]))
        elif input[pos] == 99:
            break
        else:
            print("Problem, opcode incorrect")
        answer = input[0]
        input = secondary
    return(answer)

print("part1: ", compute(input))

def part2(input):
    for noun in range(100):
        for verb in range(100):
            input = memory
            input[1] = noun
            input[2] = verb
            if compute(input) == 19690720:
                return input[1]*100 + input[2]
            
print("part2: ", part2(input))