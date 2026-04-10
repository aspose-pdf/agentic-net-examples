using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for ImageStamp, HorizontalAlignment, VerticalAlignment

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPptxPath = "output_with_logo.pptx";
        const string logoPath = "logo.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Add the logo to every page before conversion
            foreach (Page page in pdfDoc.Pages)
            {
                var logoStamp = new ImageStamp(logoPath)
                {
                    // Position (points) – adjust as required
                    XIndent = 10,
                    YIndent = 10,
                    Width = 100,
                    Height = 50,
                    // Align top‑left
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                page.AddStamp(logoStamp);
            }

            // Convert the PDF (now containing the logo) to PPTX
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PDF converted to PPTX with logo added. Output saved to '{outputPptxPath}'.");
    }
}
