using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AoC2019
{
    public class Field
    {
        public FieldPoint[] Items { get; set; }
        public int maxY { get; set; }
        public int maxX { get; set; }
        public List<FieldPaths> FieldPaths { get; private set; }
        public Field(string input)
        {
            FieldPaths = new List<FieldPaths>();
            string[] lines = input.Replace("\r","").Split('\n');
            maxY = lines.Length;
            maxX = lines[0].Length;
            Items = new FieldPoint[maxX * maxY];
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var Letter = lines[y][x];
                    Items[y * maxX + x] = new FieldPoint()
                    {
                        X = x,
                        Y = y,
                        Letter = Letter,
                        Kind = (Letter=='.' ? FieldPointKind.OpenSpace :
                                Letter=='#' ? FieldPointKind.Wall : 
                                Letter=='@' ? FieldPointKind.Entrance : 
                                "abcdefghijklmnopqrstuvwxys".IndexOf(Letter)!=-1 ? FieldPointKind.KeyLost : 
                                FieldPointKind.DoorClosed)
                    };
                }
            }
        }

        public void CalculatePath(FieldPoint from)
        {
            //as a way to speed up the process, pre-calculate the distance from one point to the next, 
            //and keep track of what doors are in the way to do so.
            var ItemsCopy = new FieldPoint[Items.Length];
            for (int i = 0; i < ItemsCopy.Length; i++)
            {
                ItemsCopy[i] = new FieldPoint()
                {
                    X = Items[i].X,
                    Y = Items[i].Y,
                    Kind = Items[i].Kind,
                    Letter = Items[i].Letter,
                    LettersCrossed = Items[i].LettersCrossed
                };
            }
            for (int i = 0; i < ItemsCopy.Length; i++)
            {
                if (ItemsCopy[i].Kind== FieldPointKind.Entrance)
                {
                    ItemsCopy[i].Kind = FieldPointKind.OpenSpace;
                    break;
                }
            }
            ItemsCopy[(from.Y * maxX) + from.X].Kind = FieldPointKind.Entrance;
            var Wall = new FieldPoint() { Kind = FieldPointKind.Wall, Letter = '#' };
            bool found = false;
            int count = 0;
            do
            {
                found = false;
                count++;
                for (int py = 0; py < maxY; py++)
                {
                    for (int px = 0; px < maxX; px++)
                    {
                        var item = ItemsCopy[(py * maxX) + px];
                        if (item.Kind != FieldPointKind.Wall && item.Kind != FieldPointKind.Flood && item.Kind != FieldPointKind.Entrance)
                        {
                            //at this point it is a key, door, or open space.

                            var itemAbove = py != 0 ? ItemsCopy[(py - 1) * maxX + px] : Wall;
                            var itemBelow = py + 1 != maxY ? ItemsCopy[(py + 1) * maxX + px] : Wall;
                            var itemLeft = px != 0 ? ItemsCopy[py * maxX + px - 1] : Wall;
                            var itemRight = px + 1 != maxX ? ItemsCopy[py * maxX + px + 1] : Wall;

                            if (itemAbove.Kind == FieldPointKind.Entrance)
                            {
                                item.LettersCrossed = itemAbove.LettersCrossed + itemAbove.Letter;

                                if (item.Kind == FieldPointKind.KeyLost)
                                {
                                    List<FieldPoint> doors = new List<FieldPoint>();
                                    foreach (var letter in item.LettersCrossed)
                                    {
                                        if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(letter)!=-1)
                                        {
                                            //unmistakeable a door.
                                            var door = (from d in Items where d.Letter == letter select d).First();
                                            doors.Add(door);
                                        }
                                    }
                                    FieldPaths.Add(new AoC2019.FieldPaths()
                                    {
                                        From = from,
                                        To = Items[(py * maxX) + px],
                                        Count = count,
                                        Doors = doors.ToArray()
                                        //Doors is not in there. How to do that??
                                    });
                                }
                                item.Kind = FieldPointKind.Flood;
                                found = true;
                            }
                            else if (itemBelow.Kind == FieldPointKind.Entrance)
                            {
                                item.LettersCrossed = itemBelow.LettersCrossed + itemBelow.Letter;

                                if (item.Kind == FieldPointKind.KeyLost)
                                {
                                    List<FieldPoint> doors = new List<FieldPoint>();
                                    foreach (var letter in item.LettersCrossed)
                                    {
                                        if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(letter) != -1)
                                        {
                                            //unmistakeable a door.
                                            var door = (from d in Items where d.Letter == letter select d).First();
                                            doors.Add(door);
                                        }
                                    }
                                    FieldPaths.Add(new AoC2019.FieldPaths()
                                    {
                                        From = from,
                                        To = Items[(py * maxX) + px],
                                        Count = count,
                                        Doors = doors.ToArray()
                                        //Doors is not in there. How to do that??
                                    });
                                }
                                item.Kind = FieldPointKind.Flood;
                                found = true;
                            }
                            else if (itemLeft.Kind == FieldPointKind.Entrance)
                            {
                                item.LettersCrossed = itemLeft.LettersCrossed + itemLeft.Letter;

                                if (item.Kind == FieldPointKind.KeyLost)
                                {
                                    List<FieldPoint> doors = new List<FieldPoint>();
                                    foreach (var letter in item.LettersCrossed)
                                    {
                                        if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(letter) != -1)
                                        {
                                            //unmistakeable a door.
                                            var door = (from d in Items where d.Letter == letter select d).First();
                                            doors.Add(door);
                                        }
                                    }
                                    FieldPaths.Add(new AoC2019.FieldPaths()
                                    {
                                        From = from,
                                        To = Items[(py * maxX) + px],
                                        Count = count,
                                        Doors = doors.ToArray()
                                        //Doors is not in there. How to do that??
                                    });
                                }
                                item.Kind = FieldPointKind.Flood;
                                found = true;
                            }
                            else if (itemRight.Kind == FieldPointKind.Entrance)
                            {
                                item.LettersCrossed = itemRight.LettersCrossed + itemRight.Letter;

                                if (item.Kind == FieldPointKind.KeyLost)
                                {
                                    List<FieldPoint> doors = new List<FieldPoint>();
                                    foreach (var letter in item.LettersCrossed)
                                    {
                                        if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(letter) != -1)
                                        {
                                            //unmistakeable a door.
                                            var door = (from d in Items where d.Letter == letter select d).First();
                                            doors.Add(door);
                                        }
                                    }
                                    FieldPaths.Add(new AoC2019.FieldPaths()
                                    {
                                        From = from,
                                        To = Items[(py * maxX) + px],
                                        Count = count,
                                        Doors = doors.ToArray()
                                        //Doors is not in there. How to do that??
                                    });
                                }
                                item.Kind = FieldPointKind.Flood;
                                found = true;
                            }
                        }
                    }
                }
                for (int i = 0; i < ItemsCopy.Length; i++)
                {
                    if (ItemsCopy[i].Kind == FieldPointKind.Flood)
                    {
                        ItemsCopy[i].Kind = FieldPointKind.Entrance;
                    }
                }
            } while (found == true);
        }
    }
    public class FieldPoint
    {
        public FieldPoint()
        {
            LettersCrossed = "";
        }
        public int X { get; set; }
        public int Y { get; set; }
        public FieldPointKind Kind { get; set; }
        public char Letter { get; set; }
        public string LettersCrossed { get; set; }//more for path calculation...
    }

    public class FieldPaths
    {
        public FieldPoint From { get; set; }
        public FieldPoint To { get; set; }
        public FieldPoint[] Doors { get; set; }
        public int Count { get; set; }
    }

    public enum FieldPointKind
    {
        Wall,
        OpenSpace,
        Entrance,
        Flood,
        KeyLost,
        KeyFound,
        DoorClosed,
        DoorOpened
    }
    public class Day18
    {
        int maxKeys = 0;
        public void Run()
        {
            var field = new Field(TestData5);
            maxKeys = (from f in field.Items where f.Kind == FieldPointKind.KeyLost select f).Count();
            foreach (FieldPoint f in from f in field.Items where f.Kind == FieldPointKind.Entrance || f.Kind == FieldPointKind.KeyLost select f)
            {
                field.CalculatePath(f);
            }
            int result = OpenDoors(field, (from f in field.Items where f.Kind == FieldPointKind.Entrance select f).First(), "","",0);
            Console.WriteLine("Day 18,P1:" + result);
            //Console.WriteLine("Hello");
        }

        private int OpenDoors(Field field, FieldPoint begin, string Doors, string keys, int level)
        {
            List<int> paths = new List<int>();
            foreach (var path in from path in field.FieldPaths
                                 where path.From == begin && 
                                    keys.IndexOf(path.To.Letter)==-1 &&
                                    (from d in path.Doors
                                        where d.Kind == FieldPointKind.DoorClosed && Doors.IndexOf(d.Letter)==-1
                                        select d).Count() == 0
                                 select path)
            {
                //count += path.Count;
                //Console.WriteLine("Path from " + path.From.Letter + " to " + path.To.Letter + ", took " + path.Count + " steps.");
                paths.Add(path.Count + OpenDoors(field, path.To, (Doors + path.To.Letter).ToUpper(), keys + path.To.Letter, level + 1));
            }
            if (level < maxKeys / 2)
            {
                Console.WriteLine("Level " + level + " best: " + (from p in paths orderby p select p).FirstOrDefault());
            }
            return (from p in paths orderby p select p).FirstOrDefault();
        }

        public string TestData1 = @"#########
#b.A.@.a#
#########";
        public string TestData2 = @"########################
#f.D.E.e.C.b.A.@.a.B.c.#
######################.#
#d.....................#
########################";
        public string TestData3 = @"########################
#...............b.C.D.f#
#.######################
#.....@.a.B.c.d.A.e.F.g#
########################";
        public string TestData4 = @"#################
#i.G..c...e..H.p#
########.########
#j.A..b...f..D.o#
########@########
#k.E..a...g..B.n#
########.########
#l.F..d...h..C.m#
#################";
        public string TestData5 = @"########################
#@..............ac.GI.b#
###d#e#f################
###A#B#C################
###g#h#i################
########################";
    }

}
