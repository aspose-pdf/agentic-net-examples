using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Disclaimer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define a rectangle at the top of the first page (A4 size assumed)
        // Coordinates: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
        Rectangle rect = new Rectangle(0, 750, 595, 800);

        // Create a TextBoxField for the disclaimer
        TextBoxField disclaimer = new TextBoxField(doc.Pages[1], rect)
        {
            PartialName = "Disclaimer",
            Value = string.Empty,
            // Set justified alignment
            TextHorizontalAlignment = HorizontalAlignment.Justify
        };

        // Add the field to the PDF form (page numbers are 1‑based)
        doc.Form.Add(disclaimer, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Disclaimer field added and saved to '{outputPath}'.");
    }
}