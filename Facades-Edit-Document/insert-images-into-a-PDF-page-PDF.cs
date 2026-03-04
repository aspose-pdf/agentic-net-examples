using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "picture.jpg";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Create the PdfFileMend facade with input and output files
            PdfFileMend mend = new PdfFileMend(inputPdfPath, outputPdfPath);

            // Add the image to page 1.
            // Coordinates are in points (1 inch = 72 points). Adjust as needed.
            // lower-left (10,10) to upper-right (200,200) defines the image rectangle.
            bool added = mend.AddImage(imagePath, 1, 10f, 10f, 200f, 200f);

            if (!added)
            {
                Console.Error.WriteLine("Failed to add the image to the PDF.");
            }

            // Close the facade to finalize and save the output PDF.
            mend.Close();

            Console.WriteLine($"Image inserted successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}