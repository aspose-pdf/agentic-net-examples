using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "fonts.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve all fonts used in the document
            var fonts = doc.FontUtilities.GetAllFonts();

            // Write the font names to a CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // CSV header
                writer.WriteLine("FontName");

                // Each font name on a separate line
                foreach (var fontInfo in fonts)
                {
                    // FontInfo.FontName holds the name of the font
                    writer.WriteLine(fontInfo.FontName);
                }
            }
        }

        Console.WriteLine($"Font list saved to '{outputCsvPath}'.");
    }
}