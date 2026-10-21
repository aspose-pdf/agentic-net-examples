using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_rotated_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the high‑level API
        Document pdfDocument = new Document(inputPath);

        // Create a text stamp
        TextStamp stamp = new TextStamp("Rotated Text")
        {
            XIndent = 200,
            YIndent = 400,
            RotateAngle = 45f // rotates 45 degrees around the stamp's centre
        };

        // Set visual appearance
        stamp.TextState.FontSize = 20;
        stamp.TextState.ForegroundColor = Color.Blue;

        // Apply the stamp to every page
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved with rotated text stamp: {outputPath}");
    }
}
