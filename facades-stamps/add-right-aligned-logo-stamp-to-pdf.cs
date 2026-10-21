using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string logoImagePath = "logo.png";
        const string outputPdfPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // Create an ImageStamp for the logo and set its alignment/opacity
        ImageStamp logoStamp = new ImageStamp(logoImagePath)
        {
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment   = VerticalAlignment.Top,
            Opacity = 1.0
        };

        // Load the PDF, apply the stamp to every page, and save the result
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(logoStamp);
            }
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Logo stamp applied and saved to '{outputPdfPath}'.");
    }
}
