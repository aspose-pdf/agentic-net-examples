using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, the edited PDF and a temporary blank page PDF
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";
        const string blankPagePath = "blank_page.pdf";

        // Ensure the original file exists
        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"Original file not found: {originalPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Create a one‑page blank PDF to be inserted (modification step)
        // -----------------------------------------------------------------
        using (Document blankDoc = new Document())
        {
            // Add an empty page
            blankDoc.Pages.Add();
            // Save the temporary blank page PDF
            blankDoc.Save(blankPagePath);
        }

        // -----------------------------------------------------------------
        // Modify the original PDF by inserting the blank page at the beginning
        // -----------------------------------------------------------------
        using (Document target = new Document(originalPath))
        using (Document source = new Document(blankPagePath))
        {
            // Insert all pages from the source document at position 1 (the beginning)
            target.Pages.Insert(1, source.Pages);
            target.Save(editedPath);
        }

        // -----------------------------------------------------------------
        // Compute SHA‑256 checksums for both files
        // -----------------------------------------------------------------
        string originalHash = ComputeSha256(originalPath);
        string editedHash   = ComputeSha256(editedPath);

        // -----------------------------------------------------------------
        // Compare and report the result
        // -----------------------------------------------------------------
        Console.WriteLine($"Original SHA‑256: {originalHash}");
        Console.WriteLine($"Edited   SHA‑256: {editedHash}");

        if (string.Equals(originalHash, editedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("The files are identical (integrity unchanged).");
        }
        else
        {
            Console.WriteLine("The files differ (integrity altered after modification).");
        }

        // Clean up the temporary blank page file
        try { File.Delete(blankPagePath); } catch { /* ignore cleanup errors */ }
    }

    // Helper method to compute SHA‑256 hash of a file and return it as a hex string
    static string ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(stream);
            StringBuilder sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}