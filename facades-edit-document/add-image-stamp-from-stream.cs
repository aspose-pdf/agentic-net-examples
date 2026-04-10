using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Path to the image file (could be any source stream)
        const string imagePath = "logo.png";

        // Validate input files
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

        // Create the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Bind the source PDF
        fileStamp.BindPdf(inputPdfPath);

        // Create a stamp object
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the image from a stream without writing to disk
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            stamp.BindImage(imgStream);
        }

        // Optional: set stamp appearance
        stamp.SetOrigin(100, 500);          // Position (X, Y) from lower‑left corner
        stamp.SetImageSize(150, 100);       // Width, Height
        stamp.Opacity = 0.7f;               // Semi‑transparent
        stamp.IsBackground = false;        // Place on top of page content

        // Add the stamp to all pages of the PDF
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPdfPath);

        // Close the facade (releases resources)
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}