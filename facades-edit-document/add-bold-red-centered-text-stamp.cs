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
            // Verify that the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Configure the text appearance: bold Helvetica, size 24, red color
            TextState textState = new TextState
            {
                Font         = FontRepository.FindFont("Helvetica"),
                FontSize     = 24,
                FontStyle    = FontStyles.Bold,
                ForegroundColor = Color.Red
            };

            // Create a TextStamp with the desired value and the configured TextState
            TextStamp textStamp = new TextStamp("Bold Red Centered Text", textState)
            {
                // Center the stamp horizontally and vertically on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp only to page five (Aspose.Pdf uses 1‑based page indexing)
            doc.Pages[5].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added to page 5 and saved as '{outputPath}'.");
    }
}