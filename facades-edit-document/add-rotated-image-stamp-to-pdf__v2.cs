using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp and alignment enums

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string stampImg = "stamp.png";
        const string outputPdf = "output.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document using the high‑level API
        Document pdfDocument = new Document(inputPdf);

        // Create the image stamp (same instance can be reused, but we set properties per page for clarity)
        ImageStamp imgStamp = new ImageStamp(stampImg)
        {
            RotateAngle = 45,   // 45‑degree rotation
            Opacity = 0.8,      // 80 % opacity
            // Positioning – XIndent/YIndent are measured from the lower‑left corner of the page
            XIndent = 100,
            YIndent = 200,
            // Ensure the stamp is drawn over existing content (Background = false)
            Background = false
        };

        // Apply the stamp to every page in the document
        foreach (Page page in pdfDocument.Pages)
        {
            // The stamp object can be added directly; Aspose.Pdf creates a copy internally.
            page.AddStamp(imgStamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}
