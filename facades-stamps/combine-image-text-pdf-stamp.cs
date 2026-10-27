using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextState, FontRepository

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath  = "logo.png";
        const string customText = "Confidential";

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Create an ImageStamp for the logo
        ImageStamp imgStamp = new ImageStamp(logoPath)
        {
            Background = false,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment   = VerticalAlignment.Top,
            XIndent = 20, // distance from left edge
            YIndent = 20  // distance from top edge
        };

        // Create a TextStamp for the custom text
        TextStamp txtStamp = new TextStamp(customText)
        {
            Background = false,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment   = VerticalAlignment.Bottom,
            XIndent = 20, // distance from right edge
            YIndent = 20  // distance from bottom edge
        };
        // Configure text appearance
        txtStamp.TextState.FontSize = 14;
        txtStamp.TextState.Font = FontRepository.FindFont("Arial");
        txtStamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(1, 0, 0); // red

        // Apply both stamps to every page
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(imgStamp);
            page.AddStamp(txtStamp);
        }

        // Save the result
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
