with open('page7.txt') as f:
    for line in f:
        if ("To complete this KU, all Topics and sub-Topics must be completed" in line):
            print(line)