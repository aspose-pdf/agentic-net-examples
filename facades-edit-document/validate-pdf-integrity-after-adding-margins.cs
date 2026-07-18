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

        // Compute SHA‑256 of the original file
        string originalHash = ComputeSha256(originalPath);
        Console.WriteLine($"Original SHA‑256: {originalHash}");

        // Modify the PDF using a Facade class (PdfFileEditor)
        // Here we add a uniform margin to all pages as a simple modification
        PdfFileEditor editor = new PdfFileEditor();
        // Parameters: input file, output file, pages (null = all), margins (left, right, top, bottom)
        editor.AddMargins(originalPath, editedPath, null, 10, 10, 10, 10);

        // Compute SHA‑256 of the edited file
        string editedHash = ComputeSha256(editedPath);
        Console.WriteLine($"Edited SHA‑256: {editedHash}");

        // Compare hashes to validate integrity after modification
        if (originalHash.Equals(editedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Integrity check FAILED: hashes are identical (no modification detected).");
        }
        else
        {
            Console.WriteLine("Integrity check PASSED: hashes differ, modification applied.");
        }
    }

    // Helper method to compute SHA‑256 checksum of a file
    private static string ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}