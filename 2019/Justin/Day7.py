import computer2, types

# inputList = [3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0]
# inputList = [3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0]
# inputList = [3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0]
inputList = [3,8,1001,8,10,8,105,1,0,0,21,34,51,76,101,126,207,288,369,450,99999,3,9,102,4,9,9,1001,9,2,9,4,9,99,3,9,1001,9,2,9,1002,9,3,9,101,3,9,9,4,9,99,3,9,102,5,9,9,1001,9,2,9,102,2,9,9,101,3,9,9,1002,9,2,9,4,9,99,3,9,101,5,9,9,102,5,9,9,1001,9,2,9,102,3,9,9,1001,9,3,9,4,9,99,3,9,101,2,9,9,1002,9,5,9,1001,9,5,9,1002,9,4,9,101,5,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,99]

# inputList = [3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5]
# inputList = [3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10]

signal = 0
settings = []

# for i in range(5):
#     a = 0
#     aPhase, a = computer22.comp(inputList, [i,0])
#     for j in range(5):
#         if j != i:
#             b = 0
#             bPhase, b = computer2.comp(inputList, [j,a])
#             for k in range(5):
#                 if k != i and k != j:
#                     c = 0
#                     cPhase, c = computer2.comp(inputList, [k,b])
#                     for l in range(5):
#                         if l != i and l != j and l != k:
#                             d = 0
#                             dPhase, d = computer2.comp(inputList, [l,c])
#                             # dPhase, d = computer2.comp(inputList, c)
#                             for m in range(5):
#                                 if m != i and m != j and m != k and m != l:
#                                     e = 0
#                                     ePhase, e = computer2.comp(inputList, [m,d])
#                                     # ePhase, e = computer2.comp(inputList, d)
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
                                    # print("m",m)
                                    a = 0
                                    b = 0
                                    c = 0
                                    d = 0
                                    e = ["input",20]
                                    positions = [0,0,0,0,0]
                                    firstInput =[0,0,0,0,0]
                                    answer = 0
                                    ePhase = inputList
                                    prevE =0 
                                    while e[0] != "done":
                                        print(positions)
                                        prevE = e[1]
                                        if firstInput[0]:
                                            aPhase, a, positions[0]= computer2.comp(aPhase, e, positions[0])
                                        else:
                                            print("a")
                                            aPhase, a, positions[0] = computer2.comp(inputList, ["input",i], positions[0])
                                            print("a2")
                                            aPhase, a, positions[0] = computer2.comp(aPhase, ["input",0], positions[0])
                                            firstInput[0]=1
                                        if firstInput[1]:
                                            bPhase, b, positions[1] = computer2.comp(bPhase, a, positions[1])
                                        else:
                                            print("b")
                                            bPhase, b, positions[1] = computer2.comp(inputList, ["input",j], positions[1])
                                            bPhase, b, positions[1] = computer2.comp(bPhase, a, positions[1])
                                            firstInput[1] = 1
                                        if firstInput[2]:
                                            cPhase, c, positions[2] = computer2.comp(cPhase, b, positions[2])
                                        else:
                                            cPhase, c, positions[2] = computer2.comp(inputList, ["input",k], positions[2])
                                            cPhase, c, positions[2] = computer2.comp(cPhase, b, positions[2])
                                            firstInput[2] = 1
                                        if firstInput[3]:
                                            dPhase, d, positions[3] = computer2.comp(dPhase, c, positions[3])
                                        else:
                                            dPhase, d, positions[3] = computer2.comp(inputList, ["input",l], positions[3])
                                            dPhase, d, positions[3] = computer2.comp(dPhase, c, positions[3])
                                            firstInput[3] = 1
                                        if firstInput[4]:
                                            ePhase, e, positions[4] = computer2.comp(ePhase, d, positions[4])
                                        else:
                                            ePhase, e, positions[4] = computer2.comp(inputList, ["input",m], positions[4])
                                            ePhase, e, positions[4] = computer2.comp(ePhase, d, positions[4])
                                            firstInput[4]=1
                                        # print(e)
                                    # print(prevE)
                                    if prevE>signal:
                                        signal = prevE
                                        settings = [i,j,k,l,m]

print(signal, settings)