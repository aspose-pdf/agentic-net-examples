using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string logoPath = "logo.png";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare an image stamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                Background = false,
                HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right,
                VerticalAlignment = Aspose.Pdf.VerticalAlignment.Bottom,
                XIndent = 10,
                YIndent = 10
            };

            // Apply the logo to every page
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Convert the stamped PDF to PPTX
            PptxSaveOptions pptxOpts = new PptxSaveOptions();
            pdfDoc.Save(outputPptxPath, pptxOpts);
        }

        Console.WriteLine($"PDF converted to PPTX with logo added: {outputPptxPath}");
    }
}