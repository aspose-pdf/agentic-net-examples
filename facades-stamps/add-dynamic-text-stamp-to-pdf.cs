using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string author     = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a text stamp with dynamic content using string interpolation
            TextStamp stamp = new TextStamp($"Author: {author}, Date: {DateTime.Now:yyyy-MM-dd}")
            {
                Background          = false,
                Opacity             = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Use cross‑platform Aspose.Pdf.Color
                TextState = { ForegroundColor = Aspose.Pdf.Color.Black }
            };

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the stamped PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}
