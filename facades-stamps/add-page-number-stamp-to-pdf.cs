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

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a text stamp that uses the {page_number} placeholder.
        // Aspose.Pdf automatically replaces {page_number} (and {page_count}) with the
        // appropriate values for each page when the stamp is applied.
        TextStamp pageNumberStamp = new TextStamp("Page {page_number}");
        pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
        pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
        pageNumberStamp.BottomMargin = 20; // 20 points from the bottom edge
        pageNumberStamp.TextState.FontSize = 12;
        pageNumberStamp.TextState.Font = FontRepository.FindFont("Arial");
        pageNumberStamp.TextState.ForegroundColor = Color.Black;

        // Load the PDF document, apply the stamp to every page, and save.
        Document pdfDocument = new Document(inputPath);
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(pageNumberStamp);
        }
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
