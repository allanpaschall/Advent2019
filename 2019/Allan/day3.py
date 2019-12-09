data = [["R8","U5","L5","D3"],["U7","R6","D4","L4"]]

# data = ["R8","U5","L5","D3"]

#line1 = 0,0 8,0 8,5 3,5 3,2
#line2 = 0,0 0,7 6,7 6,3 2,3

lines = []

for line in data:
    for i in line:
        templine = [[0,0]]
        if i[0] == "R":
            for j in range(1, int(i[1:])+1):
                templine.append([templine[-1][0]+1,templine[-1][1]])
        elif i[0] == "L":
            for j in range(1, int(i[1:])+1):
                templine.append([templine[-1][0]-1,templine[-1][1]])
        elif i[0] == "U":
            for j in range(1, int(i[1:])+1):
                templine.append([templine[-1][0],templine[-1][1]+1])
        elif i[0] == "D":
            for j in range(1, int(i[1:])+1):
                templine.append([templine[-1][0],templine[-1][1]-1])     
        lines.append(templine)
for ln in lines:
    print(ln)