using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        Document doc = new Document(inputPath);

        // Configure a page‑number stamp
        PageNumberStamp pageNumberStamp = new PageNumberStamp
        {
            // Use Arabic numerals
            NumberingStyle = NumberingStyle.NumeralsArabic,
            // Start numbering at 1 (default, kept for clarity)
            StartingNumber = 1,
            // Format string with leading zeros (001, 002, ...)
            // {0:000} pads the page number to three digits
            Format = "{0:000}",
            // Position the stamp at the bottom‑center of each page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            YIndent = 20f // distance from the bottom edge
            // Note: PageNumberStamp does not expose an IsBackground property; it always renders in front.
        };

        // Apply the stamp to every page in the document
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(pageNumberStamp);
        }

        // Save the modified PDF to the output path
        doc.Save(outputPath);

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
