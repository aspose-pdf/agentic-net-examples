using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";
        const string logoImagePath = "logo.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Add the logo to every page before conversion
            foreach (Page page in pdfDoc.Pages)
            {
                var logoStamp = new ImageStamp(logoImagePath)
                {
                    // Position (points) – adjust as needed
                    XIndent = 10,
                    YIndent = 10,
                    Width = 100,
                    Height = 50,
                    // Align top‑left corner
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                page.AddStamp(logoStamp);
            }

            // Convert the PDF (now containing the logo) to PPTX directly using Aspose.Pdf
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion completed. Logo added to each slide in '{outputPptxPath}'.");
    }
}
