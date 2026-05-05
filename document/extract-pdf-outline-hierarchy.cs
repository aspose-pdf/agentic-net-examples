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

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Access the outline (bookmark) hierarchy
            Aspose.Pdf.OutlineCollection outlines = doc.Outlines;

            if (outlines == null || outlines.Count == 0)
            {
                Console.WriteLine("The document contains no outlines.");
                return;
            }

            Console.WriteLine("Outline hierarchy:");
            // Iterate over top‑level outline items
            for (int i = 1; i <= outlines.Count; i++) // 1‑based indexing
            {
                Aspose.Pdf.OutlineItemCollection item = outlines[i];
                PrintOutline(item, 0);
            }
        }
    }

    // Recursively prints an outline item and its children with indentation
    static void PrintOutline(Aspose.Pdf.OutlineItemCollection item, int level)
    {
        string indent = new string(' ', level * 2);
        Console.WriteLine($"{indent}- {item.Title}");

        // Traverse child items using the linked‑list style navigation (First/Next)
        Aspose.Pdf.OutlineItemCollection child = item.First;
        while (child != null)
        {
            PrintOutline(child, level + 1);
            child = child.Next;
        }
    }
}