import ast

listVals = input("List: ")

realList = list(ast.literal_eval(listVals))
newList = realList

def computer(compList):
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

val1 = 0
val2 = 0

while newList[0] != 19690720 and val2<=99:
    newList = realList.copy()
    newList[1] = val1
    newList[2] = val2
    print("Val1: "+ str(val1))
    print("Val2: " +str(val2))
    computer(newList)
    if val1 == 99:
        val1 = 0
        val2+=1
    else:
        val1+=1
    print("Pos 0: "+str(newList[0]))

print(100 * newList[1] + newList[2])