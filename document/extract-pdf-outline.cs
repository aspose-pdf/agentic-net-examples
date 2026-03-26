using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            OutlineCollection outlines = doc.Outlines;
            if (outlines == null || outlines.Count == 0)
            {
                Console.WriteLine("No outline (bookmarks) found in the document.");
                return;
            }

            Console.WriteLine("Document Outline:");
            foreach (OutlineItemCollection topItem in outlines)
            {
                PrintOutlineItem(topItem, 0);
            }
        }
    }

    private static void PrintOutlineItem(OutlineItemCollection item, int level)
    {
        string indent = new string(' ', level * 2);
        Console.WriteLine($"{indent}- {item.Title}");
        foreach (OutlineItemCollection child in item)
        {
            PrintOutlineItem(child, level + 1);
        }
    }
}