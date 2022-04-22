using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberConversion;
/// <summary>
/// Contains methods used to convert numbers from one base to another
/// </summary>
public static class NumberConverter
{
    /// <summary>
    /// Largest base Handled by NumberConverter.
    /// </summary>
    public const ushort MaxBase = 36;
    /// <summary>
    /// Smallest base Handled by NumberConverter.
    /// </summary>
    public const ushort MinBase = 2;

    /// <summary>
    ///<para>
    /// Takes number represented in first base and returns that number represented in second base.
    /// </para>
    /// <para>
    /// Validates input.
    /// </para>
    /// </summary>
    /// <exception cref="FormatException">Thrown when number contains invalid characters</exception>
    /// <exception cref="OverflowException">Throw when given number value exceeds 18446744073709551615</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when one of bases is out of handled range</exception>
    public static string Convert(string number, ushort numberBase, ushort newNumberBase)
    {
        try
        {
            string output = NumberConverter.ConvertToDecimal(number, numberBase);
            output = NumberConverter.ConvertFromDecimal(output, newNumberBase);

            return output;
        }
        catch (FormatException e)
        {
            throw e;
        }
        catch (OverflowException e)
        {
            throw e;
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw e;
        }
    }

    /// <summary>
    /// <para>
    /// Takes number represented in given base and returns that number represented in base 10.
    /// </para>
    /// <para>
    /// Validates input.
    /// </para>
    /// </summary>
    /// <exception cref="FormatException">Thrown when number contains invalid characters</exception>
    /// <exception cref="OverflowException">Thrown when given number value exceeds 18446744073709551615</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when base is out of handled range</exception>
    public static string ConvertToDecimal(string number, ushort numberBase)
    {
        if (numberBase < MinBase || numberBase > MaxBase)
        {
            throw new ArgumentOutOfRangeException();
        }

        if (!ValidateNumber(number, numberBase))
        {
            throw new FormatException();
        }

        ulong result = 0;
        for (int i = 0; i < number.Length; i++)
        {
            try
            {
                ulong n = DigitToDecimal(number[i]);
                n *= checked((ulong)Math.Pow(numberBase, number.Length - i - 1));
                result = checked(result + n);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new FormatException();
            }
            catch (OverflowException e)
            {
                throw e;
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// <para>
    /// Takes number represented in base 10 and returns that number represented in given base
    /// </para>
    /// <para>
    /// Validates input.
    /// </para>
    /// </summary>
    /// <exception cref="FormatException">Thrown when number contains invalid characters</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when base is out of handled range</exception>
    public static string ConvertFromDecimal(string number, ushort numberBase)
    {
        if (numberBase < MinBase || numberBase > MaxBase)
        {
            throw new ArgumentOutOfRangeException();
        }

        if (!ulong.TryParse(number, out ulong numberValue))
        {
            throw new FormatException();
        }

        LinkedList<char> list = new LinkedList<char>();

        while (numberValue != 0)
        {
            byte remainder = (byte)(numberValue % numberBase);
            list.AddFirst(DigitFromDecimal(remainder));
            numberValue /= numberBase;
        }

        return new string(list.ToArray());
    }

    /// <summary>
    /// Checks if number contains invalid characters;
    /// </summary>
    /// <returns>
    /// False when number is Invalid. True if is Valid.
    /// </returns>
    public static bool ValidateNumber(string number, ushort numberBase)
    {
        char[] chars = number.ToCharArray();

        foreach (char c in chars)
        {
            try
            {
                byte digit = DigitToDecimal(c);
                if (digit > numberBase - 1)
                {
                    return false;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///<para>
    /// Takes number represented in first base and returns that number represented in second base.
    /// </para>
    /// <para>
    /// Makes method slightly faster by skipping all validation.
    /// </para>
    /// </summary>
    public static string ConvertUnsafe(string number, ushort numberBase, ushort newNumberBase)
    {
        string output = NumberConverter.ConvertToDecimalUnsafe(number, numberBase);
        output = NumberConverter.ConvertFromDecimal(output, newNumberBase);

        return output;
    }

    /// <summary>
    /// <para>
    /// Takes number represented in given base and returns that number represented in base 10.
    /// </para>
    /// <para>
    /// Makes method slightly faster by skipping all validation.
    /// </para>
    /// </summary>
    public static string ConvertToDecimalUnsafe(string number, ushort numberBase)
    {
        ulong result = 0;
        for (int i = 0; i < number.Length; i++)
        {
            ulong n = DigitToDecimal(number[i]);
            n *= (ulong)Math.Pow(numberBase, number.Length - i - 1);
            result = result + n;
        }
        return result.ToString();
    }

    /// <summary>
    /// <para>
    /// Takes number represented in base 10 and returns that number represented in given base
    /// </para>
    /// <para>
    /// Makes method slightly faster by skipping all validation.
    /// </para>
    /// </summary>
    public static string ConvertFromDecimalUnsafe(string number, ushort numberBase)
    {

        ulong numberValue = ulong.Parse(number);

        LinkedList<char> list = new LinkedList<char>();

        while (numberValue != 0)
        {
            byte remainder = (byte)(numberValue % numberBase);
            list.AddFirst(DigitFromDecimal(remainder));
            numberValue /= numberBase;
        }

        return new string(list.ToArray());
    }

    private static byte DigitToDecimal(char digit)
    {
        switch (digit)
        {
            case '0': return 0;
            case '1': return 1;
            case '2': return 2;
            case '3': return 3;
            case '4': return 4;
            case '5': return 5;
            case '6': return 6;
            case '7': return 7;
            case '8': return 8;
            case '9': return 9;
            case 'A': return 10;
            case 'B': return 11;
            case 'C': return 12;
            case 'D': return 13;
            case 'E': return 14;
            case 'F': return 15;
            case 'G': return 16;
            case 'H': return 17;
            case 'I': return 18;
            case 'J': return 19;
            case 'K': return 20;
            case 'L': return 21;
            case 'M': return 22;
            case 'N': return 23;
            case 'O': return 24;
            case 'P': return 24;
            case 'Q': return 26;
            case 'R': return 27;
            case 'S': return 28;
            case 'T': return 29;
            case 'U': return 30;
            case 'V': return 31;
            case 'W': return 32;
            case 'X': return 33;
            case 'Y': return 34;
            case 'Z': return 35;

        }
        throw new ArgumentOutOfRangeException();
    }

    private static char DigitFromDecimal(byte digit)
    {
        switch (digit)
        {
            case 0: return '0';
            case 1: return '1';
            case 2: return '2';
            case 3: return '3';
            case 4: return '4';
            case 5: return '5';
            case 6: return '6';
            case 7: return '7';
            case 8: return '8';
            case 9: return '9';
            case 10: return 'A';
            case 11: return 'B';
            case 12: return 'C';
            case 13: return 'D';
            case 14: return 'E';
            case 15: return 'F';
            case 16: return 'G';
            case 17: return 'H';
            case 18: return 'I';
            case 19: return 'J';
            case 20: return 'K';
            case 21: return 'L';
            case 22: return 'M';
            case 23: return 'N';
            case 24: return 'O';
            case 25: return 'P';
            case 26: return 'Q';
            case 27: return 'R';
            case 28: return 'S';
            case 29: return 'T';
            case 30: return 'U';
            case 31: return 'V';
            case 32: return 'W';
            case 33: return 'X';
            case 34: return 'Y';
            case 35: return 'Z';
        }
        throw new ArgumentOutOfRangeException();
    }
}
