
total_fuel = 0

File.open('input1.txt') do |file|
  file.each_line do |mass|
    total_fuel += (mass.to_f / 3.0).floor - 2
  end
end

puts total_fuel
