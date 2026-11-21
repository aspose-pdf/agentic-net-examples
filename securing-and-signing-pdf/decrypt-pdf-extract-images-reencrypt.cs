using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted_input.pdf";
        const string outputPath = "re_encrypted_output.pdf";
        const string imagesDir = "ExtractedImages";

        const string userPassword = "user123";
        const string newOwnerPassword = "newowner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(imagesDir);

        try
        {
            // Open the encrypted PDF using the user password
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath, userPassword))
            {
                // Decrypt the document (no parameters required)
                doc.Decrypt();

                // Extract all images from the PDF
                int imgIndex = 0;
                foreach (Aspose.Pdf.Page page in doc.Pages)
                {
                    foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                    {
                        string imgPath = Path.Combine(imagesDir, $"page{page.Number}_img{imgIndex}.png");
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }
                        imgIndex++;
                    }
                }
                Console.WriteLine($"Extracted {imgIndex} images to '{imagesDir}'.");

                // Re‑encrypt the PDF with a new owner password (keep the same user password)
                Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument | Aspose.Pdf.Permissions.ExtractContent;
                doc.Encrypt(userPassword, newOwnerPassword, perms, Aspose.Pdf.CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Re‑encrypted PDF saved to '{outputPath}'.");
        }
        catch (Aspose.Pdf.InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}