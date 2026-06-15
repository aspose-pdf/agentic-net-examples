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
            // Set the document-wide background to transparent (optional)
            doc.Background = Aspose.Pdf.Color.Transparent;

            // Ensure each page has a transparent background
            foreach (Page page in doc.Pages)
            {
                page.Background = Aspose.Pdf.Color.Transparent;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transparent background PDF saved to '{outputPath}'.");
    }
}