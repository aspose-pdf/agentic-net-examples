using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Collect page numbers (1‑based) that contain any text
        List<int> pagesWithText = new List<int>();

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Use TextAbsorber to detect text on the page
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[i].Accept(absorber);

                if (!string.IsNullOrWhiteSpace(absorber.Text))
                {
                    pagesWithText.Add(i);
                }
            }
        }

        if (pagesWithText.Count == 0)
        {
            Console.WriteLine("No pages with text found. No resizing performed.");
            return;
        }

        // Resize contents on the identified pages.
        // Here we shrink the content to 80% of its original width and height,
        // leaving automatic margins around the content.
        PdfFileEditor fileEditor = new PdfFileEditor();

        // newWidth and newHeight are specified in default space units (points).
        // For a proportional shrink we can use percentages via the overload that
        // accepts double values representing the new size in points.
        // Assuming a typical page width of 595 points (A4) and height of 842 points,
        // 80% corresponds to 476 and 674 points respectively.
        double newWidth  = 476; // 80% of typical width
        double newHeight = 674; // 80% of typical height

        // Perform the resize only on pages that contain text.
        fileEditor.ResizeContents(
            inputPath,
            outputPath,
            pagesWithText.ToArray(),
            newWidth,
            newHeight);

        Console.WriteLine($"Resizing completed. Output saved to '{outputPath}'.");
    }
}