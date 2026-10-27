using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_checksum.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare a dictionary to hold checksums for each embedded file.
            var checksums = new Dictionary<string, string>();

            // Iterate over all embedded files (FileSpecification objects).
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // The embedded file name is used as the dictionary key.
                    string name = fileSpec.Name;

                    // Ensure the file has content.
                    if (fileSpec.Contents == null)
                        continue;

                    // Copy the content to a memory stream for hashing.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Reset the source stream position before copying.
                        if (fileSpec.Contents.CanSeek)
                            fileSpec.Contents.Position = 0;

                        fileSpec.Contents.CopyTo(ms);
                        ms.Position = 0; // Reset for hashing.

                        // Compute SHA‑256 checksum.
                        using (SHA256 sha256 = SHA256.Create())
                        {
                            byte[] hash = sha256.ComputeHash(ms);
                            string checksum = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                            checksums[name] = checksum;
                        }
                    }
                }
            }

            // Store each attachment checksum in the PDF's custom metadata using the DocumentInfo indexer.
            foreach (var kvp in checksums)
            {
                string metaKey = $"AttachmentChecksum_{kvp.Key}";
                doc.Info[metaKey] = kvp.Value; // Indexer adds/updates custom metadata.
            }

            // Save the updated PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Checksums added to custom metadata and saved as '{outputPdfPath}'.");
    }
}
