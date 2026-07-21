using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string logoPath  = "logo.png";    // image to use as stamp

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

        // Load the document to obtain page dimensions (needed for positioning)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF must contain at least three pages.");
                return;
            }

            // -----------------------------------------------------------------
            // Create a PdfFileStamp facade and configure input / output files
            // -----------------------------------------------------------------
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdf);               // use BindPdf instead of obsolete InputFile

            // -----------------------------------------------------------------
            // Create the image stamp
            // -----------------------------------------------------------------
            // Fully qualify the Stamp type to avoid ambiguity between Aspose.Pdf.Stamp and Aspose.Pdf.Facades.Stamp
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the image that will be used as the stamp
            stamp.BindImage(logoPath);

            // Define the desired size of the stamp (width, height) in points
            const float stampWidth  = 100f; // adjust as needed
            const float stampHeight = 50f;  // adjust as needed
            stamp.SetImageSize(stampWidth, stampHeight);

            // Position the stamp at the bottom‑right corner of page 3
            // PDF coordinate origin is bottom‑left, so X = pageWidth - stampWidth - margin
            // Y = margin from the bottom.
            const float margin = 10f; // margin from edges
            float pageWidth = (float)doc.Pages[3].PageInfo.Width; // cast double to float
            float originX = pageWidth - stampWidth - margin;
            float originY = margin; // bottom margin

            stamp.SetOrigin(originX, originY);

            // Apply the stamp only to page 3
            stamp.Pages = new int[] { 3 };

            // -----------------------------------------------------------------
            // Add the stamp to the PDF and finalize
            // -----------------------------------------------------------------
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPdf);                 // use Save instead of obsolete OutputFile
            fileStamp.Close();                         // optional, releases resources
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}
