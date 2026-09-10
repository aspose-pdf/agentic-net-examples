using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextState to define font, size, and color
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"), // custom font
                FontSize = 24,                               // custom size
                ForegroundColor = Aspose.Pdf.Color.Blue     // custom blue color
            };

            // Create the TextStamp with the desired text and the TextState
            TextStamp stamp = new TextStamp("Custom Text Stamp", textState)
            {
                // Position the stamp on the page (example coordinates)
                XIndent = 100,   // distance from the left edge
                YIndent = 700,   // distance from the bottom edge
                // Optional alignment settings
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top
            };

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}