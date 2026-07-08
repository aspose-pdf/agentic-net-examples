using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp and related enums are here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF with header
        const string logoPath  = "company_logo.png"; // logo image file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp that will be used as the header logo.
            // ImageStamp lives in Aspose.Pdf.Drawing, not in the Facades namespace.
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Center the stamp horizontally and place it at the top of the page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Top,
                // Optional small offset from the top edge.
                YIndent = 10
                // No IsBackground property in current API – default is foreground (over page content).
            };

            // Apply the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header with logo added to all pages. Saved as '{outputPdf}'.");
    }
}
