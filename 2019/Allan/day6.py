input = [["COM","B",],
["B","C",],
["C","D",],
["D","E",],
["E","F",],
["B","G",],
["G","H",],
["D","I",],
["E","J",],
["J","K",],
["K","L"]]

class node(object):
    def __init__(self, name):
        self.name = name
        self.orbits = ""
        self.orbiters = []
        self.parents = 0

nodes = []
nodenames = []
for i in input:

    #if the basenode isn't in the list of nodes, make a node and add it to 
    #both the list of nodes and nodenames
    if i[0] not in nodenames:
        basenode = node(i[0])
        nodes.append(basenode)
        nodenames.append(basenode.name)
    
    #node exists, pick it out of the list
    else:
        basenode = [node for node in nodes if node.name == i[0]][0]


    #if the orbitingnode isn't in the list of nodes, make a node and add it to 
    #both the list of nodes and nodenames
    if i[1] not in nodenames:
        orbitingnode = node(i[1])
        nodes.append(orbitingnode)
        nodenames.append(orbitingnode.name)
    
    #node exists, pick it out of the list
    else:
        orbitingnode = [node for node in nodes if node.name == i[1]][0]

    #using the new or old node, add the base to the orbiter and the orbiter to the base
    basenode.orbiters.append(orbitingnode)
    orbitingnode.orbits = basenode
        
#for node in nodes:
#    print(node.name)

    #print("orbits", [orbit.name for orbit in node.orbits])
    #print("orbiters", [orbiter.name for orbiter in node.orbiters])
        
def countparents(node, nodes):
    if node.orbits != "":
        node.parents += 1
        nextnode = [i for i in nodes if i.name == node.orbits.name][0]
        countparents(nextnode, nodes)
    else:
        return

total = 0
#reverse so we start at the end of the list
nodes.reverse()
for node in nodes:
    countparents(node, nodes)
    total += node.parents
print(total)