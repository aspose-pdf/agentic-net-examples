using System;
using System.IO;
using Aspose.Pdf;               // Core API for Document, Page, ImageStamp, etc.
using Aspose.Pdf.Facades;      // For PdfFileStamp if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string logoImage = "logo.png";    // logo to stamp
        const string outputPdf = "output.pdf";  // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the logo file
            ImageStamp logoStamp = new ImageStamp(logoImage);

            // Align the stamp to the right margin of the page
            logoStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;

            // Optional: set vertical alignment, margins, opacity, etc.
            // logoStamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center;
            // logoStamp.Opacity = 0.8f;

            // Apply the stamp to each page (or a specific page by index)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo stamp aligned to the right margin saved as '{outputPdf}'.");
    }
}