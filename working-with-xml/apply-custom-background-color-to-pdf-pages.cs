using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For Color parsing if needed (Color is in Aspose.Pdf namespace)

class ApplyPageBackground
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path
        const string outputPath = "output_colored.pdf";
        // Example XML color value (can be any valid CSS/HTML color string)
        const string xmlColor = "#FFCC00"; // Orange shade

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Parse the XML color string into an Aspose.Pdf.Color instance
            Aspose.Pdf.Color bgColor = Aspose.Pdf.Color.Parse(xmlColor);

            // Apply the background color to each page (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = bgColor;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom page background: {outputPath}");
    }
}