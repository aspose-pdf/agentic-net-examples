using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the collection of embedded files (attachments) as FileSpecification objects
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                string name = fileSpec.Name;

                // Compute SHA‑256 checksum directly from the embedded file's content stream
                string checksum;
                using (Stream content = fileSpec.Contents)
                using (SHA256 sha = SHA256.Create())
                {
                    // Ensure the stream is positioned at the beginning
                    if (content.CanSeek)
                        content.Position = 0;
                    byte[] hash = sha.ComputeHash(content);
                    checksum = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }

                // Store the checksum in custom metadata using the DocumentInfo indexer
                string propName = $"Attachment_{name}_Checksum";
                doc.Info[propName] = checksum; // overwrites any existing entry with the same key
            }

            // Save the updated PDF with the new metadata
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Checksums added to metadata and saved as '{outputPdf}'.");
    }
}
