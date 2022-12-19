from operator import itemgetter


def create_sensor_and_beacon_list_from_input():
    sensor_list = list()
    beacon_list = list()
    with open('inputs/d15.txt', 'r') as file:
        for line in file:
            line = line.split(' ')
            x_sensor = int(line[2][2:-1])
            y_sensor = int(line[3][2:-1])
            x_beacon = int(line[-2][2:-1])
            y_beacon = int(line[-1][2:-1])
            manhattan_distance = abs(x_sensor - x_beacon) + abs(y_sensor - y_beacon)
            sensor_list.append((x_sensor, y_sensor, manhattan_distance))
            beacon_list.append((x_beacon, y_beacon))

    return sensor_list, beacon_list


sensors, beacons = create_sensor_and_beacon_list_from_input()


def part1(row=2000000):
    no_becon_possible = set()
    for sensor in sensors:
        distance_to_row = abs(sensor[1] - row)
        differnce_from_radius = sensor[2] - distance_to_row
        if differnce_from_radius >= 0:
            positions_in_range = set(range(-differnce_from_radius + sensor[0], differnce_from_radius + 1 + sensor[0]))
            no_becon_possible = no_becon_possible.union(positions_in_range)
    for beacon in beacons:
        if beacon[1] == row:
            no_becon_possible.discard(beacon[0])
    return no_becon_possible


def part2():
    maximum_coordinate = 4000000
    minimum_coordinate = 0

    for row in range(minimum_coordinate, maximum_coordinate + 1):
        if row % 100000 == 0:
            print(row)
        intervalls = []
        for sensor in sensors:
            distance_to_row = abs(sensor[1] - row)
            differnce_from_radius = sensor[2] - distance_to_row
            if differnce_from_radius >= 0:
                lower_bound = max(minimum_coordinate, -differnce_from_radius + sensor[0])
                upper_bound = min(maximum_coordinate, differnce_from_radius + sensor[0])
                intervalls.append((lower_bound, upper_bound))
        # sorted(intervalls ,key=lambda tup: tup[0])
        intervalls.sort(key=itemgetter(0)) # schneller als lambda

        if intervalls[0][0] != 0:
            return row
        continius_reachable = intervalls[0][1]
        for i in range(len(intervalls) - 1):
            if continius_reachable >= intervalls[i + 1][0]:
                continius_reachable = max(intervalls[i + 1][1], continius_reachable)
        if continius_reachable != maximum_coordinate:
            return (continius_reachable + 1) * 4000000 + row

    return 0


if __name__ == "__main__":
#    print(len(part1()))
    print(part2())