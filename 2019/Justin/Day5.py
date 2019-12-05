import ast
import computer

listVals = input("List: ")

realList = list(ast.literal_eval(listVals))

print(computer.comp(realList, 5))
