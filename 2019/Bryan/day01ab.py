#!/usr/bin/python


def calcmass(a):
    ''' run sum and output results
    this seems a little underoptimized to me, but maybe not
    ''' 
    ans = int(int(a)/3) - 2
    if ans < 0:
        return 0
    else:
        return ans

def processlist(withrecursion=True):
    ''' open advent01.txt and perform math on each line
    '''
    mysum = 0
    config = open('advent01.txt').readlines()
    for modulemass in config:
        # modulemass = modulemass.rstrip('\n') # int takes care of this in calcmass()
        thisfuel = calcmass(modulemass)
        mysum += calcmass(modulemass)
        # this recursion could probably be a little cleaner
        if (withrecursion):
            while (thisfuel > 0):
                thisfuel = calcmass(thisfuel)
                mysum += thisfuel 
    return mysum

if __name__ == "__main__":
    # how do I roll these into true unit test?
    # print ("does 12==2=={}".format(calcmass(12))) 
    # print ("does 14==2=={}".format(calcmass(14))) 
    # print ("does 1969==654=={}".format(calcmass(1969))) 
    # print ("does 100756==33583=={}".format(calcmass(100756))) 
    print ("A is {}".format(processlist(False)))
    print ("B is {}".format(processlist()))

