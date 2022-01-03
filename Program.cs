using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Temporary_App
{
    class Program
    {
        const int option1 = 0x01; // шестнадцатеричный литерал для 0000 0001
        const int option2 = 0x02; // шестнадцатеричный литерал для 0000 0010
        const int option3 = 0x04; // шестнадцатеричный литерал для 0000 0100
        const int option4 = 0x08; // шестнадцатеричный литерал для 0000 1000
        const int option5 = 0x10; // шестнадцатеричный литерал для 0001 0000
        const int option6 = 0x20; // шестнадцатеричный литерал для 0010 0000
        const int option7 = 0x40; // шестнадцатеричный литерал для 0100 0000
        const int option8 = 0x80; // шестнадцатеричный литерал для 1000 0000

        // Использую эту маску для "вытаскивания" 9 битов.
        const int mask_9 = 0b_1111_1111_1;

        enum BytesAmount
        {
            AmountIs9,
            AmountIs10,
            AmountIs11,
            AmountIs12,
            AmountIs13,
            AmountIs14
        }

        public static void WriteBites(int NumberBites, int EnumValue)
        {
            var FileStream = File.Create("D://bites_writing.bitesfile");




        }

        static void Main(string[] args)
        {
            var Request = BytesAmount.AmountIs9;

            var Bytes = new byte[4] { 221, 148, 122, 42 };

            Console.WriteLine("Bytes in Int32 is: " + Convert.ToString(BitConverter.ToInt32(Bytes), 2));

            switch (Request)
            {
                case BytesAmount.AmountIs9:
                    {
                        ReadBytes(9, Bytes);


                        break;
                    }

                case BytesAmount.AmountIs10:
                    break;
                case BytesAmount.AmountIs11:
                    break;
                case BytesAmount.AmountIs12:
                    break;
                case BytesAmount.AmountIs13:
                    break;
                case BytesAmount.AmountIs14:
                    break;
                default:
                    break;
            }


        }

        private static void ReadBytes(int NeedBites, byte[] FileBytes)
        {
            byte LastByte = 0;
            byte FirstByte = 0;
            byte Secondbyte = 0;

            byte Residue = 0;

            for (int i = 2; i <= FileBytes.Length; i += 2)
            {
                if (Residue == 0)
                {
                    FirstByte = FileBytes[i - 2];
                    Secondbyte = FileBytes[i - 1];
                }
                else
                {
                    Secondbyte = FileBytes[i - 2];
                }

                int Value = GetValue(NeedBites, FirstByte, Secondbyte, ref Residue);

                if (Residue != 0)
                {
                    FirstByte = Residue;
                }

            }
        }

        private static int GetValue(int needBites, byte firstByte, byte secondbyte, ref byte residue)
        {
            // Полученные два байта склеиваю вместе под Uint16
            UInt16 intermediaValue = BitConverter.ToUInt16(new byte[2] { firstByte, secondbyte });

            // "Вытаскиваю" 9 битов из вышеуказанного 
            var result = (intermediaValue & mask_9);

            Console.WriteLine("Uint16 value is: " + Convert.ToString(intermediaValue, 2));
            Console.WriteLine("Mask_9 is: " + Convert.ToString(mask_9, 2));
            Console.WriteLine("Result is: " + Convert.ToString(result, 2));

            // "Остаточные" биты присваиваю переменной
            residue = (byte)(intermediaValue >> 9);

            Console.WriteLine("Residue is: " + Convert.ToString(residue, 2));

            
            // Возращаю 9 битов
            return result;
        }
    }
}
