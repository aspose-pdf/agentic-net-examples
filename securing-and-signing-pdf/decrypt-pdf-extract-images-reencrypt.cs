using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "re_encrypted_output.pdf";
        const string extractedImagesDir = "ExtractedImages";
        const string existingPassword = "oldPassword"; // password of the source PDF
        const string newOwnerPassword = "newOwner123"; // new owner password
        const string userPassword = ""; // no user password after re‑encryption

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(extractedImagesDir);

        try
        {
            // Open the encrypted PDF using the known password
            using (Document doc = new Document(inputPath, existingPassword))
            {
                // Decrypt the document (no parameters)
                doc.Decrypt();

                // Extract all images from each page
                int imageIndex = 1;
                foreach (Page page in doc.Pages)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        string imgPath = Path.Combine(extractedImagesDir, $"image_{imageIndex}.png");
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs); // Save image to file
                        }
                        imageIndex++;
                    }
                }

                // Re‑encrypt the document with a new owner password (no user password)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decryption, image extraction, and re‑encryption completed.");
            Console.WriteLine($"Images saved to: {extractedImagesDir}");
            Console.WriteLine($"Re‑encrypted PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}