#!/usr/bin/python

# read file into input line
# restore initial values:
#   12 at pos 1
#   2 at pos 2
# go through line by 4's, starting at 0
# operate on 1 and 2's and mention it
# end when encounter 99
#  What value is left at position 0 after the program halts?

# part b
# desired output mydata[0]==19690720


def resetdata():
    return[1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,2,19,6,23,2,13,23,27,1,9,27,31,2,31,9,35,1,6,35,39,2,10,39,43,1,5,43,47,1,5,47,51,2,51,6,55,2,10,55,59,1,59,9,63,2,13,63,67,1,10,67,71,1,71,5,75,1,75,6,79,1,10,79,83,1,5,83,87,1,5,87,91,2,91,6,95,2,6,95,99,2,10,99,103,1,103,5,107,1,2,107,111,1,6,111,0,99,2,14,0,0]

def elvintcode(a,b,c,d,mydata):
    if (a == 1):
        mydata[d] = mydata[b] + mydata[c]
    elif (a == 2):
        mydata[d] = mydata[b] * mydata[c]

def processlist(mydata):
    by4s = 0
    lenmydata = len(mydata)
    for a in range(0,lenmydata,4):
        if mydata[a+3] > lenmydata: # impossible array update location
            return 0 # fail fast, and with something that will not match a winner
        elif (0 < mydata[a] < 3):
            elvintcode( mydata[a], mydata[a+1], mydata[a+2], mydata[a+3], mydata)
        elif (mydata[a] == 99):
            return mydata[0]

def processb():
    for n in range(100):
        for v in range(100):
            mydata = resetdata()
            mydata[1] = n
            mydata[2] = v
            if (19690720 == processlist(mydata) ):
                return 100 * n + v

if __name__ == "__main__":
    mydata = []
    mydata = resetdata()
    mydata[1] = 12
    mydata[2] = 2
    print ("A pos 0 is {}".format(processlist(mydata))) # 3716293 is correct

    mydata = resetdata()
    print ("B 100 * noun + verb = {}".format(processb())) # 6429 is correct

