using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths and passwords – adjust as needed
        const string inputPdfPath      = "encrypted_input.pdf";
        const string outputPdfPath     = "reencrypted_output.pdf";
        const string originalPassword  = "oldOwnerPass";   // password to open the encrypted PDF
        const string newOwnerPassword  = "newOwnerPass";   // password to set after re‑encryption

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the existing password
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath, originalPassword))
            {
                // Decrypt the document (removes encryption in memory)
                doc.Decrypt();

                // -----------------------------------------------------------------
                // Extract all images from the PDF and save them as separate files
                // -----------------------------------------------------------------
                int imageIndex = 0;
                foreach (Aspose.Pdf.Page page in doc.Pages)
                {
                    // The Images collection yields XImage objects directly (no dictionary)
                    foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                    {
                        // Build a unique filename for each extracted image
                        string imageFileName = $"image_{imageIndex}.png";

                        // Save the image to disk
                        using (FileStream imgStream = new FileStream(imageFileName, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(imgStream);
                        }

                        Console.WriteLine($"Extracted image saved to: {imageFileName}");
                        imageIndex++;
                    }
                }

                // -----------------------------------------------------------------
                // Re‑encrypt the PDF with a new owner password
                // -----------------------------------------------------------------
                // Define desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt with an empty user password and the new owner password
                doc.Encrypt(userPassword: "", ownerPassword: newOwnerPassword, permissions: perms, cryptoAlgorithm: CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPdfPath);
                Console.WriteLine($"Re‑encrypted PDF saved to: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}