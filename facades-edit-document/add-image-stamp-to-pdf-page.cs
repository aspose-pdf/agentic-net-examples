using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the source PDF to obtain dimensions of page 3.
        using (Document doc = new Document(inputPdf))
        {
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            Page page3 = doc.Pages[3];
            double pageWidth = page3.PageInfo.Width;
            double pageHeight = page3.PageInfo.Height;

            // Define stamp size (in points). Adjust as needed.
            const float stampWidth = 50f;
            const float stampHeight = 50f;

            // Compute origin so the stamp appears at the bottom‑right corner.
            // Origin is measured from the lower‑left corner of the page.
            float originX = (float)(pageWidth - stampWidth);
            float originY = 0f; // bottom edge

            // Configure the image stamp.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(logoPath);
            stamp.SetImageSize(stampWidth, stampHeight);
            stamp.SetOrigin(originX, originY);
            // Apply the stamp only to page 3.
            stamp.Pages = new int[] { 3 };

            // Use the facade to apply the stamp.
            PdfFileStamp fileStamp = new PdfFileStamp(doc);
            fileStamp.AddStamp(stamp);
            // Save the result.
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPdf}'.");
    }
}