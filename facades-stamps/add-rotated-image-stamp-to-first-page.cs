using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputDirectory = @"C:\PdfFolder";
        // Path to the image that will be used as a stamp
        const string stampImagePath = @"C:\stamp.png";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Process each PDF file in the directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Build output file name (original name + "_stamped.pdf")
            string outputPath = Path.Combine(
                inputDirectory,
                Path.GetFileNameWithoutExtension(pdfPath) + "_stamped.pdf");

            // Initialize the PdfFileStamp facade and bind the source PDF
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(pdfPath);

                // Configure the stamp
                Stamp stamp = new Stamp();
                stamp.BindImage(stampImagePath);               // use the image as stamp content
                stamp.Opacity = 0.7f;                          // make the stamp semi‑transparent
                stamp.Pages = new int[] { 1 };                 // apply only to the first page

                // Position the stamp (example: 100 points from left, 500 points from bottom)
                stamp.SetOrigin(100, 500);

                // Rotate the stamp by 45 degrees – use the Rotation property (degrees)
                stamp.Rotation = 45f;

                // Add the stamp to the document and save the result
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPath);
            }

            Console.WriteLine($"Stamped PDF saved to: {outputPath}");
        }
    }
}
