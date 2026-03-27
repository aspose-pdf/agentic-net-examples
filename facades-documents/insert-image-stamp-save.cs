using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp and alignment enums are here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the source PDF document
        Document pdfDoc = new Document(inputPath);

        // Create an image stamp (Aspose.Pdf.Drawing.ImageStamp)
        ImageStamp imgStamp = new ImageStamp(stampImagePath)
        {
            // IsBackground property is not available in the current Aspose.Pdf version;
            // the default behavior places the stamp over page content.
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Opacity = 0.5f
        };

        // Apply the stamp to every page in the document
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(imgStamp);
        }

        // Save the modified PDF to the destination file
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
