using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "re_encrypted_output.pdf";
        const string userPassword = "user123";
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Directory to store extracted images
        string imagesDir = Path.Combine(Path.GetDirectoryName(outputPath) ?? "", "extracted_images");
        Directory.CreateDirectory(imagesDir);

        try
        {
            // Load the encrypted PDF using the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Decrypt the document (password already supplied on load)
                doc.Decrypt();

                // Extract all images from each page
                int imageIndex = 1;
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    foreach (XImage img in page.Resources.Images)
                    {
                        string imgPath = Path.Combine(imagesDir, $"image_{imageIndex}.png");
                        // XImage.Save expects a Stream, so use a FileStream
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }
                        imageIndex++;
                    }
                }

                // Define permissions for the re‑encrypted PDF (example: allow printing and extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt with the same user password and a new owner password
                doc.Encrypt(userPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine("Decryption, image extraction, and re‑encryption completed.");
            Console.WriteLine($"Images saved to: {imagesDir}");
            Console.WriteLine($"Re‑encrypted PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
