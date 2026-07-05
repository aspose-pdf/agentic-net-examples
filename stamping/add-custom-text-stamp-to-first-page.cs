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
            // Create a TextState with custom font, size, and blue color
            TextState ts = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"), // custom font
                FontSize = 24,                               // custom size
                ForegroundColor = Aspose.Pdf.Color.Blue     // blue color
            };

            // Initialize the TextStamp with the desired text and the TextState
            TextStamp stamp = new TextStamp("Sample Text", ts)
            {
                // Position the stamp on the page (coordinates are from bottom-left)
                XIndent = 100, // horizontal position
                YIndent = 700, // vertical position
                // Optional: set alignment or margins if needed
                // HorizontalAlignment = HorizontalAlignment.Left,
                // VerticalAlignment = VerticalAlignment.Bottom
            };

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}