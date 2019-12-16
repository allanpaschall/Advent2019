#You can clean up the signal with the Flawed Frequency Transmission algorithm, or FFT.

#FFT operates in repeated phases. In each phase, a new list is constructed with the same length as the input list. 
# This new list is also used as the input for the next phase.

#Each element in the new list is built by multiplying every value in the input list
#  by a value in a repeating pattern and then adding up the results. 
# So, if the input list were 9, 8, 7, 6, 5 and the pattern for a given element were 1, 2, 3,
#  the result would be 9*1 + 8*2 + 7*3 + 6*1 + 5*2 (with each input element on the left
#  and each value in the repeating pattern on the right of each multiplication). 
# Then, only the ones digit is kept: 38 becomes 8, -17 becomes 7, and so on.

#The base pattern is 0, 1, 0, -1. Then, repeat each value in the pattern a number of times
#  equal to the position in the output list being considered. 
# Repeat once for the first element, twice for the second element, three times for the third element, and so on. 
# So, if the third element of the output list is being calculated, 
# repeating the values would produce: 0, 0, 0, 1, 1, 1, 0, 0, 0, -1, -1, -1.

#When applying the pattern, skip the very first value exactly once. (In other words, 
# offset the whole pattern left by one.) So, for the second element of the output list,
#  the actual pattern used would be: 0, 1, 1, 0, 0, -1, -1, 0, 0, 1, 1, 0, 0, -1, -1, ....

#After using this process to calculate each element of the output list, the phase is complete,
#  and the output list of this phase is used as the new input list for the next phase, if any.

data = [int(i) for i in "12345678"]
print(data)

def phase(inputsignal, phasenumber, finalphase, newlist):
    basepattern = [0,1,0,-1]
    currpattern = []
    for i in range(len(inputsignal)):
        for j in range(1,phasenumber+1):
            currpattern.append(basepattern[i % len(basepattern)])
    currpattern = currpattern[1:]
    #print(currpattern)

    output = []
    total = 0
    if phasenumber <= finalphase:
        #do stuff, recursion
        for i in range(len(inputsignal)):
            #print(inputsignal[i], "*", currpattern[i%len(currpattern)])
            output.append(inputsignal[i] * currpattern[i % len(currpattern)])
        for i in output:
            total += i
    else:
        #don't do stuff.
        pass
    newlist.append(total)
    return(output, total, newlist)

print(phase(data,1,4,[]))

