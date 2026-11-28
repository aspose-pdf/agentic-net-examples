using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string watermarkImage = "watermark.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        // Load the source PDF.
        Document pdfDoc = new Document(inputPdf);

        // Create an ImageStamp (Aspose.Pdf.Drawing) for the header image.
        ImageStamp headerStamp = new ImageStamp(watermarkImage)
        {
            // Center horizontally.
            HorizontalAlignment = HorizontalAlignment.Center,
            // Position at the top of the page.
            VerticalAlignment = VerticalAlignment.Top,
            // No top margin – distance from the top edge when using Top alignment.
            YIndent = 0,
            // Place the stamp over existing content.
            Background = false
        };

        // Apply the stamp to every page.
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(headerStamp);
        }

        // Save the resulting PDF.
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Header image added and saved to '{outputPdf}'.");
    }
}
