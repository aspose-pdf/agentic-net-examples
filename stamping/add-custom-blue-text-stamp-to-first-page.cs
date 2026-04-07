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
            // Define text appearance: custom font, size, and blue color
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 24,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Create a TextStamp with the desired text and appearance
            TextStamp stamp = new TextStamp("Custom Stamp", textState)
            {
                // Position the stamp on the page (example coordinates)
                XIndent = 100,
                YIndent = 700,
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