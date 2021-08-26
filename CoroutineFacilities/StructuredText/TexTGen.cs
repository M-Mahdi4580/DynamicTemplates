using System.Collections;
using System.IO;


namespace CoroutineFacilities.StructuredText
{
    /// <summary>
    /// Provides facilities for procedural text generation.
    /// </summary>
    public static class TexTGen
    {
        /// <summary>
        /// Writes the opening token, yields and then writes the closing token.
        /// </summary>
        /// <param name="builder">The object to use for writing</param>
        /// <param name="openingToken">The token written before the yield</param>
        /// <param name="closingToken">The token written after the yield</param>
        /// <param name="appendLineTerminator">Whether to append a line terminator after writing each token</param>
        /// <param name="ender">A coroutine to yield in the end</param>
        public static IEnumerator Enclose(TextWriter builder, string openingToken, string closingToken, IEnumerator ender = null)
        {
            builder.Write(openingToken);
            yield return null;
            builder.Write(closingToken);
            yield return ender;
        }

        /// <summary>
        /// Indents by the specified number of tabs and then yields to allow for inner text writing and indents back in the end.
        /// </summary>
        /// <param name="indentor">The object to use for indenting</param>
        /// <param name="count">The number of times to indent</param>
        public static IEnumerator Indent(IIndentable indentor, int count = 1)
        {
            indentor.IndentLevel += count;
            indentor.Indent(count);
            yield return null;
        }

        /// <summary>
        /// Writes the string to the TextWriter.
        /// </summary>
        /// <param name="builder">The object to use for writing</param>
        /// <param name="value">The string to write</param>
        public static IEnumerator Write(TextWriter builder, string value = " ")
        {
            builder.Write(value);
            yield return null;
        }

        /// <summary>
        /// Writes the string followed by a line terminator to TextWriter.
        /// </summary>
        /// <param name="builder">The object to use for writing</param>
        /// <param name="value">The string to write</param>
        public static IEnumerator WriteLine(TextWriter builder, string value = "")
        {
            builder.WriteLine(value);
            yield return null;
        }

        /// <summary>
        /// Removes the specified number of characters from the IndentableWriter.
        /// </summary>
        /// <param name="builder">The object to remove characters from</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerator Remove(IndentableWriter builder, int count)
        {
            builder.Remove(count);
            yield return null;
        }
    }
}
