using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imageFile = "stamp.png";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {imageFile}");
            return;
        }

        // Load the source PDF (Document implements IDisposable)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the facade for stamping
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(doc);                     // Bind the loaded document

            // Create a generic stamp and bind the image
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imageFile);                  // Use the image as stamp content
            stamp.Rotation = 45f;                        // Rotate 45 degrees (arbitrary angle)
            stamp.Opacity = 0.8f;                        // Set opacity to 80%
            stamp.SetOrigin(100f, 200f);                 // Position (X=100, Y=200) from lower‑left corner

            // Apply the stamp to all pages (Pages = null means every page)
            fileStamp.AddStamp(stamp);

            // Save the result and release resources
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}