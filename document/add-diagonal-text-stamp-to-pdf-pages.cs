using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string message    = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the custom message
            TextStamp stamp = new TextStamp(message);

            // Configure visual appearance of the stamp
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 72;                     // large font size
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            stamp.Opacity = 0.5;                               // semi‑transparent
            stamp.Background = false;                          // draw on top of page content

            // Position the stamp so it runs across the page diagonally
            stamp.RotateAngle = 45;                            // rotate 45 degrees
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to each page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}