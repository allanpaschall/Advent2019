#!/usr/bin/python
import sys
import itertools 

def resetdata():
    prog = open('advent08.txt').readlines()
    prog = prog[0].rstrip()
    return prog

def _countdigits(inp,x):
    ''' inp is a input string, x is the number to match
    '''
    cnt = 0
    for a in range(len(inp)):
        if int(inp[a]) == x:
            cnt += 1
    return cnt

def _layers(mydata,x,y):
    ans = []
    mydatalen = len(mydata)
    for a in range(0,mydatalen,x*y):
        ans.append(mydata[a:x*y+a])
    return ans

def findfewest0digits(mydata,x,y):
    minc = 100
    layer = []
    for a in _layers(mydata,x,y):
        #print ("ff0d: {}".format(a))
        c = _countdigits(a,0)
        if c < minc:
            minc = c
            layer = a
        # print("FND: {}\t{}".format(c,a))
    #print("minx={}\n{}".format(c,layer))
    return layer
        
def find1x2sonaline(layer):
    return _countdigits(layer,1) * _countdigits(layer,2)

def figureita(mydata,x,y):
    return find1x2sonaline(findfewest0digits(mydata,x,y))

def figureitb(mydata,x,y):
    layers = _layers(mydata,x,y)
    # print(layers)
    msg = ""
    for a in range(x*y):
        c = "2" # transparent falls down until not transparent
        b = 0
        while (2 == int(c)):
            c = layers[b][a]
            print("b={}, c={}, index={}".format(b,layers[b][a],a))
            b += 1
        msg = "{}{}".format(msg,c)
    print(msg)
    for a in range(y):
        print("{}".format(msg[x*a:y+x*a]))


if __name__ == "__main__":
    # combos()
    mydata = resetdata()
    print(figureita(mydata,25,6))
    # print(figureitb(mydata,25,6)) # screw it, done in excel and vi
    # A: 2176 is right for someone else...
