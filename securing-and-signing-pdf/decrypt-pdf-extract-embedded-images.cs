using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";   // Path to the encrypted PDF
        const string userPassword = "userpass";    // User password for opening the PDF
        const string outputDir = "ExtractedImages"; // Directory to store extracted images

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF using the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Decrypt the document (removes encryption)
                doc.Decrypt();

                // Optionally save a decrypted copy (not required for image extraction)
                string decryptedPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "decrypted.pdf");
                using (FileStream decryptedStream = new FileStream(decryptedPath, FileMode.Create, FileAccess.Write))
                {
                    doc.Save(decryptedStream);
                }

                int imageIndex = 1;

                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Iterate over all images defined in the page resources
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Save each image as PNG (you can change the extension if needed)
                        string imagePath = Path.Combine(outputDir, $"page{pageNum}_img{imageIndex}.png");
                        using (FileStream imgStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(imgStream);
                        }
                        Console.WriteLine($"Saved image: {imagePath}");
                        imageIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
