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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Check if the document contains any embedded files (attachments)
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            Console.WriteLine($"Found {doc.EmbeddedFiles.Count} attachment(s):");

            // Iterate over each embedded file using reflection to avoid direct dependency on the EmbeddedFile type
            foreach (var attachment in doc.EmbeddedFiles)
            {
                // Retrieve the attachment name via reflection
                var nameProp = attachment.GetType().GetProperty("Name");
                string name = nameProp?.GetValue(attachment) as string ?? "<unknown>";

                // Obtain a stream for the attachment's data via reflection
                var getFileMethod = attachment.GetType().GetMethod("GetFile", Type.EmptyTypes);
                if (getFileMethod == null)
                {
                    Console.WriteLine($"Attachment '{name}' does not expose a GetFile method.");
                    continue;
                }

                using (Stream dataStream = (Stream)getFileMethod.Invoke(attachment, null))
                using (SHA256 sha256 = SHA256.Create())
                {
                    // Compute the SHA‑256 hash
                    byte[] hashBytes = sha256.ComputeHash(dataStream);

                    // Convert hash to a hexadecimal string
                    string hashHex = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLowerInvariant();

                    Console.WriteLine($"Attachment: {name}");
                    Console.WriteLine($"SHA‑256: {hashHex}");
                }
            }
        }
    }
}
