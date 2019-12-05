def comp(compList, inputVal):
    compList = compList.copy()
    pos = 0
    while pos <= len(compList)-1:
        # print(pos)
        op = str(compList[pos])
        # print("Operation: " + op)
        operation = 98
        if len(op) > 1:
            operation = int(op[-2:])
        else:
            operation = int(op)
        if (operation == 1):
            val1Mode = 0
            val2Mode = 0
            if (len(str(op))>2):
                modes = str(op)[:-2]
                if len(modes) == 1:
                    val1Mode = int(modes)
                elif len(modes) == 2:
                    val1Mode = int(modes[1])
                    val2Mode = int(modes[0])
                else:
                    val1Mode = int(modes[2])
                    val2Mode = int(modes[1])
            param1 = compList[compList[pos + 1]] if val1Mode == 0 else compList[pos + 1]
            param2 = compList[compList[pos + 2]] if val2Mode == 0 else compList[pos + 2]
            # print("Add param 1: " + str(param1))
            # print("Add param 2: " + str(param2))
            try:
                compList[compList[pos+3]] = param1 + param2
                # print("Total: " + str(compList[compList[pos+3]]))
            except:
                return [0]
            pos+=4
        elif (operation == 2):
            val1Mode = 0
            val2Mode = 0
            if (len(str(op))>2):
                modes = str(op)[:-2]
                if len(modes) == 1:
                    val1Mode = int(modes)
                elif len(modes) == 2:
                    val1Mode = int(modes[1])
                    val2Mode = int(modes[0])
                else:
                    val1Mode = int(modes[2])
                    val2Mode = int(modes[1])
            param1 = compList[compList[pos + 1]] if val1Mode == 0 else compList[pos + 1]
            param2 = compList[compList[pos + 2]] if val2Mode == 0 else compList[pos + 2]
            # print("Mul param 1: " + str(param1))
            # print("Mul param 2: " + str(param2))
            try:
                compList[compList[pos+3]] = param1 * param2
                # print("Total: " + str(compList[compList[pos+3]]))
            except:
                return [0]
            pos+=4
        elif (operation == 3):
            compList[compList[pos + 1]] = inputVal
            pos+=2
        elif (operation == 4):
            valMode = 0
            if (len(str(op))>2):
                valMode = int(str(op)[0])
            # print("valmode: "+str(valMode))
            if valMode == 0:
                print("Output: " + str(compList[compList[pos + 1]]))
                # if(compList[compList[pos + 1]] != 0):
                #     print("Output: " + str(compList[:pos + 1]))
                #     return
            elif valMode == 1:
                print("Output: " + str(compList[pos+1]))
                # if(compList[pos+1] != 0):
                #     print(compList[:pos + 2])
                #     return
            pos+=2
        elif (operation == 5):
            val1Mode = 0
            val2Mode = 0
            if len(op) > 2:
                modes = op[:-2]
                if len(modes) == 1:
                    val1Mode = int(modes)
                elif len(modes) == 2:
                    val1Mode = int(modes[1])
                    val2Mode = int(modes[0])
            param1 = compList[compList[pos+1]] if val1Mode == 0 else compList[pos+1]
            param2 = compList[compList[pos+2]] if val2Mode == 0 else compList[pos+2]
            if param1 != 0:
                pos = param2
            else:
                pos+=3
        elif (operation == 6):
            val1Mode = 0
            val2Mode = 0
            if len(op) > 2:
                modes = op[:-2]
                if len(modes) == 1:
                    val1Mode = int(modes)
                elif len(modes) == 2:
                    val1Mode = int(modes[1])
                    val2Mode = int(modes[0])
            param1 = compList[compList[pos+1]] if val1Mode == 0 else compList[pos+1]
            param2 = compList[compList[pos+2]] if val2Mode == 0 else compList[pos+2]
            if param1 == 0:
                pos = param2
            else:
                pos+=3
        elif (operation == 7):
            val1Mode = 0
            val2Mode = 0
            if (len(str(op))>2):
                modes = str(op)[:-2]
                if len(modes) == 1:
                    val1Mode = int(modes)
                elif len(modes) == 2:
                    val1Mode = int(modes[1])
                    val2Mode = int(modes[0])
                else:
                    val1Mode = int(modes[2])
                    val2Mode = int(modes[1])
            param1 = compList[compList[pos + 1]] if val1Mode == 0 else compList[pos + 1]
            param2 = compList[compList[pos + 2]] if val2Mode == 0 else compList[pos + 2]
            compList[compList[pos+3]] = 1 if param1 < param2 else 0
            pos+=4
        elif (operation == 8):
            val1Mode = 0
            val2Mode = 0
            if (len(str(op))>2):
                modes = str(op)[:-2]
                if len(modes) == 1:
                    val1Mode = int(modes)
                elif len(modes) == 2:
                    val1Mode = int(modes[1])
                    val2Mode = int(modes[0])
                else:
                    val1Mode = int(modes[2])
                    val2Mode = int(modes[1])
            param1 = compList[compList[pos + 1]] if val1Mode == 0 else compList[pos + 1]
            param2 = compList[compList[pos + 2]] if val2Mode == 0 else compList[pos + 2]
            compList[compList[pos+3]] = 1 if param1 == param2 else 0
            pos+=4
        elif (operation == 99):
            return compList
    return compList