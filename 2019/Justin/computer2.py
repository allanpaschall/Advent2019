def comp(compList, inputVal, pos, baseVal):
    compList = compList.copy()
    # pos = 0
    while pos <= len(compList)-1:
        # print("position",pos)
        op = str(compList[pos])
        # print("op:",op)
        # print(compList)
        # print("base:",baseVal)
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
            return compList, ["done", 0], pos, baseVal

        # No getter, One setter
        

        # OP takes a single integer as input and saves it to the position given by its only parameter
        if (operation == 3):
            if len(modes) == 1 and modes[0] == 2:
                if baseVal + compList[pos + 1] > len(compList) - 1:
                    compList = expandMemory(compList,baseVal + compList[pos + 1])
                setter = baseVal + compList[pos + 1]
            else:
                if compList[pos + 1] > len(compList) - 1:
                    compList = expandMemory(compList,compList[pos + 1])
                setter = compList[pos + 1]
            # print("setter",setter)
            if inputVal[0] == "input":
                compList[setter] = inputVal[1]
                pos+=2
                inputVal[0] = "received"
            else:
                return compList, ["input needed",0], pos, baseVal
            continue
        
        # Single getter, no setter
        if len(modes) < 1 or modes[0] == 0:
            param1 = 0 if compList[pos+1] >len(compList) - 1 else compList[compList[pos+1]]
        elif modes[0] == 1:
            param1 = compList[pos+1]
        elif modes[0] == 2:
            param1 = 0 if baseVal + compList[pos+1] >len(compList) - 1 else compList[baseVal + compList[pos+1]]
        # print('param1',param1)

        # OP outputs the value of its only parameter
        if (operation == 4):
            print("Output: ", param1)
            # return compList, ["input",param1], pos+2, baseVal
            pos+=2
            continue
        
        if (operation == 9):
            baseVal += param1
            pos+=2
            continue

        # Two getters, no setter
        if len(modes) < 2 or modes[1] == 0:
            param2 = 0 if compList[pos+2] >len(compList) - 1 else compList[compList[pos+2]]
        elif modes[1] == 1:
            param2 =  compList[pos+2]
        elif modes[1] == 2:
            param2 =  0 if baseVal + compList[pos+2] > len(compList) - 1 else compList[baseVal + compList[pos+2]]
        # print("param 2", param2)

        # OP jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
        if (operation == 5):
            # print(op,":",param1)
            if param1 != 0:
                pos = param2
            else:
                pos+=3
            continue

        # OP jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
        if (operation == 6):
            if param1 == 0:
                pos = param2
            else:
                pos+=3
            continue
        
        # Two getters, one setter
        if len(modes) > 2 and modes[2] == 2:
            if baseVal + compList[pos + 3] > len(compList) - 1:
                compList = expandMemory(compList,baseVal + compList[pos + 3])
            setter = baseVal + compList[pos + 3]
        else:
            if compList[pos + 3] > len(compList) - 1:
                # print("Length case hit",compList[pos + 3], len(compList) - 1)
                compList = expandMemory(compList,compList[pos + 3])
            setter = compList[pos + 3]
        # print("setter",setter)
        # OP adds together numbers read from two positions and stores the result in a third position
        if (operation == 1):
            compList[setter] = param1 + param2
            pos+=4
            continue

        # OP multiplies together numbers read from two positions and stores the result in a third position
        if (operation == 2):
            # print(param1, param2)
            compList[setter] = param1 * param2
            pos+=4
            continue

        # OP less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        if (operation == 7):
            compList[setter] = 1 if param1 < param2 else 0
            pos+=4
            continue

        # OP equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
        if (operation == 8):
            compList[setter] = 1 if param1 == param2 else 0
            pos+=4
            continue
    print("broken") 
    return compList, ["broke",0], pos, baseVal

def expandMemory(compList, expansionNum):
    compList.copy()
    pos = len(compList)-1
    while pos<= expansionNum:
        compList.append(0)
        pos+=1
    return compList