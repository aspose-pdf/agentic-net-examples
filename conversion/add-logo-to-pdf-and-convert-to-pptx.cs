using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, output PPTX and logo image paths
        const string pdfPath   = "input.pdf";
        const string pptxPath  = "output.pptx";
        const string logoPath  = "company_logo.png";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF, add the logo to each page, then convert to PPTX
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Prepare an ImageStamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Place the logo in the lower‑right corner of each page
                Background = false,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Use individual margin properties (Margin object does not exist)
                RightMargin = 20,
                BottomMargin = 20
            };

            // Apply the stamp to every page
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Convert the modified PDF to PPTX
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(pptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF converted to PPTX with logo added: {pptxPath}");
    }
}
