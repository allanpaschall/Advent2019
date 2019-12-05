#!/usr/bin/env python

def calcFuelForWeight(modweight):
    fuel = 0
    fuel = ((int(modweight) / 3) - 2)
    totalfuel = [fuel]
    while (calcFuelForFuel(totalfuel[-1]) > 0):
        totalfuel.append(calcFuelForFuel(totalfuel[-1])) 
    return fuel,int((sum(totalfuel) - fuel))
    
def calcFuelForFuel(weight):
    fuelmass = 0
    fuelmass = ((int(weight) / 3) - 2)
    #print "Fuel needed for fuel = " + str(fuelmass)
    return fuelmass

if __name__ == '__main__':
    moduleList = [148319,54894,105685,136247,133339,91401,126939,102276,66395,134572,137988,65709,119909,98439,88926,74563,73275,111063,92623,66649,147991,71108,129082,58677,57884,93284,61455,110362,81726,146604,70037,82328,78802,69496,61390,134525,94583,73669,136417,80424,98700,88578,147217,108332,73965,116009,51599,55617,129014,51962,95443,114358,141826,134605,145837,112074,93422,112897,137077,141584,114605,122589,121933,67088,120788,53216,82633,55215,135617,91439,110237,130445,120865,109484,113596,133240,113525,110473,146059,129811,79370,50142,111149,137107,91647,82978,119456,51924,132979,63215,55209,114474,54585,101761,79878,63987,149324,100155,54601,115686]
    #moduleList = [raw_input("Module weight: ")]
    fuelNeededforWeight = []
    fuelNeededforFuel = []
    totModWeight = []
    for i in moduleList:
        fn, fff = calcFuelForWeight(i)
        print "Module weight = " + str(i) + "      Fuel needed = " + str(fn) + "        Fuel needed for fuel = " + str(fff)
        totModWeight.append(i)
        fuelNeededforWeight.append(fn)
        fuelNeededforFuel.append(fff)
    print sum(totModWeight)
    print sum(fuelNeededforWeight)
    print sum(fuelNeededforFuel)
    print "Total fuel needed   " + str(sum(fuelNeededforWeight) + sum(fuelNeededforFuel))