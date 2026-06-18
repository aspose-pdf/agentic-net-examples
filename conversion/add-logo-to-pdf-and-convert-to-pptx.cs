using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for ImageStamp and alignment enums

class Program
{
    static void Main()
    {
        // Input PDF, output PPTX, and logo image paths
        const string pdfPath = "input.pdf";
        const string finalPptxPath = "output_with_logo.pptx";
        const string logoPath = "company_logo.png";

        // Validate files
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

        // ------------------------------------------------------------
        // 1. Load PDF, stamp logo on each page, then convert to PPTX
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Define logo placement (in points). Adjust as needed.
            const float logoX = 10f;   // distance from left edge
            const float logoY = 10f;   // distance from top edge
            const float logoWidth = 100f;
            const float logoHeight = 50f;

            // Add the logo to every page of the PDF
            foreach (Page page in pdfDoc.Pages)
            {
                var logoStamp = new ImageStamp(logoPath)
                {
                    Width = logoWidth,
                    Height = logoHeight,
                    XIndent = logoX,
                    YIndent = logoY,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                page.AddStamp(logoStamp);
            }

            // Save the modified document directly as PPTX
            pdfDoc.Save(finalPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion completed. PPTX with logo saved to '{finalPptxPath}'.");
    }
}
