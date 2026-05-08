using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string xmlColor = "#FFCC00"; // Example XML color value

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the XML color string to an Aspose.Pdf.Color instance
            Aspose.Pdf.Color backgroundColor = Aspose.Pdf.Color.Parse(xmlColor);

            // Apply the background color to each page (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Background = backgroundColor;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom background to '{outputPath}'.");
    }
}