using System;
using System.IO;
using Aspose.Pdf;                 // Document, Permissions, CryptoAlgorithm

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string folderPath = @"C:\PdfFolder";

        // Standardized passwords
        const string newUserPassword = "StandardUserPassword";
        const string ownerPassword   = "StandardOwnerPassword";

        // Permissions to retain after re‑encryption
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // Temporary file to hold the re‑encrypted PDF
                string tempFile = Path.Combine(
                    Path.GetDirectoryName(pdfFile)!,
                    Path.GetFileNameWithoutExtension(pdfFile) + "_tmp.pdf");

                // Load the PDF with Document (throws PdfException if the file is encrypted with an unknown password)
                Document doc = new Document(pdfFile);

                // Re‑encrypt the PDF with the standardized user password using Document.Encrypt
                doc.Encrypt(newUserPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the re‑encrypted PDF to a temporary location
                doc.Save(tempFile);

                // Replace the original file with the newly encrypted version
                File.Delete(pdfFile);
                File.Move(tempFile, pdfFile);

                Console.WriteLine($"Re‑encrypted: {Path.GetFileName(pdfFile)}");
            }
            catch (PdfException)
            {
                // The PDF is already encrypted with an unknown password – skip or handle as needed
                Console.Error.WriteLine($"Skipped (encrypted with unknown password): {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfFile)}': {ex.Message}");
            }
        }

        Console.WriteLine("Password update completed.");
    }
}
