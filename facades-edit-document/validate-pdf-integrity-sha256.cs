using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF and add a simple text annotation on page 1 using the high‑level Document API
        Document pdf = new Document(originalPath);
        TextFragment tf = new TextFragment("Edited")
        {
            Position = new Position(100, 700) // lower‑left corner of the text
        };
        // optional styling
        tf.TextState.FontSize = 12;
        pdf.Pages[1].Paragraphs.Add(tf);
        pdf.Save(editedPath);

        // Compute SHA‑256 checksum of a file
        string ComputeSha256(string path)
        {
            using FileStream stream = File.OpenRead(path);
            using SHA256 sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        string originalHash = ComputeSha256(originalPath);
        string editedHash   = ComputeSha256(editedPath);

        Console.WriteLine($"Original SHA‑256: {originalHash}");
        Console.WriteLine($"Edited   SHA‑256: {editedHash}");

        if (originalHash.Equals(editedHash, StringComparison.OrdinalIgnoreCase))
            Console.WriteLine("Checksums match: the PDF integrity is unchanged.");
        else
            Console.WriteLine("Checksums differ: the PDF has been modified.");
    }
}
