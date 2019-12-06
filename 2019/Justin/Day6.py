class Planet:
    def __init__(self, name):
        self.name = name
        self.orbits = "base"

planets = []

planetInfo = input("Orbits: ")

def findPlanet(planName, planets):
    for i in range(len(planets)):
        if planets[i].name == planName:
            return i

while planetInfo != "":
    orbited, orbiter = planetInfo.split(')')
    if not any(x.name == orbiter for x in planets):
        exists = False
        planet = Planet(orbiter)
        for i in range(len(planets)):
            if planets[i].name == orbited:
                exists = True
                planet.orbits = i
                break
        if not exists:
            planets.append(Planet(orbited))
            planet.orbits=len(planets)-1

        planets.append(planet)
    else:
        orbiterInd = 0
        for i in range(len(planets)):
            if planets[i].name == orbiter:
                orbiterInd = i
                break
        exists = False
        for i in range(len(planets)):
            if planets[i].name == orbited:
                planets[orbiterInd].orbits = i
                exists = True
                break
        if not exists:
            planets.append(Planet(orbited))
            planets[orbiterInd].orbits = len(planets)-1
    planetInfo = input("Orbits: ")

count = 0

def counter(planet, planets):
    # print(planet.name + " " + str(planet.orbits))
    return 0 if planet.orbits == "base" else 1+counter(planets[planet.orbits], planets)

for i in planets:
    count+=counter(i, planets)

print(count)

you = ''
san = ''

for i in range(len(planets)):
    if planets[i].name == 'YOU':
        you = i
    if planets[i].name == 'SAN':
        san = i

youOrbits = []
sanOrbits = []

while planets[you].orbits != 'base':
    youOrbits.append(planets[you].name)
    you = planets[you].orbits

while planets[san].orbits != 'base':
    sanOrbits.append(planets[san].name)
    san = planets[san].orbits

jumps = 0
meetup = ''

for i in range(len(youOrbits)):
    if youOrbits[i] in  sanOrbits:
        jumps += i
        meetup = youOrbits[i]
        break

for i in range(len(sanOrbits)):
    if sanOrbits[i] == meetup:
        jumps += i
        break

print(jumps-2)
