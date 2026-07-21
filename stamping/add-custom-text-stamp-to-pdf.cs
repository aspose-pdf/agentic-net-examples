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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextState with custom font, size, and blue color
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"), // choose a font that exists
                FontSize = 24,                               // custom size
                ForegroundColor = Aspose.Pdf.Color.Blue    // blue color
            };

            // Create a TextStamp with the desired text and the custom TextState
            TextStamp stamp = new TextStamp("Sample Text Stamp", textState)
            {
                // Position the stamp (coordinates are from the bottom-left corner)
                XIndent = 100,   // horizontal offset
                YIndent = 700,   // vertical offset
                // Optional: alignments, margins, etc.
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom
            };

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}