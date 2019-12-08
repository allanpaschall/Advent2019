import computer, types

# inputList = [3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0]
# inputList = [3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0]
# inputList = [3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0]
# inputList = [3,8,1001,8,10,8,105,1,0,0,21,34,51,76,101,126,207,288,369,450,99999,3,9,102,4,9,9,1001,9,2,9,4,9,99,3,9,1001,9,2,9,1002,9,3,9,101,3,9,9,4,9,99,3,9,102,5,9,9,1001,9,2,9,102,2,9,9,101,3,9,9,1002,9,2,9,4,9,99,3,9,101,5,9,9,102,5,9,9,1001,9,2,9,102,3,9,9,1001,9,3,9,4,9,99,3,9,101,2,9,9,1002,9,5,9,1001,9,5,9,1002,9,4,9,101,5,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,99]

inputList = [3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5]

signal = 0
settings = []

# for i in range(5):
#     a = 0
#     aPhase, a = computer.comp(inputList, [i,0])
#     for j in range(5):
#         if j != i:
#             b = 0
#             bPhase, b = computer.comp(inputList, [j,a])
#             for k in range(5):
#                 if k != i and k != j:
#                     c = 0
#                     cPhase, c = computer.comp(inputList, [k,b])
#                     for l in range(5):
#                         if l != i and l != j and l != k:
#                             d = 0
#                             dPhase, d = computer.comp(inputList, [l,c])
#                             # dPhase, d = computer.comp(inputList, c)
#                             for m in range(5):
#                                 if m != i and m != j and m != k and m != l:
#                                     e = 0
#                                     ePhase, e = computer.comp(inputList, [m,d])
#                                     # ePhase, e = computer.comp(inputList, d)
#                                     if e > signal:
#                                         signal = e
#                                         settings = [i,j,k,l,m]

# print(signal)
# print(settings)

finalAnswer = 0

for i in range(5, 10):
    for j in range(5, 10):
        if j != i:
            for k in range(5, 10):
                if k != i and k != j:
                    for l in range(5, 10):
                        if l != i and l != j and l != k:
                            for m in range(5, 10):
                                if m != i and m != j and m != k and m != l:
                                    a = 0
                                    b = 0
                                    c = 0
                                    d = 0
                                    e = 0
                                    firstInput =[0,0,0,0,0]
                                    answer = 0
                                    ePhase = inputList
                                    while ePhase != []:
                                        if firstInput[0]:
                                            print("other a")
                                            aPhase, a = computer.comp(aPhase, [e])
                                        else:
                                            print("first a")
                                            aPhase, a = computer.comp(inputList, [i,0])
                                            firstInput[0]=1
                                        if firstInput[1]:
                                            print("other b")
                                            bPhase, b = computer.comp(bPhase, [a])
                                        else:
                                            print("first b")
                                            bPhase, b = computer.comp(inputList, [j,a[0]])
                                            firstInput[1] = 1
                                        if firstInput[2]:
                                            print("other c")
                                            cPhase, c = computer.comp(cPhase, [b])
                                        else:
                                            print("first c")
                                            cPhase, c = computer.comp(inputList, [k,b[0]])
                                            firstInput[2] = 1
                                        if firstInput[3]:
                                            print("other d")
                                            dPhase, d = computer.comp(dPhase, [c])
                                        else:
                                            print("first d")
                                            dPhase, d = computer.comp(inputList, [l,c[0]])
                                            firstInput[3] = 1
                                        if firstInput[4]:
                                            print("other e")
                                            ePhase, e = computer.comp(ePhase, [d])
                                        else:
                                            print("first e")
                                            ePhase, e = computer.comp(inputList, [m,d[0]])
                                            firstInput[4]=1
                                        print(e)
                                    if len(e) == 1 and e[0] > signal:
                                        signal = e
                                        settings = [i,j,k,l,m]

print(signal)