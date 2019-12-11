import computer2
from operator import itemgetter

panels = [[0,0,1,'notpainted']]

compList = [3,8,1005,8,310,1106,0,11,0,0,0,104,1,104,0,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,1002,8,1,28,1,105,11,10,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,0,10,4,10,102,1,8,55,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,1001,8,0,76,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,102,1,8,98,1,1004,7,10,1006,0,60,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,1002,8,1,127,2,1102,4,10,1,1108,7,10,2,1102,4,10,2,101,18,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,0,10,4,10,102,1,8,166,1006,0,28,3,8,1002,8,-1,10,101,1,10,10,4,10,108,1,8,10,4,10,101,0,8,190,1006,0,91,1,1108,5,10,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,1,10,4,10,1002,8,1,220,1,1009,14,10,2,1103,19,10,2,1102,9,10,2,1007,4,10,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,1,10,4,10,101,0,8,258,2,3,0,10,1006,0,4,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,1001,8,0,286,1006,0,82,101,1,9,9,1007,9,1057,10,1005,10,15,99,109,632,104,0,104,1,21102,1,838479487636,1,21102,327,1,0,1106,0,431,21102,1,932813579156,1,21102,1,338,0,1106,0,431,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21101,0,179318033447,1,21101,385,0,0,1105,1,431,21101,248037678275,0,1,21101,0,396,0,1105,1,431,3,10,104,0,104,0,3,10,104,0,104,0,21101,0,709496558348,1,21102,419,1,0,1105,1,431,21101,825544561408,0,1,21101,0,430,0,1106,0,431,99,109,2,22101,0,-1,1,21101,40,0,2,21102,462,1,3,21101,0,452,0,1106,0,495,109,-2,2105,1,0,0,1,0,0,1,109,2,3,10,204,-1,1001,457,458,473,4,0,1001,457,1,457,108,4,457,10,1006,10,489,1101,0,0,457,109,-2,2106,0,0,0,109,4,2101,0,-1,494,1207,-3,0,10,1006,10,512,21101,0,0,-3,22101,0,-3,1,22101,0,-2,2,21101,1,0,3,21102,531,1,0,1105,1,536,109,-4,2105,1,0,109,5,1207,-3,1,10,1006,10,559,2207,-4,-2,10,1006,10,559,22101,0,-4,-4,1106,0,627,21202,-4,1,1,21201,-3,-1,2,21202,-2,2,3,21102,578,1,0,1105,1,536,22101,0,1,-4,21101,1,0,-1,2207,-4,-2,10,1006,10,597,21102,0,1,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,619,21201,-1,0,1,21102,1,619,0,105,1,494,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2106,0,0]

def findCoord(list, x, y):
    for i in range(len(list)):
        if list[i][0] == x and list[i][1] == y:
            return i
    return -1

def rotate(facing, degree):
    if facing == 'up' and degree == 0:
        return 'left'
    if facing == 'up' and degree == 1:
        return 'right'
    if facing == 'right' and degree == 0:
        return 'up'
    if facing == 'right' and degree == 1:
        return 'down'
    if facing == 'left' and degree == 0:
        return 'down'
    if facing == 'left' and degree == 1:
        return 'up'
    if facing == 'down' and degree == 0:
        return 'right'
    if facing == 'down' and degree == 1:
        return 'left'
    return

output = ["input", 0]
panelColor = 1
pos = 0
baseVal = 0
curInd = 0
facing = 'up'
count = 0
while output[0] != "done":
    compList, output, pos, baseVal = computer2.comp(compList, ["input",panelColor], pos, baseVal)
    if output[1] == 0:
        panels[curInd][2] = 0
    elif output[1] == 1:
        panels[curInd][2] = 1
    panels[curInd][3] = 'painted'
    compList, output, pos, baseVal = computer2.comp(compList, ["noinput",panelColor], pos, baseVal)
    curX = panels[curInd][0]
    curY = panels[curInd][1]
    facing = rotate(facing, output[1])
    if facing == 'up':
        newCoord = [curX,curY+1]
    elif facing == 'down':
        newCoord = [curX,curY-1]
    elif facing == 'right':
        newCoord = [curX+1, curY]
    elif facing == 'left':
        newCoord = [curX-1, curY]

    coordPos = findCoord(panels, newCoord[0], newCoord[1])

    if coordPos == -1:
        panels.append([newCoord[0], newCoord[1], 0, 'unpainted'])
        curInd = len(panels) - 1
    else:
        curInd = coordPos
    panelColor = panels[curInd][2]

count = 0
for i in panels:
    if i[3] == 'painted':
        count+=1
print(count)

newList = sorted(sorted(panels, key = itemgetter(0)), key = itemgetter(1), reverse=True)

curVal = 0
image = []
line = ""
for i in newList:
    if i[1] == curVal:
        line += ' ' if i[2] == 0 else '#'
    else:
        image.append(line)
        line = ""
        if i[0] == 1:
            line+=' '
        line += ' ' if i[2] == 0 else '#'
        curVal = i[1]

for i in image:
    print(i)
