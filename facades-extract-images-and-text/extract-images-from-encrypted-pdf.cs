using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "encrypted.pdf";   // path to the encrypted PDF
        const string userPassword = "user123";         // user password for the PDF
        const string outputFolder = "ExtractedImages"; // folder to store extracted images

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the encrypted PDF providing the user password directly in the Document constructor
            Document pdfDoc = new Document(inputPdfPath, userPassword);

            // PdfExtractor implements IDisposable – use a using block for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the already‑loaded Document (which already has the password applied)
                extractor.BindPdf(pdfDoc);

                // Extract all images from the document
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through extracted images and save each to a file
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(outputFolder, $"image_{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'");
                    imageIndex++;
                }

                if (imageIndex == 1)
                {
                    Console.WriteLine("No images were found in the PDF.");
                }
            }
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("The provided password is incorrect.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
