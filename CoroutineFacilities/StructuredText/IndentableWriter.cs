using System;
using System.IO;
using System.Text;


namespace CoroutineFacilities.StructuredText
{
    /// <summary>
    /// An StringWriter specialized for indented writing.
    /// </summary>
    public class IndentableWriter : StringWriter, IIndentable
    {
        public int IndentLevel
        {
            get
            {
                int count = 0;

                for (int i = 0; i < NewLine.Length; i++)
                {
                    if (NewLine[i] == '\t') count++;
                    else count = 0;
                }

                return count;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Indent level cannot be negative!");
                }

                int newLineLength = Environment.NewLine.Length;
                char[] newLineTerminator = new char[newLineLength + value];

                for (int i = 0; i < newLineLength; i++)
                {
                    newLineTerminator[i] = Environment.NewLine[i];
                }
                for (int i = newLineLength; i < newLineTerminator.Length; i++)
                {
                    newLineTerminator[i] = '\t';
                }

                NewLine = new string(newLineTerminator);
            }
        }

        public IndentableWriter() { }
        public IndentableWriter(StringBuilder sb) : base(sb) { }
        public IndentableWriter(StringBuilder sb, IFormatProvider formatProvider) : base(sb, formatProvider) { }



        /// <summary>
        /// Erases the last characters.
        /// </summary>
        /// <param name="count">Number of characters to remove</param>
        public void Remove(int count)
        {
            var builder = GetStringBuilder();
            builder.Remove(builder.Length - count, count);
        }

        public void Indent(int count)
        {
            if (count >= 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Write('\t');
                }
            }
            else
            {
                var builder = GetStringBuilder();
                count *= -1; // Invert to make it positive.

                for (int i = 0; i < count; i++)
                {
                    if (builder[builder.Length - i - 1] != '\t')
                    {
                        throw new InvalidOperationException();
                    }
                }

                builder.Remove(builder.Length - count, count);
            }
        }
    }
}
