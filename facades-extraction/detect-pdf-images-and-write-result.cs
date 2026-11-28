using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "hasImages.txt";

        // Verify the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        bool containsImage = false;

        // PdfExtractor implements IDisposable – wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdfPath);

            // Check for the presence of at least one image
            // HasNextImage() returns true while there are more images to read
            while (extractor.HasNextImage())
            {
                containsImage = true;
                // No need to extract the image; we can stop after the first detection
                break;
            }
        }

        // Write the result ("true" or "false") to the output text file
        File.WriteAllText(outputTxtPath, containsImage.ToString().ToLower());

        Console.WriteLine($"Image detection result written to '{outputTxtPath}': {containsImage}");
    }
}