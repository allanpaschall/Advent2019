using System;
namespace AoC2019
{
    public class CharacterOCR
    {
        public static int CharacterHeight { get; set; }
        public static int CharacterWidth { get; set; }
        public CharacterOCR()
        {
        }
        public static string ReturnCharacters(int[] Input, int charLength, int qualifyingNumber)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int offset = 0; offset < charLength; offset++)
            {
                string Count3 = ((Input[(0 * CharacterWidth * charLength) + 0 + (offset * CharacterWidth)] == qualifyingNumber) ? "1" : "0") +
                                ((Input[(2 * CharacterWidth * charLength) + 3 + (offset * CharacterWidth)] == qualifyingNumber) ? "1" : "0") +
                                ((Input[(5 * CharacterWidth * charLength) + 3 + (offset * CharacterWidth)] == qualifyingNumber) ? "1" : "0");
                string Count4 = (Input[(3 * CharacterWidth * charLength) + 1 + (offset * CharacterWidth)] == qualifyingNumber) ? "1" : "0";
                int count = 0;
                for (int i = 0; i < CharacterHeight; i++)
                {
                    for (int j = 0; j < CharacterWidth; j++)
                    {
                        count += (Input[(i * CharacterWidth * charLength) + j + (offset * CharacterWidth)] == qualifyingNumber) ? 1 : 0;
                    }
                }
                switch (Count3)
                {
                    case "000":
                        sb.Append('C');
                        break;
                    case "001":
                        sb.Append('G');
                        break;
                    case "010":
                        sb.Append('J');
                        break;
                    case "011":
                        sb.Append('A');
                        break;
                    case "100":
                        if (count == 15) sb.Append('B');
                        if (count == 11) sb.Append('F');
                        break;
                    case "101":
                        if (count == 9) sb.Append('L');
                        else if (count == 14) sb.Append('E');
                        else if (Count4 == "0") sb.Append('K');
                        else if (Count4 == "1") sb.Append('Z');
                        break;
                    case "110":
                        if (count == 9) sb.Append('Y');
                        else if (Count4 == "1") sb.Append('P');
                        else if (Count4 == "0") sb.Append('U');
                        break;
                    case "111":
                        if (Count4 == "0") sb.Append('H');
                        if (Count4 == "1") sb.Append('R');
                        break;
                }
            }
            return sb.ToString();
        }

    }
}
/*[0,0] 12Y, 5N
 * [3,2] 8Y, 9N
 * [3,5] 8Y, 9N
 * [1,3]
 * 011, A(14)
 * 100, B(15),F(11)
 * 000, C(10)
 * 110, D(14),P(12,1101),U(12,1100),Y(9)
 * 101, E(14),K(12,1010),L(9),Z(12,1011)
 * 001, G(13)
 * 111, H(14,1110),R(14,1111)
 * 010, J(9),O(12)
 *
 *
 * A,0,1,1
 * .##..
 * #..#.
 * #..#.
 * ####.
 * #..#.
 * #..#.
*/
/* B,1,0,0
 * ###..
 * #..#.
 * ###..
 * #..#.
 * #..#.
 * ###..
*/
/* C,0,0,0
 * .##..
 * #..#.
 * #....
 * #....
 * #..#.
 * .##..
*/
/* D,1,1,0
 * ###..
 * #..#.
 * #..#.
 * #..#.
 * #..#.
 * ###..
*/
/* E,1,0,1
 * ####.
 * #....
 * ###..
 * #....
 * #....
 * ####.
*/
/* F,1,0,0
 * ####.
 * #....
 * ###..
 * #....
 * #....
 * #....
*/
/* G,0,0,1
 * .##..
 * #..#.
 * #....
 * #.##.
 * #..#.
 * .###.
*/
/* H,1,1,1,0
 * #..#.
 * #..#.
 * ####.
 * #..#.
 * #..#.
 * #..#.
*/
/* I
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* J,0,1,0
 * ..##.
 * ...#.
 * ...#.
 * ...#.
 * #..#.
 * .##..
*/
/* K,1,0,1,0
 * #..#.
 * #.#..
 * ##...
 * #.#..
 * #.#..
 * #..#.
*/
/* L,1,0,1
 * #....
 * #....
 * #....
 * #....
 * #....
 * ####.
*/
/* M
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* N
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* O,0,1,0
 * .##..
 * #..#.
 * #..#.
 * #..#.
 * #..#.
 * .##..
*/
/* P,1,1,0,1
 * ###..
 * #..#.
 * #..#.
 * ###..
 * #....
 * #....
*/
/* Q
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* R,1,1,1,1
 * ###..
 * #..#.
 * #..#.
 * ###..
 * #.#..
 * #..#.
*/
/* S
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* T
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* U,1,1,0,0
 * #..#.
 * #..#.
 * #..#.
 * #..#.
 * #..#.
 * .##..
*/
/* V
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* W
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* X
 * .....
 * .....
 * .....
 * .....
 * .....
 * .....
*/
/* Y,1,1,0
 * #...#
 * #...#
 * .#.#.
 * ..#..
 * ..#..
 * ..#..
*/
/* Z,1,0,1,1
 * ####.
 * ...#.
 * ..#..
 * .#...
 * #....
 * ####.
*/
