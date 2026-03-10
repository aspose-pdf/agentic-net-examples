using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Load the source PDF, extract its headings (outlines) and save them as a Markdown file.
class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMd = "headings.md";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Extract headings (outlines/bookmarks) from the PDF.
            var outlines = ExtractHeadings(doc);

            // Write headings to Markdown file.
            using (StreamWriter writer = new StreamWriter(outputMd))
            {
                foreach (var outline in outlines)
                {
                    string level = new string('#', outline.Level);
                    writer.WriteLine($"{level} {outline.Title}");
                }
            }
        }

        Console.WriteLine($"Headings extracted and saved to '{outputMd}'.");
    }

    static List<(int Level, string Title)> ExtractHeadings(Document doc)
    {
        var headings = new List<(int Level, string Title)>();

        // Traverse the document outlines (bookmarks)
        if (doc.Outlines != null)
        {
            foreach (OutlineItemCollection outline in doc.Outlines)
            {
                CollectOutlines(outline, headings, 1);
            }
        }

        return headings;
    }

    static void CollectOutlines(OutlineItemCollection outline, List<(int Level, string Title)> headings, int level)
    {
        if (outline.Title != null)
        {
            headings.Add((level, outline.Title));
        }

        if (outline.Count > 0)
        {
            foreach (OutlineItemCollection child in outline)
            {
                CollectOutlines(child, headings, level + 1);
            }
        }
    }
}