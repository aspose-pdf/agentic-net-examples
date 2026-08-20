using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "overlay_input.pdf";
        const string outputPath = "overlay_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set each page's background to transparent
            foreach (Page page in doc.Pages)
            {
                page.Background = Aspose.Pdf.Color.Transparent;
            }

            // Optionally set the document-wide background to transparent as well
            doc.Background = Aspose.Pdf.Color.Transparent;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transparent background applied and saved to '{outputPath}'.");
    }
}