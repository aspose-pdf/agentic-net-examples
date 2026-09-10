using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"File not found: {originalPath}");
            return;
        }

        // Load original PDF, make a simple modification, and save as edited PDF
        using (Document doc = new Document(originalPath))
        {
            // Example modification: add a blank page at the end
            doc.Pages.Add();
            doc.Save(editedPath); // PDF save (no SaveOptions needed)
        }

        // Validate that both PDFs are well‑formed using Aspose.Pdf.Facades
        ValidatePdfWithFacade(originalPath);
        ValidatePdfWithFacade(editedPath);

        // Compute SHA‑256 checksums
        string originalHash = ComputeSha256(originalPath);
        string editedHash   = ComputeSha256(editedPath);

        // Compare and report
        if (originalHash.Equals(editedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Integrity check passed: checksums match.");
        }
        else
        {
            Console.WriteLine("Integrity check failed: checksums differ.");
            Console.WriteLine($"Original SHA‑256: {originalHash}");
            Console.WriteLine($"Edited   SHA‑256: {editedHash}");
        }
    }

    // Uses PdfFileSignature facade to ensure the PDF can be opened without error
    static void ValidatePdfWithFacade(string pdfPath)
    {
        using (PdfFileSignature signatureFacade = new PdfFileSignature())
        {
            // BindPdf throws if the file is not a valid PDF
            signatureFacade.BindPdf(pdfPath);
        }
    }

    // Computes SHA‑256 hash of a file and returns it as a hex string
    static string ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}