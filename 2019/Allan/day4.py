#must be 6 digits
#The value is within the range given in your puzzle input.
#Two adjacent digits are the same (like 22 in 122345).
#Going from left to right, the digits never decrease; 
# they only ever increase or stay the same (like 111123 or 135679).

beg = 235741
end = 706948

def meetscriteria(beg,end):
    possibilities = []
    for passcode in range(beg,end+1):
        passcode = str(passcode)
        doublenums = ["11","22","33","44","55","66","77","88","99","00"]
        funfsechs = ["111111","222222","333333","444444","555555","666666","777777","888888","999999","000000","11111","22222","33333","44444","55555","66666","77777","88888","99999","00000"]
        tripquad = ["1111","2222","3333","4444","5555","6666","7777","8888","9999","0000","111","222","333","444","555","666","777","888","999","000"]
        shortcircuit = False
        newpasscode = ""
        if passcode != "".join(sorted(passcode)):
            continue
        for fivesix in funfsechs:
            if passcode.find(fivesix) > -1:
                shortcircuit = True
        if shortcircuit:
            continue
        
        for trip in tripquad:
            newpasscode = passcode.replace(trip,"a")
            if newpasscode != passcode:
                for trip2 in tripquad:
                    newpasscode = newpasscode.replace(trip2,"a")
                break

        for dub in doublenums:
            if dub in newpasscode:
                if newpasscode != passcode:
                    print("dub found in newpass:", newpasscode)
                possibilities.append(passcode)
                break

    print(len(possibilities))
    # for i in possibilities:
    #     print(i)

meetscriteria(beg,end)