using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, TextStamp, TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using Document (PdfFileEditor does not support stamping)
        Document pdfDoc = new Document(inputPath);

        // Create a multiline text stamp
        TextStamp stamp = new TextStamp("First line\nSecond line\nThird line")
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center
        };

        // Set custom font (Arial), size 14, and blue color
        stamp.TextState.Font = FontRepository.FindFont("Arial");
        stamp.TextState.FontSize = 14;
        stamp.TextState.ForegroundColor = Color.Blue;

        // Apply the stamp to every page in the document
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the stamped PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
