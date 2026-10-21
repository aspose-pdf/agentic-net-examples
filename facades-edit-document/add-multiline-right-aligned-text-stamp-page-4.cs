using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Create a TextStamp with multiline content
            TextStamp textStamp = new TextStamp("First line\r\nSecond line\r\nThird line")
            {
                // Align the stamp to the right side of the page
                HorizontalAlignment = HorizontalAlignment.Right,
                // Align the stamp to the bottom of the page
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional margin from the bottom edge (in points)
                BottomMargin        = 10,
                // Optional margin from the right edge (in points)
                RightMargin         = 10
            };

            // Configure the appearance of the text
            textStamp.TextState.Font = FontRepository.FindFont("Arial");
            textStamp.TextState.FontSize = 12;
            textStamp.TextState.ForegroundColor = Color.Black;

            // Apply the stamp to page 4 (pages are 1‑based)
            doc.Pages[4].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added to page 4 and saved as '{outputPath}'.");
    }
}
