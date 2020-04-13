using System;
using System.Linq;
using System.Text;

namespace RWD.Toolbox.PasswordGenerator
{
   /// <summary>
   /// A Tool Used to Generate Random Passwords
   /// </summary>
   public interface IPasswordGenerator
   {
      /// <summary>
      /// Generate a Random Password with no repeating characters
      /// </summary>
      /// <param name="pwdLength">Length of Password</param>
      /// <returns>Password as <see cref="string"/></returns>
      string Generate(int pwdLength);

      /// <summary>
      /// Generate a Random Password
      /// </summary>
      /// <param name="pwdLength">Length of Password</param>
      /// <param name="allowRepeatCharacters">Allow any character to be used more than once anywhere in password as <see cref="bool"/></param>
      /// <param name="allowConsecutiveCharacters">Allow a character to be used more than once consecutively (side by side) as <see cref="bool"/></param>
      /// <returns>Password as <see cref="string"/></returns>
      string Generate(int pwdLength, bool allowConsecutiveCharacters, bool allowRepeatCharacters);
   }

   /// <inheritdoc cref="IPasswordGenerator"/>
   public class PasswordGenerator : IPasswordGenerator
   {
      private readonly char[] AlphaChars;
      private readonly char[] NumericChars;
      private readonly char[] SpecialChars;
      private readonly char[] Exclusions;

      private readonly Random _random;
      private const string DefaultAlphaSet = "abcdefghijklmnopqrstuvwxyz";
      private const string DefaultNumberSet = "1234567890";
      private const string DefaultSpecialSet = "!@#$%^&*()_+=";

      /// <summary>
      /// Default Constructor
      /// <para>Default AlphaSet: abcdefghijklmnopqrstuvwxyz </para>
      /// <para>Default NumberSet: 1234567890 </para>
      /// <para>Default SpecialSet: !@#$%^&amp;*()_+= </para>
      /// </summary>
      /// <remarks>This will generate passwords using the default character sets with no excluded characters.</remarks>
      public PasswordGenerator()
      {
         _random = new Random();
         AlphaChars = DefaultAlphaSet.ToCharArray();
         NumericChars = DefaultNumberSet.ToCharArray();
         SpecialChars = DefaultSpecialSet.ToCharArray();
      }

      /// <summary>
      /// Defaults with Exclusions Constructor
      /// <para>Default AlphaSet: abcdefghijklmnopqrstuvwxyz </para>
      /// <para>Default NumberSet: 1234567890 </para>
      /// <para>Default SpecialSet: !@#$%^&amp;*()_+= </para>
      /// </summary>
      /// <param name="excludeChars">Array of Characters to NOT use in creating the password as <see cref="T:char[]"/></param>
      /// <remarks>This will generate passwords using the default character sets EXCEPT excluded characters.</remarks>
      public PasswordGenerator(char[] excludeChars)
      {
         _random = new Random();
         AlphaChars = DefaultAlphaSet.ToCharArray();
         NumericChars = DefaultNumberSet.ToCharArray();
         SpecialChars = DefaultSpecialSet.ToCharArray();
         Exclusions = excludeChars;
      }


      /// <summary>
      /// Full Details Constructor
      /// </summary>
      /// <param name="alphaChars">
      /// Array of ALPHABETIC Characters to use in creating the password as <see cref="T:char[]"/>
      /// <para>If Null Defaults to: abcdefghijklmnopqrstuvwxyz </para>
      /// </param>
      /// <param name="numericChars">
      /// Array of NUMERIC Characters to use in creating the password as <see cref="T:char[]"/>
      /// <para>If Null Defaults to: 1234567890 </para>
      /// </param>
      /// <param name="specialChars">
      /// Array of SPECIAL Characters to use in creating the password as <see cref="T:char[]"/>
      /// <para>If Null Defaults to: !@#$%^&amp;*()_+= </para>
      /// </param>
      /// <param name="excludeChars">Array of Characters to NOT use in creating the password as <see cref="T:char[]"/></param>
      /// <remarks>Any parameter equal to null will force use of default values</remarks>
      public PasswordGenerator(char[] alphaChars, char[] numericChars, char[] specialChars, char[] excludeChars)
      {
         _random = new Random();
         AlphaChars = alphaChars ?? DefaultAlphaSet.ToArray();
         NumericChars = numericChars ?? DefaultNumberSet.ToArray();
         SpecialChars = specialChars ?? DefaultSpecialSet.ToArray();
         Exclusions = excludeChars;
      }

      /// <inheritdoc cref="IPasswordGenerator.Generate(int)"/>
      public string Generate(int pwdLength)
      {
         return Generate(pwdLength, false, false);
      }

      /// <inheritdoc cref="IPasswordGenerator.Generate(int, bool, bool)"/>
      public string Generate(int pwdLength, bool allowRepeatCharacters, bool allowConsecutiveCharacters)
      {
         var pwdBuffer = new StringBuilder();
         var lastCharacter = '\n';

         var values = Enumerable.Range(0, pwdLength).OrderBy(x => _random.Next()).ToArray();
         var upperIndex = values[0];

         var numIndex = -99;
         if (NumericChars.Length > 0)
            numIndex = values[1];

         var specIndex = -98;
         if (NumericChars.Length > 0)
            specIndex = values[2];

         for (var i = 0; i < pwdLength; i++)
         {
            char[] arrayToPullFrom;
            if (i == numIndex)
               arrayToPullFrom = NumericChars;
            else if (i == specIndex)
               arrayToPullFrom = SpecialChars;
            else
               arrayToPullFrom = AlphaChars;

            var nextCharacter = GetRandomCharacter(arrayToPullFrom);

            if (!allowConsecutiveCharacters)
            {
               while (char.ToUpper(lastCharacter) == char.ToUpper(nextCharacter))
               {
                  nextCharacter = GetRandomCharacter(arrayToPullFrom);
               }
            }

            if (!allowRepeatCharacters)
            {
               var temp = pwdBuffer.ToString();
               var duplicateIndex = temp.IndexOf(nextCharacter.ToString(), StringComparison.CurrentCultureIgnoreCase);
               while (-1 != duplicateIndex)
               {
                  nextCharacter = GetRandomCharacter(arrayToPullFrom);
                  duplicateIndex = temp.IndexOf(nextCharacter);
               }
            }

            if (null != Exclusions)
            {
               while (Exclusions.Any(c => char.ToUpper(c) == char.ToUpper(nextCharacter)))
               {
                  nextCharacter = GetRandomCharacter(arrayToPullFrom);
               }
            }

            if (i == upperIndex)
               nextCharacter = char.ToUpper(nextCharacter);

            pwdBuffer.Append(nextCharacter);
            lastCharacter = nextCharacter;
         }

         return pwdBuffer.ToString();
      }

      /// <summary>
      /// Get a Random Character from Array or Characters
      /// </summary> 
      private char GetRandomCharacter(char[] charArray)
      {
         var randomCharPosition = _random.Next(charArray.Length);
         var randomChar = charArray[randomCharPosition];
         return randomChar;
      }

   }

}
