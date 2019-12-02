def comp(compList):
    pos = 0
    while pos <= len(compList)-1:
        op = compList[pos]
        if (op == 1):
            try:
                compList[compList[pos+3]] = compList[compList[pos + 1]] + compList[compList[pos + 2]]
            except:
                return [0]
        elif (op == 2):
            try:
                compList[compList[pos+3]] = compList[realList[pos + 1]] * compList[compList[pos + 2]]
            except:
                return [0]
        elif (op == 99):
            return compList
        pos+=4
    return compList