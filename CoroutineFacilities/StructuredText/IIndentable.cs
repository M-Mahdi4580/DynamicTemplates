namespace CoroutineFacilities.StructuredText
{
    public interface IIndentable
    {
        /// <summary>
        /// Number of tabs to indent by.
        /// </summary>
        int IndentLevel { get; set; }

        /// <summary>
        /// Indents by the specified number of tabs.
        /// </summary>
        /// <param name="count">Number of tabs</param>
        void Indent(int count);
    }
}
