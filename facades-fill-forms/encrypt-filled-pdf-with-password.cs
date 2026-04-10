using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf   = "filled.pdf";      // PDF that has been filled previously
        const string encryptedPdf = "filled_protected.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Input file not found: {sourcePdf}");
            return;
        }

        // Ensure the source PDF is a valid PDF (wrap in using for deterministic disposal)
        using (Document doc = new Document(sourcePdf))
        {
            // No further modifications – just demonstrate the save step
            // Document.Save writes a PDF regardless of the extension
            doc.Save(sourcePdf);   // sourcePdf already contains the filled content
        }

        // Apply password protection using the Facades PdfFileSecurity class
        // Constructor takes input file path and output file path
        PdfFileSecurity fileSecurity = new PdfFileSecurity(sourcePdf, encryptedPdf);

        // Encrypt the file, set desired privileges (e.g., allow printing only)
        // KeySize.x256 provides strong AES‑256 encryption
        bool success = fileSecurity.EncryptFile(
            userPassword,
            ownerPassword,
            DocumentPrivilege.Print,
            KeySize.x256);

        if (!success)
        {
            Console.Error.WriteLine("Failed to encrypt the PDF.");
            return;
        }

        Console.WriteLine($"Password‑protected PDF saved to '{encryptedPdf}'.");
    }
}