using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for FontRepository

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Determine the creation date: use the PDF metadata if present, otherwise fall back to current date
        string stampText;
        if (pdfDocument.Info != null && pdfDocument.Info.CreationDate != DateTime.MinValue)
        {
            stampText = pdfDocument.Info.CreationDate.ToString("yyyy-MM-dd");
        }
        else
        {
            stampText = DateTime.Now.ToString("yyyy-MM-dd");
        }

        // Configure a TextStamp that will be added to every page
        TextStamp textStamp = new TextStamp(stampText)
        {
            // Align to the top‑left corner with a small margin
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment   = VerticalAlignment.Top,
            XIndent = 10,   // 10 points from the left edge
            YIndent = 10,   // 10 points from the top edge
            Opacity = 0.8f
        };
        // Optional styling – font, size, colour
        textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        textStamp.TextState.FontSize = 12;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

        // Add the stamp to each page of the document
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(textStamp);
        }

        // Save the stamped PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
