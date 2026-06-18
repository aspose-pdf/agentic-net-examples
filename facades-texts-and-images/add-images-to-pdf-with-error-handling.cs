using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // List of image file paths to add
        string[] imagePaths = { "image1.jpg", "image2.png", "image3.bmp" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Initialize PdfFileMend with the source and destination PDF files
        PdfFileMend mender = new PdfFileMend(inputPdf, outputPdf);

        foreach (string imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found: {imgPath}");
                continue;
            }

            try
            {
                // Attempt to add the image to page 1 at the specified rectangle
                // Coordinates: lower-left (10,10), upper-right (100,100)
                bool added = mender.AddImage(imgPath, 1, 10, 10, 100, 100);
                if (!added)
                {
                    Console.Error.WriteLine($"AddImage returned false for: {imgPath}");
                }
            }
            catch (Exception ex)
            {
                // Log the problematic image path and the exception details
                Console.Error.WriteLine($"Error adding image '{imgPath}': {ex.Message}");
            }
        }

        // Finalize and save the modified PDF
        mender.Close();
    }
}