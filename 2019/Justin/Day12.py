m1 = [16, -11, 2, 0, 0, 0]
m2 = [0, -4, 7, 0, 0, 0]
m3 = [6, 4, -10, 0, 0, 0]
m4 = [-3, -2, -4, 0, 0, 0]

# m1 = [-8, -10, 0, 0, 0, 0]
# m2 = [5, 5, 10, 0, 0, 0]
# m3 = [2, -7, 3, 0, 0, 0]
# m4 = [9, -8, -3, 0, 0, 0]

# m1 = [-1, 0, 2, 0, 0, 0]
# m2 = [2, -10, -7, 0, 0, 0]
# m3 = [4, -8, 8, 0, 0, 0]
# m4 = [3, 5, -1, 0, 0, 0]

prev = []

def calc(pos1, positions):
    total = 0
    for i in positions:
        if pos1>i:
            total-=1
        elif pos1<i:
            total+=1
    return total

count = 0
newPrev = [m1.copy(),m2.copy(),m3.copy(),m4.copy()]
while newPrev not in prev:
    prev.append(newPrev)
    m1[3] += calc(m1[0],[m2[0],m3[0],m4[0]])
    m1[4] += calc(m1[1],[m2[1],m3[1],m4[1]])
    m1[5] += calc(m1[2],[m2[2],m3[2],m4[2]])

    m2[3] += calc(m2[0],[m1[0],m3[0],m4[0]])
    m2[4] += calc(m2[1],[m1[1],m3[1],m4[1]])
    m2[5] += calc(m2[2],[m1[2],m3[2],m4[2]])

    m3[3] += calc(m3[0],[m1[0],m2[0],m4[0]])
    m3[4] += calc(m3[1],[m1[1],m2[1],m4[1]])
    m3[5] += calc(m3[2],[m1[2],m2[2],m4[2]])

    m4[3] += calc(m4[0],[m1[0],m3[0],m2[0]])
    m4[4] += calc(m4[1],[m1[1],m3[1],m2[1]])
    m4[5] += calc(m4[2],[m1[2],m3[2],m2[2]])

    m1[0]+=m1[3]
    m1[1]+=m1[4]
    m1[2]+=m1[5]

    m2[0]+=m2[3]
    m2[1]+=m2[4]
    m2[2]+=m2[5]

    m3[0]+=m3[3]
    m3[1]+=m3[4]
    m3[2]+=m3[5]

    m4[0]+=m4[3]
    m4[1]+=m4[4]
    m4[2]+=m4[5]

    newPrev = [m1.copy(),m2.copy(),m3.copy(),m4.copy()]

    count+=1

m1Pot = abs(m1[0])+abs(m1[1])+abs(m1[2])
m1Kin = abs(m1[3])+abs(m1[4])+abs(m1[5])
m1Total = m1Pot*m1Kin

m2Pot = abs(m2[0])+abs(m2[1])+abs(m2[2])
m2Kin = abs(m2[3])+abs(m2[4])+abs(m2[5])
m2Total = m2Pot*m2Kin

m3Pot = abs(m3[0])+abs(m3[1])+abs(m3[2])
m3Kin = abs(m3[3])+abs(m3[4])+abs(m3[5])
m3Total = m3Pot*m3Kin

m4Pot = abs(m4[0])+abs(m4[1])+abs(m4[2])
m4Kin = abs(m4[3])+abs(m4[4])+abs(m4[5])
m4Total = m4Pot*m4Kin

# print(m1)
# print(m2)
# print(m3)
# print(m4)
# print(prev)
# print(newPrev)

print(m1Total+m2Total+m3Total+m4Total)
print(count)