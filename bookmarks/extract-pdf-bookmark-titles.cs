using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";
        const string txtPath = "titles.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF without modifying it
        using (Document doc = new Document(pdfPath))
        using (StreamWriter writer = new StreamWriter(txtPath, false))
        {
            // Recursively write all bookmark titles
            WriteOutlines(doc.Outlines, writer);
        }

        Console.WriteLine($"Bookmark titles written to '{txtPath}'.");
    }

    // Recursive helper to traverse any outline collection (root or child)
    static void WriteOutlines(System.Collections.Generic.IEnumerable<OutlineItemCollection> outlines,
                              StreamWriter writer)
    {
        foreach (OutlineItemCollection item in outlines)
        {
            writer.WriteLine(item.Title);
            // An OutlineItemCollection is itself enumerable for its children
            if (item.Count > 0)
            {
                WriteOutlines(item, writer);
            }
        }
    }
}
