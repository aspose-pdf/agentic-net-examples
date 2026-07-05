using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for FormattedText if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string imagePath  = "logo.png";

        // Validate input files
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp image not found: {imagePath}");
            return;
        }

        // Create a stamp (image based)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imagePath);               // Set image to be used as stamp
        stamp.SetOrigin(100, 500);                // Position (X, Y) from bottom‑left corner
        stamp.SetImageSize(150, 50);              // Width and height of the stamp
        stamp.Opacity = 0.5f;                     // Semi‑transparent
        stamp.IsBackground = false;               // Place stamp in foreground
        stamp.Pages = null;                       // Null means affect all pages

        // Apply the stamp to every page using PdfFileStamp facade
        using (Document doc = new Document(inputPath))
        {
            PdfFileStamp fileStamp = new PdfFileStamp(doc);
            fileStamp.AddStamp(stamp);            // Single AddStamp call for all pages
            fileStamp.Save(outputPath);           // Save the result
            fileStamp.Close();                    // Close the facade
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}