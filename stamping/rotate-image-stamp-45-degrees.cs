using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";      // source PDF
        const string stampImage = "logo.png";    // image to use as stamp
        const string outputPdf = "output.pdf";   // result PDF

        // Ensure files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF document (lifecycle: create/load)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create an image stamp from the file
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Rotate the stamp by an arbitrary angle (45 degrees)
            imgStamp.RotateAngle = 45;   // property from Aspose.Pdf.ImageStamp

            // Position the stamp (example: top‑right corner)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;
            // Use XIndent/YIndent instead of the non‑existent Margin property
            imgStamp.XIndent = 10; // offset from the right edge (points)
            imgStamp.YIndent = 10; // offset from the top edge (points)

            // Add the stamp to the page
            page.AddStamp(imgStamp);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with rotated image stamp: {outputPdf}");
    }
}
