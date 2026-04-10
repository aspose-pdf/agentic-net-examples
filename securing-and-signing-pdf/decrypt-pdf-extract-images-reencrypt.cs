using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input encrypted PDF and its current user password
        const string inputPdfPath      = "encrypted_input.pdf";
        const string currentUserPwd    = "user123";

        // New owner password to apply after re‑encryption
        const string newOwnerPassword  = "newOwner456";

        // Output paths
        const string outputPdfPath     = "re_encrypted_output.pdf";
        const string imagesOutputDir   = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesOutputDir);

        try
        {
            // Open the encrypted PDF using the known user password
            using (Document doc = new Document(inputPdfPath, currentUserPwd))
            {
                // Decrypt the document (no parameters)
                doc.Decrypt();

                // -----------------------------------------------------------------
                // Extract all images from the document and save them to files
                // -----------------------------------------------------------------
                int imageIndex = 1;
                foreach (Page page in doc.Pages)
                {
                    // XImageCollection is iterable directly; no dictionary access
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Save each image as PNG using a FileStream (stream overload required)
                        string imagePath = Path.Combine(imagesOutputDir, $"image_{imageIndex}.png");
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }
                        imageIndex++;
                    }
                }

                // -----------------------------------------------------------------
                // Re‑encrypt the PDF with the same user password and a new owner password
                // -----------------------------------------------------------------
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(currentUserPwd, newOwnerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine("Decryption, image extraction, and re‑encryption completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
