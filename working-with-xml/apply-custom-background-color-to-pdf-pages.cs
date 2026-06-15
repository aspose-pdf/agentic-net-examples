using System;
using System.IO;
using Aspose.Pdf;

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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Parse the XML color string to an Aspose.Pdf.Color object
            Color bgColor = Color.Parse(xmlColor);

            // Apply the background color to each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = bgColor;
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom background color to '{outputPath}'.");
    }
}