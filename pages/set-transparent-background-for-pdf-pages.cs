using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "overlay.pdf";
        const string outputPath = "overlay_transparent.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Optional: set the document‑level background to transparent
            doc.Background = Aspose.Pdf.Color.Transparent;

            // Set each page's background to transparent
            foreach (Page page in doc.Pages)
            {
                page.Background = Aspose.Pdf.Color.Transparent;
            }

            // Save the result
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transparent background PDF saved to '{outputPath}'.");
    }
}