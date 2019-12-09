#!/usr/bin/python
import sys
import itertools 

def resetdata():
    ans = []
    prog = open('advent07.txt').readlines()
    for a in prog[0].split(","):
        ans.append(int(a))
    return ans

def combos(mydata):
    listA = [0,1,2,3,4]
    perm = itertools.permutations(listA) 
    maxthrust = 0
    maxthrustcombo = []

    for i in list(perm): 
        # pass mydata, then an array of inputs from right to left (pop)
        # print("processed 0,{} ao={}".format(i[0],ao)) 
        ao = processa(mydata,[0,i[0]]) # A
        bo = processa(mydata,[ao,i[1]]) # B
        co = processa(mydata,[bo,i[2]]) # C
        do = processa(mydata,[co,i[3]]) # D
        eo = processa(mydata,[do,i[4]]) # E
        if eo > maxthrust:
            maxthrust = eo
            maxthrustcombo = i
    print ("Max with {} is {}".format(maxthrust,maxthrustcombo))


def processa(mydata,ins):

    lenmydata = len(mydata)
    i = 0
    while (i < lenmydata and mydata[i] != 99 ):
        # print(mydata)
        oc = mydata[i]%10
        if mydata[i] > 100 and int(mydata[i]/100)%10 == 1:
            p1 = 1 # immediate
        else:
            p1 = 0 # positional
        if mydata[i] > 1000:
            p2 = 1
        else:
            p2 = 0
        if (4 == oc):
            if p1:
                dout = mydata[i+1]
            else:
                dout = mydata[mydata[i+1]]
            #print("output: {}".format(dout))
            return dout
            i += 2
        elif (3 == oc):
            mydata[mydata[i+1]] = ins.pop()
            i += 2
        else:
            a = mydata[i+1]
            b = mydata[i+2]
            p = mydata[i+3] # always positional
            #print ("setof4 ocfull:{},a:{},b:{},p:{},|,p1:{},p2:{}".format(mydata[i],a,b,p,p1,p2))
            # 000oo
            if p1:
                av = a
            else:
                av = mydata[a]
            if p2:
                bv = b
            else:
                bv = mydata[b]
            #print ("av,bv={},{}".format(av,bv))
            if (1 == oc):
                mydata[p] = av + bv
                i += 4
            elif (2 == oc):
                mydata[p] = av * bv
                i += 4
            elif (5 == oc):
                # Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter.
                # Otherwise, it does nothing.
                if 0 != av:
                    i = bv
                else:
                    i += 3
            elif (6 == oc):
                #    Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter.
                # Otherwise, it does nothing.
                if 0 == av:
                    i = bv
                else:
                    i += 3
            elif (7 == oc):
                #    Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter.
                # Otherwise, it stores 0.
                if av < bv:
                    mydata[p] = 1
                    i += 4
                else:
                    mydata[p] = 0
                    i += 4
            elif (8 == oc):
                #    Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter.
                # Otherwise, it stores 0.
                if av == bv:
                    mydata[p] = 1
                    i += 4
                else:
                    mydata[p] = 0
                    i += 4
            else:
                i += 1 
if __name__ == "__main__":
    mydata = resetdata()
    #print (mydata)
    combos(mydata)
