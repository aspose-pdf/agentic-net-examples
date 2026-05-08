using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logoImagePath = "logo.png";

        // Verify that the required files exist
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

        // Load the PDF document (create‑load‑save lifecycle)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; get the first page
            Page firstPage = pdfDoc.Pages[1];

            // Create an ImageStamp for the logo image
            ImageStamp logoStamp = new ImageStamp(logoImagePath);

            // Center the stamp horizontally and vertically on the page
            logoStamp.HorizontalAlignment = HorizontalAlignment.Center;
            logoStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Ensure the stamp appears in front of page content
            logoStamp.Background = false;

            // Add the stamp to the first page only
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF (save‑rule)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Company logo added to first page and saved as '{outputPdfPath}'.");
    }
}