using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string extractedImagePath = "extracted_image.jpg";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists before proceeding.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: The file '{inputPdf}' was not found. Please place the PDF in the executable's working directory or provide a correct path.");
            return;
        }

        // ---------- Extract the first image from the PDF ----------
        // Use a using‑statement to guarantee proper disposal of the facade.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            if (extractor.HasNextImage())
            {
                // The file extension of the target path determines the image format.
                extractor.GetNextImage(extractedImagePath);
                Console.WriteLine($"Image extracted to '{extractedImagePath}'.");
            }
            else
            {
                Console.WriteLine("No images were found in the PDF.");
            }
        }

        // ---------- Reposition a stamp annotation ----------
        // Again, wrap the editor in a using‑statement.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // Move the first stamp on page 1 to new coordinates (X=100, Y=200).
            // If the page does not contain a stamp, Aspose.Pdf will silently ignore the call.
            editor.MoveStamp(pageNumber: 1, stampIndex: 1, x: 100, y: 200);

            // Save the modified PDF to a new file.
            editor.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved as '{outputPdf}'.");
        }
    }
}
