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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Check if the document contains any embedded files (attachments)
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            Console.WriteLine($"Found {doc.EmbeddedFiles.Count} attachment(s):");

            // Iterate over each embedded file using reflection (avoids direct dependency on EmbeddedFile type)
            foreach (var attachment in doc.EmbeddedFiles)
            {
                // Retrieve the attachment name via reflection
                var nameProp = attachment.GetType().GetProperty("Name");
                string name = nameProp?.GetValue(attachment) as string ?? "<unknown>";

                // Obtain a stream for the embedded file content via reflection
                var getStreamMethod = attachment.GetType().GetMethod("GetFileStream");
                if (getStreamMethod == null)
                {
                    Console.WriteLine($"- Name: {name}\n  Unable to read file stream (method not found).");
                    continue;
                }

                using (Stream fileStream = (Stream)getStreamMethod.Invoke(attachment, null))
                {
                    // Compute SHA-256 hash
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] hashBytes = sha256.ComputeHash(fileStream);
                        string hashHex = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLowerInvariant();

                        Console.WriteLine($"- Name: {name}");
                        Console.WriteLine($"  SHA-256: {hashHex}");
                    }
                }
            }
        }
    }
}
