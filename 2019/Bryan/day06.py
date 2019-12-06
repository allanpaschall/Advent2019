#!/usr/bin/python

def resetdata():
   prog = open('advent06.txt').readlines() 
   return prog

def takemetoyourleader(p,o):
    if (p == "COM"):
        # done, wrap it up
        return 1
    else:
        return 1 + takemetoyourleader(o[p],o)

def parsemydataintodictionary(mydata):
    o = {}
    for p in mydata:
        lr = p.rstrip().split(")")
        l = lr[0]
        r = lr[1]
        o[r] = l 
    return o

def processa(mydata):
    # for each item, count it's orbits back to the comm
    o=parsemydataintodictionary(mydata)
    orbits = 0
    for p in o.values():
        orbits += takemetoyourleader(p,o)
    return orbits

def calcpath(p,o):
    if (p == "COM"):
        return "COM"
    else:
        return "{}){}".format(o[p],calcpath(o[p],o))

def processb(mydata):
    # calculate the common orbital root
    # then caclulate how many for each of us to get there
    # not counting the orbit we are already around
    o=parsemydataintodictionary(mydata)
    i = calcpath("YOU",o)
    s = calcpath("SAN",o)
    # print "i,s:\n{}\n{}".format(i,s)
    while (i[-1] == s[-1]):
        i = i[:-1]
        s = s[:-1]
    # this gets it a little too clean, destroying the ) from the one common root, so +2 on there
    # print "i,s:{}{}".format(i,s)
    return "{}{}".format(i,s).count(")") + 2

if __name__ == "__main__":
    mydata = resetdata()
    print("A: {}".format(processa(mydata)))
    print("B: {}".format(processb(mydata)))
