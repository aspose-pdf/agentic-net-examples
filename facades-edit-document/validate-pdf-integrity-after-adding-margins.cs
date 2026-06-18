using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        // Compute SHA‑256 checksum of the original file
        string originalHash = ComputeSha256(originalPath);
        Console.WriteLine($"Original SHA‑256: {originalHash}");

        // Modify the PDF using a Facade class (add uniform margins to all pages)
        int[] allPages;
        using (Document doc = new Document(originalPath))
        {
            allPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
        }

        PdfFileEditor editor = new PdfFileEditor();
        // Add 10 units margin on each side of every page
        editor.AddMargins(originalPath, editedPath, allPages, 10, 10, 10, 10);

        // Compute SHA‑256 checksum of the edited file
        string editedHash = ComputeSha256(editedPath);
        Console.WriteLine($"Edited SHA‑256:   {editedHash}");

        // Compare the two checksums
        if (originalHash.Equals(editedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Integrity check passed: files are identical.");
        }
        else
        {
            Console.WriteLine("Integrity check failed: files differ.");
        }
    }

    // Helper method to compute SHA‑256 hash of a file and return it as a hex string
    private static string ComputeSha256(string filePath)
    {
        using (var sha256 = SHA256.Create())
        using (var stream = File.OpenRead(filePath))
        {
            byte[] hash = sha256.ComputeHash(stream);
            StringBuilder sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}