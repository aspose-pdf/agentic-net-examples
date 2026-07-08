using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "encrypted_input.pdf";   // existing encrypted PDF
        const string userPassword = "user123";               // password that opens the PDF
        const string newOwnerPassword = "newOwner456";       // new owner password to set
        const string outputPdfPath = "reencrypted_output.pdf";

        // Directory to store extracted images
        const string imagesOutputDir = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output folder exists
        Directory.CreateDirectory(imagesOutputDir);

        // Open the encrypted PDF using the user password
        using (Document doc = new Document(inputPdfPath, userPassword))
        {
            // Decrypt the document so it can be saved without protection
            doc.Decrypt();

            // -----------------------------------------------------------------
            // Extract all images from the PDF and save them to files
            // -----------------------------------------------------------------
            int imageCounter = 1;
            foreach (Page page in doc.Pages) // 1‑based page indexing
            {
                // XImageCollection is enumerable; iterate directly
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each image
                    string imagePath = Path.Combine(
                        imagesOutputDir,
                        $"Page{page.Number}_Image{imageCounter}.png"); // default to PNG

                    // Save the image to disk via a FileStream (XImage.Save expects a Stream)
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved image: {imagePath}");
                    imageCounter++;
                }
            }

            // -----------------------------------------------------------------
            // Re‑encrypt the PDF with a new owner password
            // -----------------------------------------------------------------
            Permissions perms = Permissions.PrintDocument |
                                 Permissions.ModifyContent |
                                 Permissions.ExtractContent |
                                 Permissions.AssembleDocument;

            // User password left empty (no user password), new owner password set
            doc.Encrypt(string.Empty, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the re‑encrypted PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Re‑encrypted PDF saved to: {outputPdfPath}");
    }

    // Helper retained for possible future use – currently returns a fixed PNG extension
    private static string GetImageExtension(XImage img)
    {
        // Aspose.Pdf.XImage does not expose an ImageFormat property.
        // For simplicity we always use PNG as the output format.
        return ".png";
    }
}
