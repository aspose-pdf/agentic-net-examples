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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Check if the document contains any embedded files (attachments)
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            Console.WriteLine($"Found {doc.EmbeddedFiles.Count} attachment(s):");

            // Iterate over each embedded file
            foreach (FileSpecification attachment in doc.EmbeddedFiles)
            {
                // Retrieve the attachment name
                string name = attachment.Name;

                // Copy the embedded file content into a memory stream to compute its hash
                using (var ms = new MemoryStream())
                {
                    // Ensure the source stream is positioned at the beginning
                    if (attachment.Contents.CanSeek)
                        attachment.Contents.Position = 0;

                    // Copy the contents to the memory stream
                    attachment.Contents.CopyTo(ms);
                    ms.Position = 0; // Reset stream position before hashing

                    // Compute SHA‑256 hash of the attachment content
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] hashBytes = sha256.ComputeHash(ms);
                        string hashHex = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLowerInvariant();

                        // Output the result
                        Console.WriteLine($"Attachment: {name}");
                        Console.WriteLine($"SHA‑256: {hashHex}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
