using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath      = "protected.pdf";
        const string decryptedPath  = "decrypted.pdf";
        const string outputPath     = "protected_out.pdf";
        const string ownerPassword  = "owner123";
        const string userPassword   = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -------------------------------------------------
        // Decrypt the protected PDF using PdfFileSecurity
        // -------------------------------------------------
        using (PdfFileSecurity decryptor = new PdfFileSecurity(inputPath, decryptedPath))
        {
            bool decrypted = decryptor.DecryptFile(ownerPassword);
            if (!decrypted)
            {
                Console.Error.WriteLine("Decryption failed.");
                return;
            }
        }

        // -------------------------------------------------
        // Load the decrypted PDF, edit its content, and save
        // -------------------------------------------------
        using (Document doc = new Document(decryptedPath))
        {
            // Example edit: add a text annotation on the first page
            Page page = doc.Pages[1];

            // Fully qualified rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Edit Note",
                Contents = "Document edited after decryption.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };

            page.Annotations.Add(annotation);

            // Overwrite the decrypted file with the edited version
            doc.Save(decryptedPath);
        }

        // -------------------------------------------------
        // Re‑encrypt the edited PDF using PdfFileSecurity
        // -------------------------------------------------
        using (PdfFileSecurity encryptor = new PdfFileSecurity(decryptedPath, outputPath))
        {
            // Set desired privileges (e.g., allow printing) and 256‑bit AES encryption
            bool encrypted = encryptor.EncryptFile(userPassword, ownerPassword,
                                                   DocumentPrivilege.Print, KeySize.x256);
            if (!encrypted)
            {
                Console.Error.WriteLine("Encryption failed.");
                return;
            }
        }

        // Clean up the intermediate decrypted file (optional)
        try { File.Delete(decryptedPath); } catch { /* ignore */ }

        Console.WriteLine($"Successfully edited and re‑encrypted PDF saved to '{outputPath}'.");
    }
}