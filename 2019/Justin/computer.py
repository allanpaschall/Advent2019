def comp(compList, inputVal):
    compList = compList.copy()
    pos = 0
    while pos <= len(compList)-1:
        op = str(compList[pos])
        operation = 98
        modes = []
        if len(op) > 1:
            operation = int(op[-2:])
            modes = [int(x) for x in list(op[:-2])[::-1]]
        else:
            operation = int(op)

        # No params
        # Exit op
        if (operation == 99):
            return compList

        # No getter, One setter
        # takes a single integer as input and saves it to the position given by its only parameter
        if (operation == 3):
            compList[compList[pos + 1]] = inputVal
            pos+=2
            continue
        
        # Single getter, no setter
        if len(modes) < 1 or modes[0] == 0:
            param1 = compList[compList[pos+1]]
        elif modes[0] == 1:
            param1 = compList[pos+1]
        
        # outputs the value of its only parameter
        if (operation == 4):
            print("Output: " + str(param1))
            pos+=2
            continue

        # Two getters, no setter
        if len(modes) < 2 or modes[1] == 0:
            param2 = compList[compList[pos+2]]
        elif modes[1] == 1:
            param2 = compList[pos+2]

        # jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
        if (operation == 5):
            if param1 != 0:
                pos = param2
            else:
                pos+=3
            continue

        # jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
        if (operation == 6):
            if param1 == 0:
                pos = param2
            else:
                pos+=3
            continue
        
        # Two getters, one setter
        # adds together numbers read from two positions and stores the result in a third position
        if (operation == 1):
            compList[compList[pos+3]] = param1 + param2
            pos+=4
            continue

        # multiplies together numbers read from two positions and stores the result in a third position
        if (operation == 2):
            compList[compList[pos+3]] = param1 * param2
            pos+=4
            continue

        # less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        if (operation == 7):
            compList[compList[pos+3]] = 1 if param1 < param2 else 0
            pos+=4
            continue

        # equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        if (operation == 8):
            compList[compList[pos+3]] = 1 if param1 == param2 else 0
            pos+=4
            continue
        
    return compList