using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    // Compute SHA‑256 hash of a file and return it as a lowercase hex string
    static string ComputeSha256(string filePath)
    {
        using (SHA256 sha = SHA256.Create())
        using (FileStream stream = File.OpenRead(filePath))
        {
            byte[] hash = sha.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }

    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"Source file not found: {originalPath}");
            return;
        }

        // Use PdfFileEditor (a Facades class) to modify the PDF.
        // Here we add uniform margins of 10 units to all pages.
        PdfFileEditor editor = new PdfFileEditor();
        // pages == null => apply to all pages
        editor.AddMargins(originalPath, editedPath, null, 10, 10, 10, 10);

        // Compute checksums
        string originalHash = ComputeSha256(originalPath);
        string editedHash   = ComputeSha256(editedPath);

        // Compare and report
        Console.WriteLine($"Original SHA‑256: {originalHash}");
        Console.WriteLine($"Edited   SHA‑256: {editedHash}");

        if (originalHash.Equals(editedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Integrity check: PASS (files are identical)");
        }
        else
        {
            Console.WriteLine("Integrity check: FAIL (files differ)");
        }
    }
}