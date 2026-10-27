using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPdfPath = "reencrypted_output.pdf";
        const string imagesOutputDir = "ExtractedImages";

        const string userPassword = "user123";
        const string newOwnerPassword = "newOwner456";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(imagesOutputDir);

        try
        {
            // Open the encrypted PDF using the user password.
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Decrypt the document.
                doc.Decrypt();

                // Extract all images from each page.
                int imgCounter = 1;
                for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
                {
                    Page page = doc.Pages[i];
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Determine a safe file extension (default to png).
                        string ext = GetImageExtension(img);
                        string imgPath = Path.Combine(imagesOutputDir, $"image_{imgCounter}.{ext}");

                        // Save the image to a file via a FileStream (XImage.Save expects a Stream).
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        Console.WriteLine($"Saved image {imgCounter} to {imgPath}");
                        imgCounter++;
                    }
                }

                // Re‑encrypt with the same user password and a new owner password.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Re‑encrypted PDF saved to '{outputPdfPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper that returns a default image file extension.
    // Aspose.Pdf's XImage does not expose format information in older versions,
    // so we default to PNG which works for the majority of raster images.
    private static string GetImageExtension(XImage img)
    {
        // If future versions expose format info, this method can be extended.
        return "png";
    }
}
