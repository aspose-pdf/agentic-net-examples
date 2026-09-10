using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the resulting PDF, and the images to add
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        string[] imagePaths = { "image1.jpg", "image2.png", "missing.jpg" };

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileMend facade with input and output files
        PdfFileMend mend = new PdfFileMend(inputPdf, outputPdf);

        // Iterate over each image and attempt to add it to page 1
        foreach (string imgPath in imagePaths)
        {
            try
            {
                // Ensure the image file exists before attempting to add it
                if (!File.Exists(imgPath))
                {
                    throw new FileNotFoundException("Image file not found", imgPath);
                }

                // Add the image to page 1 with specified rectangle coordinates
                // (lowerLeftX, lowerLeftY) = (10, 10), (upperRightX, upperRightY) = (100, 100)
                bool added = mend.AddImage(imgPath, 1, 10, 10, 100, 100);

                // If AddImage returns false, log the failure
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
        mend.Close();

        Console.WriteLine($"Processing complete. Output saved to '{outputPdf}'.");
    }
}