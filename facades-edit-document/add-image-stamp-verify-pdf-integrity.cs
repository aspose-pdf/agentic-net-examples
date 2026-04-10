using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string modifiedPath = "modified.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"Original file not found: {originalPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the original PDF, apply an image stamp to every page, and save as a new file.
        Document pdfDoc = new Document(originalPath);

        ImageStamp imgStamp = new ImageStamp(stampImagePath)
        {
            Background = false,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Opacity = 0.5f
        };

        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(imgStamp);
        }

        pdfDoc.Save(modifiedPath);

        // Compute SHA‑256 checksums for both files.
        string originalHash = ComputeSha256(originalPath);
        string modifiedHash = ComputeSha256(modifiedPath);

        // Compare and report the result.
        Console.WriteLine($"Original SHA‑256: {originalHash}");
        Console.WriteLine($"Modified SHA‑256: {modifiedHash}");

        if (originalHash.Equals(modifiedHash, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Integrity check: PASS (files are identical).");
        }
        else
        {
            Console.WriteLine("Integrity check: FAIL (files differ).");
        }
    }

    // Helper method to compute SHA‑256 hash of a file and return it as a hex string.
    private static string ComputeSha256(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(stream);
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
