using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Check for embedded files (attachments)
                if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No attachments found in the PDF.");
                }
                else
                {
                    // Iterate over each attachment using dynamic to avoid compile‑time dependency on the EmbeddedFile type
                    foreach (var attachmentObj in doc.EmbeddedFiles)
                    {
                        dynamic attachment = attachmentObj; // attachment now exposes Name, FileSize, FileData, etc.

                        // Extract the attachment data into a memory stream
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // FileData is a Stream; call its Save method (or copy directly)
                            // Some Aspose versions expose FileData as a Stream, others as a custom type with Save(Stream)
                            // Use reflection as a fallback for maximum compatibility
                            try
                            {
                                attachment.FileData.Save(ms);
                            }
                            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                            {
                                // If Save is not available, assume FileData is a Stream and copy it
                                ((Stream)attachment.FileData).CopyTo(ms);
                            }

                            byte[] data = ms.ToArray();

                            // Compute SHA‑256 hash of the attachment bytes
                            using (SHA256 sha256 = SHA256.Create())
                            {
                                byte[] hash = sha256.ComputeHash(data);
                                string hashHex = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                                // Output attachment information and its hash
                                Console.WriteLine($"Attachment: {attachment.Name}");
                                Console.WriteLine($"Size (bytes): {attachment.FileSize}");
                                Console.WriteLine($"SHA‑256: {hashHex}");
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
