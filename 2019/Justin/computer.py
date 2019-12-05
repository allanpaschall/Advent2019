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
        
        if (operation == 1):
            param1 = compList[compList[pos + 1]] if len(modes) < 1 or modes[0] == 0 else compList[pos + 1]
            param2 = compList[compList[pos + 2]] if len(modes) < 2 or modes[1] == 0 else compList[pos + 2]
            try:
                compList[compList[pos+3]] = param1 + param2
            except:
                return [0]
            pos+=4
        elif (operation == 2):
            param1 = compList[compList[pos + 1]] if len(modes) < 1 or modes[0] == 0 else compList[pos + 1]
            param2 = compList[compList[pos + 2]] if len(modes) < 2 or modes[1] == 0 else compList[pos + 2]
            try:
                compList[compList[pos+3]] = param1 * param2
            except:
                return [0]
            pos+=4
        elif (operation == 3):
            compList[compList[pos + 1]] = inputVal
            pos+=2
        elif (operation == 4):
            if len(modes) < 1 or modes[0] == 0:
                print("Output: " + str(compList[compList[pos + 1]]))
            elif modes[0] == 1:
                print("Output: " + str(compList[pos+1]))
            pos+=2
        elif (operation == 5):
            param1 = compList[compList[pos+1]] if len(modes) < 1 or modes[0] == 0 else compList[pos+1]
            param2 = compList[compList[pos+2]] if len(modes) < 2 or modes[1] == 0 else compList[pos+2]
            if param1 != 0:
                pos = param2
            else:
                pos+=3
        elif (operation == 6):
            param1 = compList[compList[pos+1]] if len(modes) < 1 or modes[0] == 0 else compList[pos+1]
            param2 = compList[compList[pos+2]] if len(modes) < 2 or modes[1] == 0 else compList[pos+2]
            if param1 == 0:
                pos = param2
            else:
                pos+=3
        elif (operation == 7):
            param1 = compList[compList[pos + 1]] if len(modes) < 1 or modes[0] == 0 else compList[pos + 1]
            param2 = compList[compList[pos + 2]] if len(modes) < 2 or modes[1] == 0 else compList[pos + 2]
            compList[compList[pos+3]] = 1 if param1 < param2 else 0
            pos+=4
        elif (operation == 8):
            param1 = compList[compList[pos + 1]] if len(modes) < 1 or modes[0] == 0 else compList[pos + 1]
            param2 = compList[compList[pos + 2]] if len(modes) < 2 or modes[1] == 0 else compList[pos + 2]
            compList[compList[pos+3]] = 1 if param1 == param2 else 0
            pos+=4
        elif (operation == 99):
            return compList
    return compList