total = 0

fuel = input("Mass: ")

while fuel != '':
    currentF = int(fuel)
    while currentF > 0:
        currentF = int(currentF/3) - 2
        if currentF > 0:
            total += currentF
    fuel = input("Mass: ")

print(total)