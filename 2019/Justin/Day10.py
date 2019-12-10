from operator import itemgetter

asteroids = []

line = input("Line: ")

while line != "":
    asteroids.append(line)
    line = input("Line: ")

def isOpposite(seenX, seenY, homeX, homeY, newX, newY):
    if seenX < homeX < newX and  seenY < homeY < newY:
        return True
    if seenX == homeX == newX and  seenY < homeY < newY:
        return True
    if seenX > homeX > newX and  seenY < homeY < newY:
        return True
    if seenX < homeX < newX and seenY == homeY == newY:
        return True
    return False

def findSight(x, y, asteroids):
    newAsteroids = []
    homeX = 0
    homeY = 0
    for i in range(len(asteroids)):
        line = []
        for j in range(len(asteroids[i])):
            if j == x and i == y:
                line.append("h")
                homeX = j
                homeY = i
                continue
            if asteroids[i][j] == ".":
                line.append(".")
                continue
            if x-j == 0:
                line.append(str(len(asteroids)*100))
                continue
            if y-i == 0:
                line.append("0")
                continue
            line.append(str((y-i)/(x-j)))
        newAsteroids.append(line)
    count = 0
    seen = []
    seenAt = []
    for i in range(len(newAsteroids)):
        for j in range(len(newAsteroids[i])):
            if newAsteroids[i][j] == "h" or newAsteroids[i][j] == '.':
                continue
            if newAsteroids[i][j] != '.' and newAsteroids[i][j] not in seen:
                count+=1
                seen.append(newAsteroids[i][j])
                seenAt.append([j,i,1])
                continue
            index = seenAt[seen.index(newAsteroids[i][j])]
            if newAsteroids[i][j] in seen and index[2] == 1 and isOpposite(index[0], index[1], homeX, homeY, j, i):
                count += 1
                seenAt[seen.index(newAsteroids[i][j])][2] = 2

    return count, newAsteroids


highest = 0
x = 0
y = 0
newAsteroids = []
for i in range(len(asteroids)):
    for j in range(len(asteroids[i])):
        if asteroids[i][j] == '#':
            count, tempAsteroids = findSight(j, i, asteroids)
            if count > highest:
                newAsteroids = tempAsteroids
                highest = count
                x = j
                y = i
print(highest, "at", x, y)

maxY = len(asteroids)
maxX = len(asteroids[0])

toDestroy = []
print(newAsteroids)
while len(toDestroy) < 200:
    seen = []
    unorderedDestroy = []
    for i in range(x, maxX):
        for j in range(y, -1, -1):
            if newAsteroids[j][i] == 'h' or newAsteroids[j][i] == '.':
                continue
            if newAsteroids[j][i] not in seen and [i,j] not in toDestroy:
                seen.append(newAsteroids[j][i])
                unorderedDestroy.append([i,j,abs(float(newAsteroids[j][i]))])
    for i in sorted(unorderedDestroy, key = itemgetter(2), reverse = True):
        toDestroy.append([i[0],i[1]])
    
    # Quadrant 2
    seen = []
    unorderedDestroy = []
    for i in range(x, maxX):
        for j in range(y+1, maxY):
            if newAsteroids[j][i] == 'h' or newAsteroids[j][i] == '.':
                continue
            if newAsteroids[j][i] not in seen and [i,j] not in toDestroy:
                seen.append(newAsteroids[j][i])
                unorderedDestroy.append([i,j,newAsteroids[j][i]])
    for i in sorted(unorderedDestroy, key = itemgetter(2)):
        toDestroy.append([i[0],i[1]])
    
    # Quadrant 3
    seen = []
    unorderedDestroy = []
    for i in range(x-1, -1, -1):
        for j in range(y, maxY):
            if newAsteroids[j][i] == 'h' or newAsteroids[j][i] == '.':
                continue
            if newAsteroids[j][i] not in seen and [i,j] not in toDestroy:
                seen.append(newAsteroids[j][i])
                unorderedDestroy.append([i,j,newAsteroids[j][i]])
    for i in sorted(unorderedDestroy, key = itemgetter(2), reverse = True):
        toDestroy.append([i[0],i[1]])
    
    # Quadrant 4
    seen = []
    unorderedDestroy = []
    for i in range(x-1, -1, -1):
        for j in range(y-1, -1, -1):
            if newAsteroids[j][i] == 'h' or newAsteroids[j][i] == '.':
                continue
            if newAsteroids[j][i] not in seen and [i,j] not in toDestroy:
                seen.append(newAsteroids[j][i])
                unorderedDestroy.append([i,j,abs(float(newAsteroids[j][i]))])
    for i in sorted(unorderedDestroy, key = itemgetter(2)):
        toDestroy.append([i[0],i[1]])
print(toDestroy)
print(toDestroy[199][0]*100 + toDestroy[199][1])