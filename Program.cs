using CoroutineFacilities.StructuredText;
using static CoroutineFacilities.CoroutineManager;


/// <summary>
/// An example program using coroutine structured text facilities to creat a simple hello world html page.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        IndentableWriter writer = new IndentableWriter();

        Start(
            TexTGen.WriteLine(writer, "<!DOCTYPE html>"),
            TexTGen.Enclose(writer, "<html>", "</html>"),
            TexTGen.WriteLine(writer),
            TexTGen.Indent(writer),
            Yield(
                TexTGen.Enclose(writer, "<head>", "</head>", TexTGen.WriteLine(writer)),
                TexTGen.WriteLine(writer),
                TexTGen.Indent(writer),
                TexTGen.Enclose(writer, "<title>", "</title>", Yield(TexTGen.WriteLine(writer), TexTGen.Indent(writer, -1))),
                TexTGen.Write(writer, "Start Page")
                ),
            TexTGen.Enclose(writer, "<body>", "</body>", Yield(TexTGen.WriteLine(writer), TexTGen.Indent(writer, -1))),
            TexTGen.WriteLine(writer),
            TexTGen.Indent(writer),
            TexTGen.Write(writer, "Hello World!"),
            TexTGen.WriteLine(writer),
            TexTGen.Indent(writer, -1)
            );
        
        Console.WriteLine(writer.ToString());
    }
}
