lower = 147981
upper = 691423

count = 0

def noDecrease(value):
    strVal = str(value)
    for i in range(len(strVal)-1):
        if int(strVal[i+1]) < int(strVal[i]):
            return False
    return True

def hasRepeat(value):
    strVal = str(value)
    repeats = []
    curInd = 0
    for i in range(len(strVal)-1):
        if int(strVal[i+1]) == int(strVal[i]):
            if repeats == []:
                repeats.append([int(strVal[i]), 2])
            elif repeats[curInd][0] != int(strVal[i]):
                repeats.append([int(strVal[i]), 2])
                curInd+=1
            else:
                repeats[curInd][1]+=1
    for i in repeats:
        if i[1] == 2:
            return True
    return False

while lower <= upper:
    if noDecrease(lower) and hasRepeat(lower):
        count += 1
    lower += 1

print(count)