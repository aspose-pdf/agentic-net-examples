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
            // Embedded files are stored in the Document.EmbeddedFiles collection
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Ensure the content stream is positioned at the start
                Stream content = fileSpec.Contents;
                if (content.CanSeek)
                    content.Position = 0;

                string hashHex;
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(content);
                    hashHex = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLowerInvariant();
                }

                Console.WriteLine($"Embedded file: {fileSpec.Name}");
                Console.WriteLine($"SHA‑256: {hashHex}");
                Console.WriteLine();
            }
        }
    }
}
