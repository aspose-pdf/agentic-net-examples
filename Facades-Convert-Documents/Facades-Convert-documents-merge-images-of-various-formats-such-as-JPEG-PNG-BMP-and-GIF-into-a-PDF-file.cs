using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // List of image files to merge (JPEG, PNG, BMP, GIF)
        string[] imageFiles = new string[]
        {
            "image1.jpg",
            "image2.png",
            "image3.bmp",
            "image4.gif"
        };

        string outputPdf = "merged.pdf";

        // Verify that all image files exist before processing
        foreach (var imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found: {imgPath}");
                return;
            }
        }

        try
        {
            // Initialize the PdfFileMend facade (creates a new PDF document internally)
            PdfFileMend pdfMend = new PdfFileMend();

            // Add each image on a separate page.
            // Page numbers are 1‑based; coordinates (0,0) to (595,842) cover an A4 page.
            for (int i = 0; i < imageFiles.Length; i++)
            {
                int pageNumber = i + 1;
                pdfMend.AddImage(imageFiles[i], pageNumber, 0, 0, 595, 842);
            }

            // Save the merged PDF using the facade's Save method
            pdfMend.Save(outputPdf);
            Console.WriteLine($"PDF created successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}