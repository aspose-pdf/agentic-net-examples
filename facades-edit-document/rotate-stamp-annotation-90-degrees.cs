using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "rotated_stamp.pdf"; // result PDF
        const string stampImg = "stamp.png";          // image to use as stamp

        // Verify required files exist.
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

        // Load the PDF document first – PdfFileStamp expects a Document, not a file path.
        using (Document doc = new Document(inputPdf))
        {
            // Initialise PdfFileStamp with the loaded Document.
            using (PdfFileStamp fileStamp = new PdfFileStamp(doc))
            {
                // Fully qualify the ambiguous Stamp type.
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

                // Bind the image that will be stamped onto each page.
                stamp.BindImage(stampImg);

                // Rotate the stamp 90 degrees clockwise to match the underlying image orientation.
                stamp.Rotation = 90f; // degrees

                // Optional visual tweaks.
                stamp.Opacity = 0.8f;          // semi‑transparent
                stamp.IsBackground = false;   // place stamp above page content

                // Add the configured stamp to the PDF (applies to all pages).
                fileStamp.AddStamp(stamp);

                // Write the output file.
                fileStamp.Save(outputPdf);
            }
        }

        Console.WriteLine($"Stamp applied with 90° rotation. Output saved to '{outputPdf}'.");
    }
}