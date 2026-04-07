using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF
        const string stampImg  = "stamp.png";   // image to use as stamp

        // Verify required files exist
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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // ----- Preserve existing XMP metadata -----
            // Retrieve current XMP metadata into a memory stream
            MemoryStream xmpStream = new MemoryStream();
            doc.GetXmpMetadata(xmpStream);
            // Reset stream position so it can be read again later
            xmpStream.Position = 0;

            // ----- Create the image stamp -----
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp in the bottom‑right corner of each page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                Opacity             = 0.5f,   // semi‑transparent
                RightMargin         = 10,
                BottomMargin        = 10
            };

            // ----- Apply the stamp to every page -----
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // ----- Restore the XMP metadata -----
            doc.SetXmpMetadata(xmpStream);

            // ----- Save the modified PDF (PDF format) -----
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and PDF saved to '{outputPdf}'.");
    }
}