using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // List of image file paths to add
        string[] imagePaths = { "image1.jpg", "image2.png", "missing.jpg" };

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Initialize PdfFileMend with source and destination files
        PdfFileMend mend = new PdfFileMend(inputPdf, outputPdf);

        // Target page and rectangle coordinates (lower‑left and upper‑right)
        int targetPage = 1;
        float lowerLeftX = 10f;
        float lowerLeftY = 10f;
        float upperRightX = 100f;
        float upperRightY = 100f;

        // Process each image, handling any exceptions that occur
        foreach (string imgPath in imagePaths)
        {
            try
            {
                // Attempt to add the image; AddImage returns true on success
                bool added = mend.AddImage(imgPath, targetPage, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
                if (!added)
                {
                    // The method reported failure without throwing
                    Console.Error.WriteLine($"AddImage returned false for: {imgPath}");
                }
            }
            catch (Exception ex)
            {
                // Log the problematic image file path and the exception message
                Console.Error.WriteLine($"Error adding image '{imgPath}': {ex.Message}");
            }
        }

        // Finalize and write the output PDF
        mend.Close();

        Console.WriteLine($"Processing complete. Output saved to '{outputPdf}'.");
    }
}