


def fuel_for_mass(mass)
  return 0 if mass < 9
  fuel = (mass / 3.0).floor - 2
  if fuel > 0
    fuel + fuel_for_mass(fuel)
  end
end

total_fuel = 0

File.open('input1.txt') do |file|
  file.each_line do |mass|
    total_fuel += fuel_for_mass(mass.to_f)
  end
end

puts total_fuel
