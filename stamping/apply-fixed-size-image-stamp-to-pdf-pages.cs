using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create an ImageStamp with fixed dimensions (e.g., 100x100 points)
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Fixed size regardless of page dimensions
                Width  = 100,   // points
                Height = 100,   // points

                // Position the stamp at a fixed location (e.g., 50 points from left and bottom)
                LeftMargin   = 50,
                BottomMargin = 50,

                // Ensure the stamp is placed on top of page content
                Background = false
            };

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied with fixed size and saved to '{outputPdf}'.");
    }
}