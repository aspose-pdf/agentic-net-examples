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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextState to define font, size and color
            TextState textState = new TextState
            {
                // FontRepository is in Aspose.Pdf.Text
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 24, // custom size
                // Use Aspose.Pdf.Color for cross‑platform color
                ForegroundColor = Aspose.Pdf.Color.Blue
            };

            // Create the TextStamp with the desired value and the TextState
            TextStamp stamp = new TextStamp("Custom Text Stamp", textState)
            {
                // Position the stamp on the page (example coordinates)
                XIndent = 100, // distance from left edge
                YIndent = 700, // distance from bottom edge
                // Optional: set alignment or other properties as needed
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom
            };

            // Add the stamp to the first page (page indexing is 1‑based)
            doc.Pages[1].AddStamp(stamp);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}