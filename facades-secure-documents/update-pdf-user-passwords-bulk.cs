using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Standardized user password to apply to every PDF
    private const string StandardUserPassword = "StandardPassword123";

    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Create a temporary file for the output to avoid locking the source file
            string tempOutputPath = Path.Combine(Path.GetDirectoryName(pdfPath),
                                                 Path.GetFileNameWithoutExtension(pdfPath) + "_tmp.pdf");

            try
            {
                // Initialize PdfFileSecurity with source and destination files
                using (PdfFileSecurity fileSecurity = new PdfFileSecurity(pdfPath, tempOutputPath))
                {
                    // Encrypt the PDF with the standardized user password.
                    // Owner password is set to null so Aspose generates a random one.
                    // DocumentPrivilege.Print is used as a generic privilege; adjust as needed.
                    // KeySize.x256 provides strong AES-256 encryption.
                    fileSecurity.EncryptFile(
                        StandardUserPassword,   // user password
                        null,                   // owner password (null => random)
                        DocumentPrivilege.Print,
                        KeySize.x256);
                }

                // Replace the original file with the newly encrypted file
                File.Delete(pdfPath);
                File.Move(tempOutputPath, pdfPath);

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Clean up temporary file if something went wrong
                if (File.Exists(tempOutputPath))
                {
                    try { File.Delete(tempOutputPath); } catch { /* ignore */ }
                }

                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Password update completed.");
    }
}