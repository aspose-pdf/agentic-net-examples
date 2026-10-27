using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "footer.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document using the Document class (PdfFileEditor has no BindPdf).
        Document pdfDoc = new Document(inputPath);

        // Create an image stamp positioned as a footer 10 points above the bottom edge.
        ImageStamp footerStamp = new ImageStamp(stampImagePath)
        {
            // Align to the bottom of each page.
            VerticalAlignment = VerticalAlignment.Bottom,
            // Center horizontally.
            HorizontalAlignment = HorizontalAlignment.Center,
            // Set bottom margin to 10 points.
            BottomMargin = 10,
            // Place the stamp over page content (false = foreground).
            Background = false
        };

        // Apply the stamp to every page by iterating over Document.Pages.
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(footerStamp);
        }

        // Save the resulting PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Footer stamp applied and saved to '{outputPath}'.");
    }
}