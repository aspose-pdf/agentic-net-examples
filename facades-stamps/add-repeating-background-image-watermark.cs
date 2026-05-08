using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "watermarked.pdf"; // result PDF
        const string watermarkImage = "watermark.png"; // image to use as watermark

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(watermarkImage))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImage}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdf))
            {
                // Initialize the PdfFileStamp facade.
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.InputFile  = inputPdf;   // source file
                fileStamp.OutputFile = outputPdf;  // destination file

                // Create a single Aspose.Pdf.Facades.Stamp instance that will be applied to all pages.
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

                // Bind the image that will be used as the watermark.
                stamp.BindImage(watermarkImage);

                // Position the watermark (origin is measured from the lower‑left corner).
                stamp.SetOrigin(100, 200); // X = 100, Y = 200

                // Define the size of the image stamp (width, height).
                stamp.SetImageSize(150, 100);

                // Make the stamp a background element so it appears behind page content.
                stamp.IsBackground = true;

                // Set a semi‑transparent opacity (0.0 = fully transparent, 1.0 = opaque).
                stamp.Opacity = 0.5f;

                // Apply the stamp to all pages (null means every page).
                stamp.Pages = null;

                // Add the stamp to the document via the facade.
                fileStamp.AddStamp(stamp);

                // Persist changes and release resources.
                fileStamp.Close();
            }

            Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}