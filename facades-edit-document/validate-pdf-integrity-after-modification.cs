using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf.Facades;

class Program
{
    // Compute SHA‑256 hash of a file and return it as a hex string
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
            Console.Error.WriteLine($"File not found: {originalPath}");
            return;
        }

        // Use Aspose.Pdf.Facades to modify the PDF.
        // Here we insert the first page of the original PDF after page 1,
        // creating a new file (editedPath). This demonstrates a modification
        // performed via a Facades class.
        PdfFileEditor editor = new PdfFileEditor();
        // Insert page 1 of original.pdf after page 1 of original.pdf,
        // saving the result as edited.pdf.
        editor.Insert(originalPath, 1, originalPath, new int[] { 1 }, editedPath);

        // Compute SHA‑256 checksums of both files
        string originalHash = ComputeSha256(originalPath);
        string editedHash   = ComputeSha256(editedPath);

        // Compare and report the result
        Console.WriteLine($"Original SHA‑256: {originalHash}");
        Console.WriteLine($"Edited   SHA‑256: {editedHash}");

        if (originalHash.Equals(editedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Integrity check passed: files are identical.");
        }
        else
        {
            Console.WriteLine("Integrity check failed: files differ after modification.");
        }
    }
}