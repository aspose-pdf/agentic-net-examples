using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Paths
        // -----------------------------------------------------------------
        const string inputPdf   = "protected_input.pdf";   // encrypted source
        const string tempPdf    = "decrypted_temp.pdf";    // intermediate decrypted file
        const string outputPdf  = "protected_output.pdf";  // final encrypted file

        // -----------------------------------------------------------------
        // Passwords
        // -----------------------------------------------------------------
        const string ownerPassword = "ownerPass";   // owner password for decryption/encryption
        const string userPassword  = "userPass";    // user password for re‑encryption

        // -----------------------------------------------------------------
        // 1. Decrypt the protected PDF by loading it with the owner password
        //    and saving an un‑protected copy.
        // -----------------------------------------------------------------
        Document doc;
        try
        {
            // The Document constructor that accepts a password will open the file
            // even if it is encrypted. Supplying the owner password gives full rights.
            doc = new Document(inputPdf, ownerPassword);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to open encrypted PDF: {ex.Message}");
            return;
        }

        // Save a clear (un‑encrypted) version that we will edit.
        doc.Save(tempPdf);

        // -----------------------------------------------------------------
        // 2. Load the decrypted PDF, edit its content, and save it back.
        // -----------------------------------------------------------------
        using (Document decryptedDoc = new Document(tempPdf))
        {
            // Example edit: add a yellow text annotation on the first page
            Page page = decryptedDoc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Edit Note",
                Contents = "Document edited after decryption.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };
            page.Annotations.Add(annotation);

            // Overwrite the temporary file with the edited version
            decryptedDoc.Save(tempPdf);
        }

        // -----------------------------------------------------------------
        // 3. Re‑encrypt the edited PDF using Document.Encrypt (recommended API).
        // -----------------------------------------------------------------
        using (Document finalDoc = new Document(tempPdf))
        {
            // Define the permissions you want to allow. Here we allow printing only.
            var permissions = Aspose.Pdf.Permissions.PrintDocument;

            // Encrypt with AES‑256. The owner password stays the same; the user password
            // is what end‑users will need to open the file.
            finalDoc.Encrypt(userPassword, ownerPassword, permissions, Aspose.Pdf.CryptoAlgorithm.AESx256);

            finalDoc.Save(outputPdf);
        }

        // Optional: clean up the intermediate decrypted file
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Successfully decrypted, edited, and re‑encrypted the PDF to '{outputPdf}'.");
    }
}
