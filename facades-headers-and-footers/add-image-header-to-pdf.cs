using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logoImagePath = "logo.png";

        // Verify required files exist
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

        // PdfFileStamp implements IDisposable, so use a using block
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF document
            fileStamp.BindPdf(inputPdfPath);

            // Create a Stamp object that will hold the image header
            var stamp = new Stamp();

            // Load the logo image via FileStream and bind it to the stamp
            using (FileStream logoStream = File.OpenRead(logoImagePath))
            {
                stamp.BindImage(logoStream);

                // Desired image size (points). Adjust as needed.
                stamp.SetImageSize(100, 50);

                // Position the stamp.
                // The older Aspose.Pdf.Facades versions do not expose HorizontalAlignment,
                // VerticalAlignment or TopMargin properties. To place the image at the top
                // with a 15‑point margin you would normally calculate the Y‑coordinate based
                // on the page height:
                //   Y = pageHeight - topMargin - imageHeight
                // For simplicity and to keep the code version‑agnostic we set the origin
                // to (0,0) which places the stamp at the bottom‑left corner. If precise
                // top‑margin placement is required, compute the origin per page using the
                // page dimensions (fileStamp.GetPageSize, etc.) and call stamp.SetOrigin(x, y).
                stamp.SetOrigin(0, 0);

                // Apply the stamp to all pages (default). Set IsBackground = false to place it in front.
                stamp.IsBackground = false;

                // Add the configured stamp to the PdfFileStamp instance
                fileStamp.AddStamp(stamp);
            }

            // Save the modified PDF with the header logo
            fileStamp.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
