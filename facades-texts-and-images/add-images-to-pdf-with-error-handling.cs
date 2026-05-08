using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and images to add
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        string[] images = { "image1.jpg", "image2.png", "image3.bmp" };

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify each image exists before processing
        foreach (string imgPath in images)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found and will be skipped: {imgPath}");
            }
        }

        // Initialize PdfFileMend facade with input and output files
        // PdfFileMend implements IDisposable via SaveableFacade, so we use using
        using (PdfFileMend mender = new PdfFileMend(inputPdf, outputPdf))
        {
            // Add each image to page 1; coordinates are example values
            foreach (string imgPath in images)
            {
                if (!File.Exists(imgPath))
                {
                    // Skip missing files
                    continue;
                }

                try
                {
                    // AddImage(string imagePath, int pageNum, float llx, float lly, float urx, float ury)
                    bool success = mender.AddImage(imgPath, 1, 10, 10, 200, 200);
                    if (!success)
                    {
                        Console.Error.WriteLine($"AddImage returned false for: {imgPath}");
                    }
                }
                catch (Exception ex)
                {
                    // Log the problematic image file path and exception details
                    Console.Error.WriteLine($"Failed to add image '{imgPath}': {ex.Message}");
                }
            }

            // Close the facade to finalize the output PDF
            mender.Close();
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
    }
}