using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where watermarked PDFs will be written
        const string outputFolder = "OutputPdfs";
        // Path to the image that will be used as the watermark
        const string watermarkImagePath = "watermark.png";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Create a single ImageStamp instance to reuse for all documents.
        // Reusing the same instance avoids reloading the image file repeatedly.
        ImageStamp watermarkStamp = new ImageStamp(watermarkImagePath)
        {
            Opacity = 0.5,                     // 50% transparent
            Background = false,                // place on top of page content
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center,
            // Optional: Scale = 0.5, // 50% of original size
            // Optional: Rotate = 45   // degrees
        };

        // Iterate over every PDF file in the input folder.
        foreach (string sourcePdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(sourcePdfPath);
            string targetPath = Path.Combine(outputFolder, fileName);

            // Load the PDF, add the watermark to each page, and save.
            using (Document doc = new Document(sourcePdfPath))
            {
                foreach (Page page in doc.Pages)
                {
                    // Add the same ImageStamp instance to each page.
                    page.AddStamp(watermarkStamp);
                }

                // Save the modified PDF.
                doc.Save(targetPath);
            }

            Console.WriteLine($"Watermarked PDF saved: {targetPath}");
        }
    }
}
