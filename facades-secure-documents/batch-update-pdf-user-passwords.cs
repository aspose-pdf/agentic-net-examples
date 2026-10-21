using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as requested

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string folderPath = @"C:\PdfFolder";
        // Standardized user password to apply to every PDF
        const string newUserPassword = "StandardPassword123";
        // Owner password (can be any value; keep it constant here)
        const string ownerPassword = "OwnerPassword";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Enumerate all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Define permissions you want to keep (example: allow printing and content extraction)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                    // Encrypt the document with the new user password, a fixed owner password,
                    // desired permissions, and the strongest algorithm (AES‑256)
                    doc.Encrypt(newUserPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Overwrite the original file with the newly encrypted version
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (InvalidPasswordException)
            {
                // The source PDF is already encrypted with an unknown password
                Console.Error.WriteLine($"Skipping (cannot open): {Path.GetFileName(pdfPath)} – unknown password.");
            }
            catch (Exception ex)
            {
                // General error handling
                Console.Error.WriteLine($"Error processing {Path.GetFileName(pdfPath)}: {ex.Message}");
            }
        }

        Console.WriteLine("Password update completed.");
    }
}