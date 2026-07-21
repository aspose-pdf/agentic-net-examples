using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for ImageStamp alignment enums

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // source PDF
        const string logoImage  = "logo.png";    // company logo
        const string outputPptx = "output.pptx"; // resulting PPTX

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Prepare an ImageStamp that will be placed on every page
                ImageStamp logoStamp = new ImageStamp(logoImage)
                {
                    // Place the logo as a foreground element (not a background watermark)
                    Background = false,
                    // Semi‑transparent so it doesn't obscure page content
                    Opacity = 0.5f,
                    // Center the logo horizontally and position it near the top
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Optional offsets (in points) from the alignment position
                    XIndent = 0,
                    YIndent = 20
                };

                // Apply the stamp to each page of the PDF
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(logoStamp);
                }

                // Convert the modified PDF to PPTX
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"Conversion complete. PPTX saved to '{outputPptx}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}