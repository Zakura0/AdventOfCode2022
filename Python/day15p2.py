import re

pattern = re.compile(r"\d+")

lines = [list(map(int, pattern.findall(line))) for line in open('inputs/d15.txt')]

area = 4000000

percent = 2.5
for row in range(area + 1):
    if row % 100000 == 0:
        print("At ", percent , " percent")
        percent += 2.5
    intervals = []

    for sensor_x, sensor_y, beacon_x, beacon_y in lines:
        distance = abs(sensor_x - beacon_x) + abs(sensor_y - beacon_y)
        offset = distance - abs(sensor_y - row)
        lower = sensor_x - offset
        upper = sensor_x + offset
        intervals.append((lower, upper))
    intervals.sort()
    merged_intervals = []
    for lower, upper in intervals:
        if not merged_intervals:
            merged_intervals.append([lower, upper])
            continue
        lowest, highest = merged_intervals[-1]
        if lower > highest + 1:
            merged_intervals.append([lower, upper])
            continue
        merged_intervals[-1][1] = max(highest, upper)
    x = 0
    for lower, upper in merged_intervals:
        if x < lower:
            print(x * 4000000 + row)
            exit(0)
        x = max(x, upper + 1)
        if x > area:
            break