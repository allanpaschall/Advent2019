import ast
import computer

listVals = input("List: ")

realList = list(ast.literal_eval(listVals))

computer.comp(realList, 5)
