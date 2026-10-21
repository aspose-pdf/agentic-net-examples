using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDir = @"C:\PdfInput";
        // Directory where stamped PDFs will be saved
        const string outputDir = @"C:\PdfOutput";
        // Path to the image that will be used as a stamp
        const string stampImagePath = @"C:\stamp.png";
        // Rotation angle for the stamp (in degrees)
        const float rotationAngle = 45f;

        // Validate input folder and stamp image
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputDir);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_stamped.pdf");

            // Load the PDF using Document (PdfFileEditor does not support BindPdf/AddStamp/Save)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Create an image stamp and configure its rotation and alignment
                ImageStamp stamp = new ImageStamp(stampImagePath)
                {
                    RotateAngle = rotationAngle,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Add the stamp to the first page (pages are 1‑based)
                pdfDoc.Pages[1].AddStamp(stamp);

                // Save the modified PDF to the output location
                pdfDoc.Save(outputPath);
            }
        }

        Console.WriteLine("Batch stamping completed.");
    }
}
