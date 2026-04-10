using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "encrypted.pdf";   // Path to the encrypted PDF
        const string userPassword   = "user123";         // User password for decryption
        const string outputFolder   = "ExtractedImages"; // Folder to store extracted images

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Open the encrypted PDF using the user password
            using (Document pdfDoc = new Document(inputPdfPath, userPassword))
            {
                // Decrypt the document (required before saving or further processing)
                pdfDoc.Decrypt();

                int imageIndex = 1; // Counter for naming extracted images

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Iterate over all images defined in the page resources
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name for each extracted image
                        string imageFileName = $"image_{imageIndex}_page{pageNum}.png";
                        string imagePath = Path.Combine(outputFolder, imageFileName);

                        // Save the image to the file system
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            // XImage.Save(Stream) writes the image in its original format.
                            // If a specific format is required, use the overload with ImageFormat.
                            img.Save(fs);
                        }

                        Console.WriteLine($"Extracted image {imageIndex} from page {pageNum} to '{imagePath}'.");
                        imageIndex++;
                    }
                }

                // Optionally, save the now‑decrypted PDF for verification
                string decryptedPdfPath = Path.Combine(outputFolder, "decrypted.pdf");
                pdfDoc.Save(decryptedPdfPath);
                Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}