using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths and password – adjust as needed
        const string inputPdfPath = "encrypted_input.pdf";
        const string outputDir    = "ExtractedImages";
        const string userPassword = "user123";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the encrypted PDF with the user password
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath, userPassword))
            {
                // Initialize the extractor and bind the loaded document
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(doc);
                    extractor.ExtractImage();

                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        string outPath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                        // GetNextImage requires a System.Drawing.Imaging.ImageFormat argument
                        extractor.GetNextImage(outPath, ImageFormat.Png);
                        Console.WriteLine($"Saved image {imageIndex} to {outPath}");
                        imageIndex++;
                    }

                    if (imageIndex == 1)
                    {
                        Console.WriteLine("No images were found in the PDF.");
                    }
                }
            }
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}