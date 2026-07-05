using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Example list of image files to add
        string[] imageFiles = new string[]
        {
            "image1.jpg",
            "image2.png",
            "missing.jpg"   // this will trigger an exception
        };

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Create the PdfFileMend facade (create rule)
        PdfFileMend mender = new PdfFileMend(inputPdf, outputPdf);

        // Add each image to page 1 at specified coordinates
        foreach (string imgPath in imageFiles)
        {
            try
            {
                // Verify image file exists before attempting to add
                if (!File.Exists(imgPath))
                    throw new FileNotFoundException("Image file not found.", imgPath);

                // AddImage returns true on success; coordinates are example values
                bool added = mender.AddImage(imgPath, 1, 10, 10, 200, 200);
                if (!added)
                {
                    // If AddImage returns false, treat it as a failure
                    Console.Error.WriteLine($"Failed to add image (method returned false): {imgPath}");
                }
            }
            catch (Exception ex)
            {
                // Log the problematic image path and exception details
                Console.Error.WriteLine($"Error adding image '{imgPath}': {ex.Message}");
            }
        }

        // Close the facade to finalize the output PDF (save rule)
        mender.Close();

        Console.WriteLine($"Image insertion completed. Output saved to '{outputPdf}'.");
    }
}