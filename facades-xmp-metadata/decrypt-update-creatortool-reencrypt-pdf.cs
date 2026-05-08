using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";
        const string decryptedPath  = "temp_decrypted.pdf";
        const string outputPath     = "re_encrypted_output.pdf";

        const string ownerPassword  = "ownerPass";
        const string userPassword   = "userPass";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // ---------- Decrypt the protected PDF ----------
            // PdfFileSecurity works on file level: input file -> output file
            PdfFileSecurity decryptor = new PdfFileSecurity(inputPath, decryptedPath);
            decryptor.DecryptFile(ownerPassword);   // throws if password is wrong

            // ---------- Update CreatorTool metadata ----------
            // Load the now‑decrypted PDF, modify its Info, and save back
            using (Document doc = new Document(decryptedPath))
            {
                // Update the CreatorTool (or Creator if CreatorTool is unavailable)
                // Document.Info is of type DocumentInfo; it has a Creator property.
                // If a specific CreatorTool property exists, use it; otherwise set Creator.
                doc.Info.Creator = "MyApplication v1.0";

                // Overwrite the same file (decryptedPath) with the updated metadata
                doc.Save(decryptedPath);
            }

            // ---------- Re‑encrypt the PDF ----------
            // Choose desired privileges; here we allow printing.
            PdfFileSecurity encryptor = new PdfFileSecurity(decryptedPath, outputPath);
            encryptor.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);

            Console.WriteLine($"Successfully decrypted, updated, and re‑encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary decrypted file
            try
            {
                if (File.Exists(decryptedPath))
                    File.Delete(decryptedPath);
            }
            catch { /* ignore cleanup errors */ }
        }
    }
}