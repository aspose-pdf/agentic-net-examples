using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Check if the document contains any embedded files (attachments)
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            Console.WriteLine($"Found {doc.EmbeddedFiles.Count} attachment(s):");

            // Iterate over each embedded file without referencing the concrete EmbeddedFile type
            foreach (object obj in doc.EmbeddedFiles)
            {
                // Use dynamic to access members at runtime (Name, Data)
                dynamic attachment = obj;

                // Ensure the data stream is positioned at the beginning
                if (attachment.Data != null)
                {
                    attachment.Data.Position = 0;
                }

                // Compute SHA‑256 hash of the attachment's data
                using (SHA256 sha256 = SHA256.Create())
                {
                    // If Data is null, treat it as empty content
                    byte[] hashBytes = attachment.Data != null
                        ? sha256.ComputeHash(attachment.Data)
                        : sha256.ComputeHash(Array.Empty<byte>());

                    string hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

                    Console.WriteLine($"- Name: {attachment.Name}");
                    Console.WriteLine($"  SHA‑256: {hashHex}");
                }

                // Reset the stream position in case further processing is needed
                if (attachment.Data != null)
                {
                    attachment.Data.Position = 0;
                }
            }
        }
    }
}
