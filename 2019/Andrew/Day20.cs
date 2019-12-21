using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace AoC2019.Day20
{
    public class Field
    {
        public int Level { get; set; }
        public FieldPoint[] Items { get; set; }
        public int maxY { get; set; }
        public int maxX { get; set; }
        public int maxKeys { get; set; }
        public List<FieldPaths> FieldPaths0 { get; private set; }
        public List<FieldPaths> FieldPaths1toN { get; private set; }
        public List<FieldPaths> FieldPathsN { get; private set; }
        public Field(string input)
        {
            FieldPaths0 = new List<FieldPaths>();
            FieldPaths1toN = new List<FieldPaths>();
            FieldPathsN = new List<FieldPaths>();
            string[] lines = input.Replace("\r", "").Split('\n');
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
                        Token = "" + Letter,
                        Kind = (Letter == '.' ? FieldPointKind.OpenSpace :
                                Letter == '#' ? FieldPointKind.Wall :
                                Letter == ' ' ? FieldPointKind.Void :
                                char.IsUpper(Letter) ? FieldPointKind.Token :
                                FieldPointKind.Void)
                    };
                }
            }
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (Items[y*maxX+x].Kind==FieldPointKind.Token)
                    {
                        //we need to find the second character. Look down, then right. If down, then it is a vertical
                        //token. If right, it is a horizontal token.
                        if (Items[(y+1)*maxX + x].Kind==FieldPointKind.Token)
                        {
                            if (y == 0)
                            {
                                Items[maxX + x].Token = Items[x].Token + Items[maxX + x].Token;
                                Items[maxX + x].OuterOrInner = OuterOrInner.Outer;
                                Items[x].Kind = FieldPointKind.Void;
                            }
                            else if (y+2 == maxY)
                            {
                                Items[y*maxX + x].Token = Items[y*maxX + x].Token + Items[(y+1)*maxX + x].Token;
                                Items[y*maxX + x].OuterOrInner = OuterOrInner.Outer;
                                Items[(y + 1) * maxX + x].Kind = FieldPointKind.Void;
                            }
                            else if (y < maxY/2)
                            {
                                Items[y * maxX + x].Token = Items[y * maxX + x].Token + Items[(y + 1) * maxX + x].Token;
                                Items[y*maxX + x].OuterOrInner = OuterOrInner.Inner;
                                Items[(y + 1) * maxX + x].Kind = FieldPointKind.Void;
                            }
                            else
                            {
                                Items[(y+1) * maxX + x].Token = Items[y * maxX + x].Token + Items[(y + 1) * maxX + x].Token;
                                Items[(y+1) * maxX + x].OuterOrInner = OuterOrInner.Inner;
                                Items[(y * maxX) + x].Kind = FieldPointKind.Void;
                            }
                        }
                        else if(Items[y * maxX + x + 1].Kind == FieldPointKind.Token)
                        {
                            if (x == 0)
                            {
                                Items[y * maxX + x + 1].Token = Items[y * maxX + x].Token + Items[y * maxX + x + 1].Token;
                                Items[y * maxX + x + 1].OuterOrInner = OuterOrInner.Outer;
                                Items[y * maxX + x].Kind = FieldPointKind.Void;
                            }
                            else if (x + 2 == maxX)
                            {
                                Items[y * maxX + x].Token = Items[y * maxX + x].Token + Items[y * maxX + x + 1].Token;
                                Items[y * maxX + x].OuterOrInner = OuterOrInner.Outer;
                                Items[y * maxX + x + 1].Kind = FieldPointKind.Void;
                            }
                            else if (x < maxX/2)
                            {
                                Items[y * maxX + x].Token = Items[y * maxX + x].Token + Items[y * maxX + x + 1].Token;
                                Items[y * maxX + x].OuterOrInner = OuterOrInner.Inner;
                                Items[y * maxX + x+1].Kind = FieldPointKind.Void;
                            }
                            else
                            {
                                Items[y * maxX + x+1].Token = Items[y * maxX + x].Token + Items[y * maxX + x + 1].Token;
                                Items[y * maxX + x + 1].OuterOrInner = OuterOrInner.Inner;
                                Items[y * maxX + x].Kind = FieldPointKind.Void;
                            }
                        }
                    }
                }
            }
        }

        public void CalculatePathLevel0(FieldPoint from)
        {
            //bail outer field points except AA and ZZ.
            if (from.Token=="AA" || from.Token=="ZZ")
            {

            }
            else if (from.Y==1 || from.X==1 || from.X+2==maxX || from.Y+2==maxY)
            {
                return;
            }

            var ItemsCopy = new FieldPoint[Items.Length];
            for (int i = 0; i < ItemsCopy.Length; i++)
            {
                ItemsCopy[i] = new FieldPoint()
                {
                    X = Items[i].X,
                    Y = Items[i].Y,
                    Kind = Items[i].Kind,
                    Token = Items[i].Token,
                    OuterOrInner = Items[i].OuterOrInner
                };
            }
            ItemsCopy[(from.Y * maxX) + from.X].Kind = FieldPointKind.Flood1;
            var Wall = new FieldPoint() { Kind = FieldPointKind.Wall, Token = "#" };
            int count = 0;
            bool found;
            do
            {
                found = false;
                count++;
                for (int py = 0; py < maxY; py++)
                {
                    for (int px = 0; px < maxX; px++)
                    {
                        var item = ItemsCopy[(py * maxX) + px];
                        if (item.Kind == FieldPointKind.OpenSpace || item.Kind == FieldPointKind.Token)
                        {
                            //at this point it is a key, door, or open space.

                            var itemAbove = py != 0 ? ItemsCopy[(py - 1) * maxX + px] : Wall;
                            var itemBelow = py + 1 != maxY ? ItemsCopy[(py + 1) * maxX + px] : Wall;
                            var itemLeft = px != 0 ? ItemsCopy[py * maxX + px - 1] : Wall;
                            var itemRight = px + 1 != maxX ? ItemsCopy[py * maxX + px + 1] : Wall;

                            if (itemAbove.Kind == FieldPointKind.Flood1)
                            {
                                found = FloodSegment0(from, count, py, px, item, itemAbove);
                                found = true;
                            }
                            else if (itemBelow.Kind == FieldPointKind.Flood1)
                            {
                                FloodSegment0(from, count, py, px, item, itemBelow);
                                found = true;
                            }
                            else if (itemLeft.Kind == FieldPointKind.Flood1)
                            {
                                FloodSegment0(from, count, py, px, item, itemLeft);
                                found = true;
                            }
                            else if (itemRight.Kind == FieldPointKind.Flood1)
                            {
                                FloodSegment0(from, count, py, px, item, itemRight);
                                found = true;
                            }
                        }
                    }
                }
                for (int i = 0; i < ItemsCopy.Length; i++)
                {
                    if (ItemsCopy[i].Kind == FieldPointKind.Flood2)
                    {
                        ItemsCopy[i].Kind = FieldPointKind.Flood1;
                    }
                }
            } while (found == true);
        }

        public void CalculatePathLevel1toN(FieldPoint from)
        {
            var ItemsCopy = new FieldPoint[Items.Length];
            for (int i = 0; i < ItemsCopy.Length; i++)
            {
                ItemsCopy[i] = new FieldPoint()
                {
                    X = Items[i].X,
                    Y = Items[i].Y,
                    Kind = Items[i].Kind,
                    Token = Items[i].Token,
                    OuterOrInner = Items[i].OuterOrInner
                };
            }
            ItemsCopy[(from.Y * maxX) + from.X].Kind = FieldPointKind.Flood1;
            var Wall = new FieldPoint() { Kind = FieldPointKind.Wall, Token = "#" };
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
                        if (item.Kind == FieldPointKind.OpenSpace || item.Kind == FieldPointKind.Token)
                        {
                            //at this point it is a key, door, or open space.

                            var itemAbove = py != 0 ? ItemsCopy[(py - 1) * maxX + px] : Wall;
                            var itemBelow = py + 1 != maxY ? ItemsCopy[(py + 1) * maxX + px] : Wall;
                            var itemLeft = px != 0 ? ItemsCopy[py * maxX + px - 1] : Wall;
                            var itemRight = px + 1 != maxX ? ItemsCopy[py * maxX + px + 1] : Wall;

                            if (itemAbove.Kind == FieldPointKind.Flood1)
                            {
                                found = FloodSegment1toN(from, count, py, px, item, itemAbove);
                                found = true;
                            }
                            else if (itemBelow.Kind == FieldPointKind.Flood1)
                            {
                                FloodSegment1toN(from, count, py, px, item, itemBelow);
                                found = true;
                            }
                            else if (itemLeft.Kind == FieldPointKind.Flood1)
                            {
                                FloodSegment1toN(from, count, py, px, item, itemLeft);
                                found = true;
                            }
                            else if (itemRight.Kind == FieldPointKind.Flood1)
                            {
                                FloodSegment1toN(from, count, py, px, item, itemRight);
                                found = true;
                            }
                        }
                    }
                }
                for (int i = 0; i < ItemsCopy.Length; i++)
                {
                    if (ItemsCopy[i].Kind == FieldPointKind.Flood2)
                    {
                        ItemsCopy[i].Kind = FieldPointKind.Flood1;
                    }
                }
            } while (found == true);
        }

        private bool FloodSegment0(FieldPoint from, int count, int py, int px, FieldPoint item, FieldPoint neighbor)
        {
            if (neighbor.Kind == FieldPointKind.Flood1)
            {
                if (item.Kind == FieldPointKind.Token && item.OuterOrInner!= OuterOrInner.Outer)
                {
                    FieldPaths0.Add(new FieldPaths()
                    {
                        From = from,
                        To = Items[(py * maxX) + px],
                        Count = count - 1,
                    });
                }
                item.Kind = FieldPointKind.Flood2;
                return true;
            }
            return false;
        }
        private bool FloodSegment1toN(FieldPoint from, int count, int py, int px, FieldPoint item, FieldPoint neighbor)
        {
            if (neighbor.Kind == FieldPointKind.Flood1)
            {
                if (item.Kind == FieldPointKind.Token)
                {
                    FieldPaths1toN.Add(new FieldPaths()
                    {
                        From = from,
                        To = Items[(py * maxX) + px],
                        Count = count - 1,
                    });
                }
                item.Kind = FieldPointKind.Flood2;
                return true;
            }
            return false;
        }
    }

    [DebuggerDisplay("{Token}({X},{Y})")]
    public class FieldPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Token { get; set; }
        public FieldPointKind Kind { get; set; }
        public OuterOrInner OuterOrInner { get; set; }
    }

    [DebuggerDisplay("{From}-{Count}-{To})")]
    public class FieldPaths
    {
        public int Segments { get; set; }
        public FieldPoint From { get; set; }
        public FieldPoint To { get; set; }
        public int Count { get; set; }
        public string Tokenize(int Level)
        {
            return From.Token + To.Token + Level;
        }
    }

    public enum FieldPointKind
    {
        Wall,
        OpenSpace,
        Flood1,
        Flood2,
        Void,
        Token
    }
    public enum OuterOrInner
    {
        Outer,
        Inner
    }
    public class Day20
    {
        public void Run()
        {
            var begin = DateTime.Now;
            var field = new Field(Data);
            foreach (FieldPoint f in from f in field.Items where f.Kind == FieldPointKind.Token select f)
            {
                field.CalculatePathLevel0(f);
                field.CalculatePathLevel1toN(f);
            }
            Console.WriteLine("Day 20,P1:" + OpenDoors(field, new FieldPoint { Token = "AA" }, new string[] { }, int.MinValue) + ", completed in " + (DateTime.Now - begin).TotalMilliseconds + " milliseconds");
            begin = DateTime.Now;
            Console.WriteLine("Day 20,P2:" + OpenDoors(field, new FieldPoint { Token = "AA" }, new string[] { }, 0) + ", completed in " + (DateTime.Now - begin).TotalMilliseconds + " milliseconds");
        }

        private int OpenDoors(Field field, FieldPoint begin, string[] UsedTokens, int Level)
        {
            if (begin.Token == "ZZ") return -1;
            if (Level != int.MinValue && (Level < 0 || Level > 25)) return 99999999;
            List<FieldPaths> fieldPaths = (Level == 0 && begin.Token == "AA") ? field.FieldPaths0 : field.FieldPaths1toN;
            return (from p in
                        from path in
                            from path in field.FieldPaths1toN
                            where path.To.Token != "AA" &&
                                  (path.To.Token != "ZZ" || (Level == 0 || Level == int.MinValue)) &&
                                  path.From.X != begin.X &&
                                  path.From.Y != begin.Y &&
                                  path.From.Token == begin.Token &&
                                  !UsedTokens.Contains(path.Tokenize(Level))
                            orderby path.Count
                            select path
                        select path.Count + OpenDoors(field, path.To, new List<string>(UsedTokens) { path.Tokenize(Level) }.ToArray(), Level == int.MinValue ? int.MinValue : Level + (path.To.OuterOrInner == OuterOrInner.Inner ? 1 : -1))
                    orderby p
                    select p).DefaultIfEmpty(99999999).FirstOrDefault();
        }

        public string TestData1 = @"         A           
         A           
  #######.#########  
  #######.........#  
  #######.#######.#  
  #######.#######.#  
  #######.#######.#  
  #####  B    ###.#  
BC...##  C    ###.#  
  ##.##       ###.#  
  ##...DE  F  ###.#  
  #####    G  ###.#  
  #########.#####.#  
DE..#######...###.#  
  #.#########.###.#  
FG..#########.....#  
  ###########.#####  
             Z       
             Z       ";
        public string TestData2 = @"                   A               
                   A               
  #################.#############  
  #.#...#...................#.#.#  
  #.#.#.###.###.###.#########.#.#  
  #.#.#.......#...#.....#.#.#...#  
  #.#########.###.#####.#.#.###.#  
  #.............#.#.....#.......#  
  ###.###########.###.#####.#.#.#  
  #.....#        A   C    #.#.#.#  
  #######        S   P    #####.#  
  #.#...#                 #......VT
  #.#.#.#                 #.#####  
  #...#.#               YN....#.#  
  #.###.#                 #####.#  
DI....#.#                 #.....#  
  #####.#                 #.###.#  
ZZ......#               QG....#..AS
  ###.###                 #######  
JO..#.#.#                 #.....#  
  #.#.#.#                 ###.#.#  
  #...#..DI             BU....#..LF
  #####.#                 #.#####  
YN......#               VT..#....QG
  #.###.#                 #.###.#  
  #.#...#                 #.....#  
  ###.###    J L     J    #.#.###  
  #.....#    O F     P    #.#...#  
  #.###.#####.#.#####.#####.###.#  
  #...#.#.#...#.....#.....#.#...#  
  #.#####.###.###.#.#.#########.#  
  #...#.#.....#...#.#.#.#.....#.#  
  #.###.#####.###.###.#.#.#######  
  #.#.........#...#.............#  
  #########.###.###.#############  
           B   J   C               
           U   P   P               ";
        public string TestData3 = @"             Z L X W       C                 
             Z P Q B       K                 
  ###########.#.#.#.#######.###############  
  #...#.......#.#.......#.#.......#.#.#...#  
  ###.#.#.#.#.#.#.#.###.#.#.#######.#.#.###  
  #.#...#.#.#...#.#.#...#...#...#.#.......#  
  #.###.#######.###.###.#.###.###.#.#######  
  #...#.......#.#...#...#.............#...#  
  #.#########.#######.#.#######.#######.###  
  #...#.#    F       R I       Z    #.#.#.#  
  #.###.#    D       E C       H    #.#.#.#  
  #.#...#                           #...#.#  
  #.###.#                           #.###.#  
  #.#....OA                       WB..#.#..ZH
  #.###.#                           #.#.#.#  
CJ......#                           #.....#  
  #######                           #######  
  #.#....CK                         #......IC
  #.###.#                           #.###.#  
  #.....#                           #...#.#  
  ###.###                           #.#.#.#  
XF....#.#                         RF..#.#.#  
  #####.#                           #######  
  #......CJ                       NM..#...#  
  ###.#.#                           #.###.#  
RE....#.#                           #......RF
  ###.###        X   X       L      #.#.#.#  
  #.....#        F   Q       P      #.#.#.#  
  ###.###########.###.#######.#########.###  
  #.....#...#.....#.......#...#.....#.#...#  
  #####.#.###.#######.#######.###.###.#.#.#  
  #.......#.......#.#.#.#.#...#...#...#.#.#  
  #####.###.#####.#.#.#.#.###.###.#.###.###  
  #.......#.....#.#...#...............#...#  
  #############.#.#.###.###################  
               A O F   N                     
               A A D   M                     ";
        public string Data =
@"                                     L         W   B         S A       N   V                                           
                                     A         X   G         D A       C   L                                           
  ###################################.#########.###.#########.#.#######.###.#########################################  
  #.....#.#.....#.#...........#.#.#...#...#...#.#.#...#.#.#.......#.......#...........#.......#.#.......#.#.#...#...#  
  #####.#.###.###.#.#########.#.#.#.###.###.#.#.#.###.#.#.#.#.###.#####.#.#.#####.#######.###.#.#.#####.#.#.#.###.###  
  #.......#.#.#...#.#.......#.........#.#.#.#.#...#.......#.#.#.....#...#.#...#.......#.....#.......#...............#  
  #.#.###.#.#.#.#########.###.#.###.###.#.#.#.###.###.###.#####.#########.#.###########.#####.#########.###.###.#####  
  #.#.#...........#.........#.#.#.........#.#.......#.#...#.......#.....#.#.#.#.....#.....#.#.#.....#.#...#.#.....#.#  
  #.###########.#.#.#####.#######.###.###.#.#####.###.#.#######.#.#####.#.#.#.#.#######.###.#####.###.###.###.#####.#  
  #.#...#.......#.#.#.#...#.#.#...#.#...#.#.....#...#.#.....#...#.....#...#.............#.#.#...........#.#.#.#.#.#.#  
  ###.#.#########.###.###.#.#.###.#.#####.#.#####.###.#.#########.#.###.###.#####.#####.#.#.#.#######.#####.###.#.#.#  
  #...#.#.#...............#.#.#...#.......#.#.#.#.#...#.#.#.#.....#.#.....#...#.......#.#.....#.#.#.#...#...#...#...#  
  #####.#.###.###.#.#.###.#.#.#####.#####.#.#.#.#######.#.#.#####.#######.#.###.#.#######.###.#.#.#.#######.###.###.#  
  #...#.#.#...#...#.#.#.#...#.....#.#.....#.....#.............#.....#.....#.#.#.#.........#.#...........#...#.#...#.#  
  #.#.#.#.#.#.#.#.#####.###.#.#.#.#######.###.#####.#.###.###.###.#.#.#.#####.#.#.#.#.#.#.#.#####.#########.#.###.#.#  
  #.#.#...#.#.#.#.#.........#.#.#.#...#.#.#...#.#...#.#.....#.#...#.#.#...#.....#.#.#.#.#.....#.#...#.#.#.....#...#.#  
  #.###.#.###########.###.#.#####.###.#.#.#.###.###.#####.#######.#####.#.###.###.#######.###.#.###.#.#.###.#####.#.#  
  #.#...#.#.#.#.....#.#...#.......#.#.....#.#.....#.#.#.#...#.#.#.#.....#...#...#.#...#.#.#...#...#.......#.#.#...#.#  
  #.#.###.#.#.###.#####.###.#####.#.#####.#.###.#####.#.#.###.#.#.###.###.###.#######.#.#########.#####.###.#.#.###.#  
  #.#.#.........#...#.#...#.#.........#...#.......#.......#.#.#.#.#.#.#...#.....#...........#.......#.#.#...#.....#.#  
  #.#.###.#.#.###.###.###########.###.###.###.#.#####.#.###.#.#.#.#.#####.#.###########.###.#.#######.#####.#.#.###.#  
  #.#.#...#.#.#.....#...#.#.......#.....#.#...#.#.#...#...#.....#...#.....#.....#.#.#...#.#.....#.#.....#...#.#.#...#  
  #.#####.#######.#####.#.#.###.#.###.#.#.#.#####.#######.#.###.#.#####.###.#####.#.###.#.###.###.#.#######.###.#.###  
  #.#...#...#.....#...#...#.#...#.#.#.#...#.#.#...#...#...#...#.....#.....#...#.#.#.........#.#.#.......#.#.#.....#.#  
  #.#.###.#######.#.#####.#.#######.#.#####.#.###.#.#####.###.#.#.#######.#.###.#.#.#.#######.#.###.#####.#.###.###.#  
  #...#.#.#...#.......#.#.#...#.#.#...#.......#.....#.....#...#.#.#...#...#.#...#...#.#...#.#.#...#.#.........#.....#  
  ###.#.#.#.#####.#####.#.#####.#.#.#########.#.#.#####.#####.#####.#####.#.###.#.#.###.###.###.###.#####.#.###.#####  
  #...#.....#.......#.......#...#...........#...#...#.#.#.#.......#.....#.#.......#.........#.#.....#.....#.#...#.#.#  
  #.###.#########.#######.###.#.#########.###.###.#.#.#.#.#.#######.###.#.#######.#.#####.###.###.#######.#####.#.#.#  
  #...#.#...#.#.#.#.......#...#.............#.#...#.#.....#.......#...#...#.......#.....#.#.#...#.#.....#...........#  
  #.###.#.###.#.#.#####.###########.###########.#####.#########.###.#######.#############.#.###.#.###.#####.#########  
  #.#.........#.#.#.#.#.....#.#    F           B     P         W   V       O            #.#.#...#.#.#.....#...#.....#  
  #.###.#######.#.#.#.#.#####.#    A           P     V         X   X       W            ###.###.#.#.#####.#.###.###.#  
  #...#...#.#.................#                                                         #...#...#.#.....#.........#.#  
  #.#.#.###.#.###.###.#######.#                                                         #.#.###.#.#.#.#####.###.#####  
  #.#.........#...#.....#...#..OU                                                       #.#.#.#...#.#.#.#.#.#.#.#...#  
  ###.###.###.###.#########.#.#                                                         #.###.###.#.###.#.#.#.#####.#  
SL..#...#.#...#...#.....#.#...#                                                         #.....#...#.....#.......#....UY
  #.#.###############.###.#####                                                         #.###.#.#.#.###.#.###.###.###  
  #.....#.....#...#.....#.....#                                                       NC..#.....#.#...#...#.#...#...#  
  ###.#######.#.###.#######.###                                                         #####.###.#.#.###.#.#.###.###  
  #.#...#.........#...#.......#                                                         #.#...#.....#.#.#.#...#.#...#  
  #.#.#########.#.#.#####.###.#                                                         #.#######.###.#.###.###.#.#.#  
  #...#...#.#.#.#.#.#.#.....#.#                                                         #...#...#.#.#.#.#.#.......#.#  
  #####.###.#.#.###.#.#####.#.#                                                         ###.###.###.###.#.###########  
  #.#.......#.#.....#.......#.#                                                         #.......#.....#...#.......#..OW
  #.###.###.#.#.#.#.#####.###.#                                                         ###.###.#.###.#.#.#.#####.#.#  
VX......#.#.....#.#.......#.#..HR                                                     BR....#...#...#...#...#.......#  
  #.#####.#####.###########.#.#                                                         #####.#####.###.###.###.###.#  
  #.#.......#.#.#...#.#.#.#.#.#                                                         #.#...#.....#.#...#.#...#.#.#  
  ###.#.#####.#####.#.#.#.#.#.#                                                         #.#.###.###.#.###########.#.#  
HR..#.#...#.#.#.#.....#.....#.#                                                         #.......#.....#.......#.#.#.#  
  #.#.#.###.#.#.###.#.#.###.###                                                         #.#.#.###########.#####.#.###  
  #...#.............#...#.#....SD                                                       #.#.#.#.....................#  
  #.#.#.#.###.#.###.###.#.#####                                                         #.###.#.#.#####.#.#.#.#####.#  
  #.#.#.#.#.#.#...#...#.#...#.#                                                         #...#.#.#...#...#.#.#...#....KZ
  #########.#.#####.#.###.#.#.#                                                         #######.#####.#.#.#.#######.#  
  #.#.#.#...#.#.#.#.#.#.#.#...#                                                         #...#.......#.#.#.#.#...#.#.#  
  #.#.#.#.#####.#.#####.#.###.#                                                         #.#.#.#######.#.#####.###.###  
FD..#.#...#.....#...#.....#....VD                                                     AD..#.....#.#...#.#.#.#...#.#.#  
  #.#.#.#.###.###.#.###.###.###                                                         #########.#######.#.#.###.#.#  
  #.....#.........#.....#.#...#                                                         #.#...........#...#.#...#.#.#  
  #########.#####.#.#####.#.###                                                         #.#.#####.###.###.#.###.#.#.#  
  #...#.......#...#.#...#.#.#.#                                                       LW..#.....#...#.#...#.......#.#  
  #.#.###.#.#.#.#####.#.#.###.#                                                         #.#####.#.#######.#.###.###.#  
  #.#...#.#.#.#.#.#.#.#...#....QI                                                       #.......#...........#........BP
  #.###.#########.#.#.#.#.#.###                                                         ###.#####.#.#####.###.#.#####  
PV..#...#.....#.......#.#...#.#                                                         #...#.#.#.#.#.......#.#.#....QI
  ###.#####.#####.#########.#.#                                                         #####.#.#######.###.#####.#.#  
  #.#...................#.....#                                                         #...#...#.#.#.#...#.#.....#.#  
  #.###########.#.#############                                                         #.###.#.#.#.#.#.#######.###.#  
BR....#...#...#.#.#...#.......#                                                         #...#.#...#.#.#...#...#...#.#  
  ###.#.#.#.#.###.###.#####.###                                                         #.#.#.###.#.#.#####.###.###.#  
  #.....#.#.#.#...#.....#.....#                                                       RH..#...#.#.................#.#  
  ###.#.#.#.#.#####.#.#####.#.#                                                         #######.###########.#.#.#####  
  #...#.#...#.......#.....#.#..FD                                                       #.....#...........#.#.#.#...#  
  ###.###.###.#########.#####.#                                                         #.#.###.###.###.#.#######.#.#  
  #...#.#...#.#...#.#.........#                                                         #.#.......#...#.#.........#..RB
  #####.#######.###.###########                                                         #.###########.#.#.#.#.#.###.#  
  #.....#...#..................GE                                                       #...#.#.......#.#.#.#.#.#.#.#  
  #####.#.#.#.#######.#.#.#.###                                                         ###.#.#####.#.#.#.#.###.#.#.#  
  #.....#.#.#.#...#.#.#.#.#...#                                                       LB....#.....#.#.#.#.#.#.....#.#  
  ###.#.###.#.#.###.#.###.#.###                                                         #.#.###.#####.###.#####.#####  
RH....#...#.......#...#...#...#                                                         #.#.........#...#.#.#.....#.#  
  ###.###.#####.#.#######.###.#                                                         #.#.#.#.#######.###.###.#.#.#  
  #.#...#.......#.....#.#.#...#                                                         #.#.#.#.#.#...#.#.#.#...#...#  
  #.#.###.###.#########.#.#####      R     V         S   H           K     U B L        ###.###.#.###.###.#.###.###.#  
  #.....#...#...#...#...#.....#      B     L         L   S           Z     Y G A        #.#...#.......#.......#.#...#  
  #.###.#.#.#.###.#.###.#############.#####.#########.###.###########.#####.#.#.#########.###.#######.#####.###.###.#  
  #...#.#.#.#.....#.....#.....#...#.....#.......#.....#...#...#.......#.....#.#...........#.....#...#...#.........#.#  
  #.###.#.#.#.###.###.###.#.#.#.###.#.#.###.#.#####.###.###.###.#####.#.###.#.###.###.###########.#############.#.###  
  #.#...#.#.#.#.#.#...#...#.#.......#.#.#...#.....#.#.........#...#.#.#.#.#.#.#...#...#.......#...#...#.........#.#.#  
  #.#.###.#.###.#.#.###.#####.#.#####.#.#.#########.#######.#####.#.#.#.#.#.#.#######.#.###.###.#.#.#############.#.#  
  #.#.#...#...#...#.#.#.#.....#.#.#.#.#.#.#...#.#.....#.....#.#.#.#.#.#.#.......#.......#.#.....#.....#.#.....#.....#  
  ###.#####.###.#.#.#.#######.###.#.###.#.#.#.#.#####.#####.#.#.###.#.#.###.#####.#.###.#.###.###.#####.#####.#.#.###  
  #.#...#...#...#.#.....#.....#.........#...#.....#...#...#.......#...#...#...#.#.#...#.#.#...#.#.......#.#.....#.#.#  
  #.#.#####.#.###.###################.#########.#####.###.#.#########.###.#####.#####.###.#####.#.#.#####.#######.#.#  
  #...#.....#.#.#...#.#.#...#.......#.....#.......#.#.#.........#.....#.#.........#.#...#.....#...#.#.........#.....#  
  #.#####.#.#.#.#####.#.#.#####.#####.#.#######.###.#.#.###.###.#####.#.#.#.###.###.#.###.###.#######.#.#####.#####.#  
  #.#.#.#.#.#...#.#...........#.#...#.#.#.#.....#.#...#.#.#...#.#.....#...#.#.....#.........#.#.......#.....#.#.....#  
  #.#.#.###.#.#.#.###.#.#.#.###.###.###.#.#####.#.###.#.#.#.#####.###.#.#.#.#########.#.###########.###.###.#######.#  
  #.......#.#.#.#.#...#.#.#.......#.#.....#.........#.#...#.....#.#...#.#.#.#.....#.#.#.........#...#.....#.#.......#  
  #.###.###.#####.#####.###.#####.#.#.###.###.#######.#.#.###########.#.#########.#.#.#.#.###.###.#####.#########.#.#  
  #.#...#.#...#.....#.#.#.....#.#.....#...#.....#.#.#.#.#...#.....#.#.#...#...#.#.....#.#.#.......#...#.......#...#.#  
  #.#####.#########.#.###.#####.#.#.#.#########.#.#.#.#.#######.###.#.#.###.###.#.#.###########.#.###.###########.#.#  
  #...#...........#.#.#.#.#.#.....#.#.......#...#...#.#.....#...#...#.#.#...#.#.#.#.#.#.....#...#.......#.#.#...#.#.#  
  #######.#.#.###.#.#.#.###.###.#######.#######.#.#.#.#.#######.#.###.#.#.###.#.#.###.###.#######.#######.#.#.#######  
  #.......#.#.#.....#.#.#...#...#.#.......#...#.#.#.#.#.....#.........#...#.#.....#...........#.#...#.#.............#  
  ###.###.#.###.#.#.#.#.#.#######.#.#######.#.#.#.#.#.#.#.#####.#####.#.###.###.#####.#########.#####.#.#.###.#######  
  #.#.#...#.#.#.#.#.........#.#.#.#.....#...#.#.#.#...#.#.#.#.#.....#.#...#...#.......#.#.#.#.....#.#...#...#...#.#.#  
  #.#.#######.#######.###.###.#.#.#####.###.#.#.#.#####.###.#.###.#####.#.#.#.#######.#.#.#.###.###.#.#######.###.#.#  
  #...#.....#.........#...............#...#.#...#.....#.#.#...#.......#.#.#.#.#.......#...#.......#.........#.#.....#  
  #.#######.#######.#.###.###.#.#.#.###.###.#.###.###.#.###.#.#####.#.###.#.#.#.#######.#####.#####.#####.#######.###  
  #.#.#...#...#.....#.#...#...#.#.#.....#...#...#.#...#...#.#.#.....#.#.....#.#.#.....................#...#.....#...#  
  ###.#.###.#######.###.#####.###.###.###.#######.###.###.#.#.#.#######.#####.#.###.###.###.#.#.#.###.#.#.#.#####.###  
  #.................#.....#...#...#...#.........#.#...#.....#.#.......#.#.....#.....#.....#.#.#.#...#.#.#...........#  
  ###############################.#########.#######.#####.###.#####.#####.###.###.###################################  
                                 A         F       G     O   Z     H     V   L   L                                     
                                 D         A       E     U   Z     S     D   W   B                                     ";
    }

}