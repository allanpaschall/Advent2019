import ast

line1 = input("Line 1: ")
line2 = input("Line 2: ")

line1list = line1.split(',')
line2list = line2.split(',')

line1coords = [[0,0]]
lastCoord = 0


for i in line1list:
    lastX = line1coords[lastCoord][0]
    lastY = line1coords[lastCoord][1]
    if i[0] == 'U':
        line1coords.append([lastX,lastY+int(i[1:])])
    elif i[0] == 'R':
        line1coords.append([lastX+int(i[1:]),lastY])
    elif i[0] == 'L':
        line1coords.append([lastX-int(i[1:]),lastY])
    elif i[0] == 'D':
        line1coords.append([lastX,lastY-int(i[1:])])
    lastCoord+=1

line2coords = [[0,0]]
lastCoord = 0

for i in line2list:
    lastX = line2coords[lastCoord][0]
    lastY = line2coords[lastCoord][1]
    if i[0] == 'U':
        line2coords.append([lastX,lastY+int(i[1:])])
    elif i[0] == 'R':
        line2coords.append([lastX+int(i[1:]),lastY])
    elif i[0] == 'L':
        line2coords.append([lastX-int(i[1:]),lastY])
    elif i[0] == 'D':
        line2coords.append([lastX,lastY-int(i[1:])])
    lastCoord+=1

def intersectPoint (p1, p2, q1, q2):
    x = 0
    y = 0
    if (p1[0] == p2[0]):
        x = p1[0]
    if (p1[1] == p2[1]):
        y = p1[1]
    if (q1[0] == q2[0]):
        x = q1[0]
    if (q1[1] == q2[1]):
        y = q1[1]
    return [x, y]


def onLine(p1, p2, p3):
    if(p2[0] <= max(p1[0], p3[0]) and p2[0] >= min(p1[0], p3[0])
        and p2[1] <= max(p1[1], p3[1]) and p2[1] >= min(p1[1], p3[1])):
        return True
    return False

intPoints = []

for i in range(len(line1coords)-1):
    for j in range(len(line2coords)-1):
        iPoint = intersectPoint(line1coords[i+1], line1coords[i], line2coords[j+1], line2coords[j])
        if onLine(line1coords[i], iPoint, line1coords[i+1]) and onLine(line2coords[j], iPoint, line2coords[j+1]):
            intPoints.append(iPoint)

def modifyList(list1):
    newList = []

    for i in list1:
        if i not in newList and i != [0,0]:
            newList.append(i)
    
    return newList

intPoints = modifyList(intPoints)

# mDist = 0

# for i in intPoints:
#     dist = abs(i[0])+abs(i[1])
#     if mDist == 0 or dist < mDist:
#         mDist = dist

# print(mDist)

line1steps = []
line2steps = []

for i in intPoints:
    steps = 0
    for j in range(len(line1coords)-1):
        if onLine(line1coords[j], i, line1coords[j+1]):
            steps += abs(i[0]-line1coords[j][0])+abs(i[1]-line1coords[j][1])
            break
        steps += abs(line1coords[j+1][0]-line1coords[j][0])+abs(line1coords[j+1][1]-line1coords[j][1])
    line1steps.append(steps)
    steps = 0
    for j in range(len(line2coords)-1):
        if onLine(line2coords[j], i, line2coords[j+1]):
            steps += abs(i[0]-line2coords[j][0])+abs(i[1]-line2coords[j][1])
            break
        steps += abs(line2coords[j+1][0]-line2coords[j][0])+abs(line2coords[j+1][1]-line2coords[j][1])
    line2steps.append(steps)

print(line1steps)
print(line1steps)
lowestSteps = 0
for i in range(len(line1steps)-1):
    val = line1steps[i] + line2steps[i]
    if lowestSteps == 0 or val < lowestSteps:
        lowestSteps = val

print(lowestSteps)
