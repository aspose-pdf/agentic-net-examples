using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository and related text types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_odd_rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Iterate over pages and apply the rotated text stamp to every odd‑numbered page
        int pageCount = pdfDocument.Pages.Count;
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            if (pageNumber % 2 == 1) // odd page
            {
                TextStamp textStamp = new TextStamp("Rotated Text");
                textStamp.TextState.Font = FontRepository.FindFont("Arial");
                textStamp.TextState.FontSize = 14;
                textStamp.TextState.ForegroundColor = Color.Blue;
                // Use the correct property for rotation
                textStamp.RotateAngle = 30f; // rotates 30 degrees around the stamp's centre
                // Position the stamp on the left margin, vertically centred
                textStamp.HorizontalAlignment = HorizontalAlignment.Left;
                textStamp.VerticalAlignment = VerticalAlignment.Center;
                textStamp.XIndent = 10; // distance from the left edge
                textStamp.YIndent = 0;  // top of the page (adjust as needed)

                pdfDocument.Pages[pageNumber].AddStamp(textStamp);
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Rotated text stamp applied to odd pages. Output saved to '{outputPath}'.");
    }
}
