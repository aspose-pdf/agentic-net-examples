using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF
        const string imagePath = "stamp.png";   // image to use as stamp

        // Ensure the input files exist before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!System.IO.File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {imagePath}");
            return;
        }

        // PdfFileStamp is a disposable facade; use a using block for deterministic cleanup
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Specify input and output files
            fileStamp.InputFile  = inputPdf;
            fileStamp.OutputFile = outputPdf;

            // Bind the source PDF (required before adding stamps)
            fileStamp.BindPdf(inputPdf);

            // Create a stamp that uses an image
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the image file to the stamp
            stamp.BindImage(imagePath);

            // Position the stamp on the page (coordinates are from the lower‑left corner)
            stamp.SetOrigin(100f, 500f);          // X = 100, Y = 500
            stamp.SetImageSize(120f, 60f);        // Width = 120, Height = 60

            // Optional visual settings
            stamp.Opacity = 0.8f;                 // 80 % opacity
            stamp.IsBackground = false;           // place on top of page content

            // Apply the stamp only to the second page (pages are 1‑based)
            stamp.PageNumber = 2;

            // Add the configured stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Finalize and write the output file
            fileStamp.Close();
        }

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp added to page 2 and saved as '{outputPdf}'.");
    }
}