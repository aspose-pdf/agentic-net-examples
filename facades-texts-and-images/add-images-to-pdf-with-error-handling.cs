using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // List of image files to add
        string[] imagePaths = { "image1.jpg", "image2.png", "image3.bmp" };

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileMend facade with input and output files
        PdfFileMend mend = new PdfFileMend(inputPdf, outputPdf);

        foreach (string imgPath in imagePaths)
        {
            // Skip missing image files but continue processing others
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found: {imgPath}");
                continue;
            }

            try
            {
                // Attempt to add the image to page 1 at the specified rectangle
                // (lowerLeftX, lowerLeftY) = (10,10), (upperRightX, upperRightY) = (100,100)
                bool added = mend.AddImage(imgPath, 1, 10, 10, 100, 100);

                // If the method returns false, treat it as a failure
                if (!added)
                {
                    Console.Error.WriteLine($"AddImage returned false for: {imgPath}");
                }
            }
            catch (Exception ex)
            {
                // Log the problematic image path along with the exception details
                Console.Error.WriteLine($"Exception while adding image '{imgPath}': {ex.Message}");
            }
        }

        // Finalize and write the output PDF
        mend.Close();

        Console.WriteLine($"Processing complete. Output saved to '{outputPdf}'.");
    }
}