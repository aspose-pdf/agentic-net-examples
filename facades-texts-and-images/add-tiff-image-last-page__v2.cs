using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string tiffImage = "image.tiff";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(tiffImage))
        {
            Console.Error.WriteLine($"TIFF image not found: {tiffImage}");
            return;
        }

        // Initialize PdfFileMend and bind the source PDF
        using (PdfFileMend mender = new PdfFileMend())
        {
            mender.BindPdf(inputPdf);

            // Determine the last page number (pages are 1‑based)
            int lastPage = mender.Document.Pages.Count;

            // Define image rectangle coordinates (example values)
            float lowerLeftX = 50f;
            float lowerLeftY = 50f;
            float upperRightX = 300f;
            float upperRightY = 300f;

            // Add the TIFF image to the last page
            bool added = mender.AddImage(tiffImage, lastPage, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add image to the PDF.");
            }

            // Save the modified PDF
            mender.Save(outputPdf);
        }

        Console.WriteLine($"Image added to last page. Saved as '{outputPdf}'.");
    }
}
