using System;
using System.IO;


namespace CoroutineFacilities.StructuredText.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Removes the specified number of line terminators.
        /// </summary>
        /// <param name="count">Number of line terminators to remove</param>
        public static void RemoveLineTerminator(this StringWriter obj, int count = 1)
        {
            var builder = obj.GetStringBuilder();
            string lineTerminator = obj.NewLine;

            for (int i = 0; i < count; i++)
            {
                if (builder.ToString(builder.Length - (i + 1) * lineTerminator.Length, lineTerminator.Length) != lineTerminator)
                {
                    throw new InvalidOperationException();
                }
            }

            int removeCount = count * lineTerminator.Length;
            builder.Remove(builder.Length - removeCount, removeCount);
        }


        /// <summary>
        /// Writes the character the specified number of times.
        /// </summary>
        /// <param name="value">The character to repeat</param>
        /// <param name="repeatCount">The number of times to repeat</param>
        public static void Write(this TextWriter obj, char value, int repeatCount)
        {
            for (int i = 0; i < repeatCount; i++) obj.Write(value);
        }

        /// <summary>
        /// Automatically appends the character after writing the string if the string is not empty or null.
        /// </summary>
        /// <param name="value">The character to append</param>
        /// <param name="count">The number of times to append</param>
        public static void Write_AutoAppend(this TextWriter obj, string value, char character = ' ', int count = 1)
        {
            if (!string.IsNullOrEmpty(value))
            {
                obj.Write(value);

                for (int i = 0; i < count; i++)
                {
                    obj.Write(character);
                }
            }
        }


        /// <summary>
        /// Writes the string to the writer and does automatic indentation based on line breaks and tabs.
        /// </summary>
        /// <param name="value">The string to write</param>
        public static void Write_AutoIndent(this IndentableWriter obj, string value)
        {
            int initialIndentLevel = obj.IndentLevel;

            for (int i = 0, charIndex = 0; i < value.Length; i++)
            {
                char c = value[i];
                obj.Write(c);

                if (c == '\t')
                {
                    obj.IndentLevel++;
                }

                if (c == Environment.NewLine[charIndex])
                {
                    if (charIndex++ == Environment.NewLine.Length - 1)
                    {
                        obj.Remove(charIndex + 1); // Removes the written newLine characters
                        obj.WriteLine(); // Adds newLine and tabs characters
                        obj.IndentLevel = initialIndentLevel; // Reset the indent level since each line's indentation can be different
                        charIndex = 0;
                    }
                }
                else
                {
                    charIndex = 0; // If the condition failed, reset.
                }
            }
        }
    }
}
