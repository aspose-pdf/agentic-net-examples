using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, company logo image, and output PPTX paths
        const string pdfPath = "input.pdf";
        const string logoPath = "company_logo.png";
        const string pptxPath = "output.pptx";

        // Verify that required files exist
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

        // Load the PDF, add the logo to each page, then save as PPTX
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Apply the logo stamp to every page individually.
            // A stamp instance cannot be reused after it has been added to a page, so we create a fresh one per page.
            foreach (Page page in pdfDocument.Pages)
            {
                ImageStamp logoStamp = new ImageStamp(logoPath)
                {
                    Background = false,               // Place on top of page content
                    Opacity = 0.8f,                    // Slightly transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top
                    // Optional: set Width, Height, XIndent, YIndent if needed
                };
                page.AddStamp(logoStamp);
            }

            // Convert the modified PDF to PPTX using SaveFormat
            pdfDocument.Save(pptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with logo added: {pptxPath}");
    }
}
