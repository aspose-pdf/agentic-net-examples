using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath = "logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the source PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the PdfFileStamp facade with the loaded document
            PdfFileStamp fileStamp = new PdfFileStamp(pdfDoc);

            // Create a Aspose.Pdf.Facades.Stamp object (facade) and bind the image from a stream
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                stamp.BindImage(imgStream);
            }

            // Configure stamp appearance
            stamp.SetOrigin(100, 500);          // X and Y coordinates (bottom‑left origin)
            stamp.SetImageSize(150, 100);       // Width and height of the image
            stamp.Opacity = 0.5f;               // Semi‑transparent
            stamp.IsBackground = false;         // Place on top of page content

            // Add the stamp to the document (applies to all pages by default)
            fileStamp.AddStamp(stamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}