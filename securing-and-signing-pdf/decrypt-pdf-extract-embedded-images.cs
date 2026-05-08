using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";   // path to the encrypted PDF
        const string userPassword = "user123";      // user password for opening the PDF
        const string outputDir = "ExtractedImages"; // folder where images will be saved

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF with the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Decrypt the document (required before saving or further processing)
                doc.Decrypt();

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    int imageCounter = 1;

                    // Iterate over all images defined in the page resources
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name for each extracted image
                        string fileName = Path.Combine(
                            outputDir,
                            $"page{pageNum}_img{imageCounter}.png");

                        // Save the image to disk using a FileStream (XImage.Save expects a Stream)
                        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        imageCounter++;
                    }
                }

                // Optionally save the now‑decrypted PDF (overwrites the original file)
                doc.Save(inputPath);
            }

            Console.WriteLine("All embedded images have been extracted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
