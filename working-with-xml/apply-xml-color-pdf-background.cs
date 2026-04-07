using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // For Color parsing (Color is in Aspose.Pdf namespace, but Parse is available here)

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path
        const string outputPath = "output_colored.pdf";
        // XML color value (e.g., "#FFCC00" or "rgb(255,204,0)")
        const string xmlColor = "#FFCC00";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Parse the XML color string into an Aspose.Pdf.Color instance
            Aspose.Pdf.Color bgColor = Aspose.Pdf.Color.Parse(xmlColor);

            // Apply the background color to each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Page is the Aspose.Pdf.Page class
                Aspose.Pdf.Page page = doc.Pages[i];
                page.Background = bgColor;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom background color to '{outputPath}'.");
    }
}