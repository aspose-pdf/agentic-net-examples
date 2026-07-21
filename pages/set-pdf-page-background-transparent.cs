using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For Color definitions (if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "overlay.pdf";      // PDF to be used as overlay
        const string outputPath = "overlay_transparent.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Set the background of each page to transparent
            foreach (Page page in doc.Pages)
            {
                // Aspose.Pdf.Color.Transparent represents a fully transparent color
                page.Background = Aspose.Pdf.Color.Transparent;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transparent overlay PDF saved to '{outputPath}'.");
    }
}