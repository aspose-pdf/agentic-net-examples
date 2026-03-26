using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath1 = "first.pdf";
        const string inputPath2 = "second.pdf";
        const string outputPath = "merged.pdf";

        if (!File.Exists(inputPath1))
        {
            Console.Error.WriteLine($"File not found: {inputPath1}");
            return;
        }
        if (!File.Exists(inputPath2))
        {
            Console.Error.WriteLine($"File not found: {inputPath2}");
            return;
        }

        try
        {
            using (Document source1 = new Document(inputPath1))
            using (Document source2 = new Document(inputPath2))
            using (Document target = new Document())
            {
                // Merge the two source documents; bookmarks are preserved automatically
                target.Merge(source1, source2);
                target.Save(outputPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
