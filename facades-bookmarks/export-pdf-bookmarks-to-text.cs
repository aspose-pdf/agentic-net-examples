using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "bookmarks.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // The root outline collection of the document
        OutlineCollection rootOutlines = pdfDocument.Outlines;

        // Write the outline hierarchy to a plain‑text file
        using (StreamWriter writer = new StreamWriter(outputTxt))
        {
            WriteBookmarks(rootOutlines, writer, 0);
        }

        Console.WriteLine($"Bookmarks exported to '{outputTxt}'.");
    }

    // Recursive helper that writes each bookmark with indentation reflecting its level
    static void WriteBookmarks(IEnumerable<OutlineItemCollection> items, StreamWriter writer, int level)
    {
        foreach (OutlineItemCollection item in items)
        {
            // Indent 4 spaces per hierarchy level
            string indent = new string(' ', level * 4);
            writer.WriteLine($"{indent}{item.Title}");

            // If the current item has child bookmarks, recurse into them
            if (item.Count > 0)
            {
                WriteBookmarks(item, writer, level + 1);
            }
        }
    }
}