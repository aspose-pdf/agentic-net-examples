using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Check if the document contains any embedded files (attachments)
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            Console.WriteLine($"Found {doc.EmbeddedFiles.Count} embedded file(s):");

            // Iterate over each embedded file
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Resolve a display name for the file
                string name = !string.IsNullOrEmpty(fileSpec.Name) ? fileSpec.Name : "EmbeddedFile";

                // Read the file contents into a byte array
                byte[] content;
                using (MemoryStream ms = new MemoryStream())
                {
                    // Ensure the stream is positioned at the beginning
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;
                    fileSpec.Contents.CopyTo(ms);
                    content = ms.ToArray();
                }

                // Compute SHA‑256 hash
                string hashHex;
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(content);
                    hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }

                Console.WriteLine($"- {name}");
                Console.WriteLine($"  Size: {content.Length} bytes");
                Console.WriteLine($"  SHA‑256: {hashHex}");
            }
        }
    }
}
