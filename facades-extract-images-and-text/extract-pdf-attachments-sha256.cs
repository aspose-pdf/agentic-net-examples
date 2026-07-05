using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // source PDF containing attachments
        const string outputDir = "ExtractedAttachments";  // folder to store extracted files

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Make sure the source PDF exists. If it does not, create an
        // empty PDF so the example can run without throwing a
        // FileNotFoundException. This also demonstrates a pattern that
        // can be reused in other samples where an input file might be
        // missing.
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File '{pdfPath}' not found. Creating an empty PDF for demonstration purposes.");
            using (Document emptyDoc = new Document())
            {
                // No pages or attachments are added – the document is
                // intentionally empty.
                emptyDoc.Save(pdfPath);
            }
        }

        try
        {
            // ---------- Extract attachments ----------
            // Create the extractor, bind the PDF and extract all attachments
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractAttachment();

                // Retrieve attachment names and their corresponding streams
                IList<string> attachmentNames = extractor.GetAttachNames();          // list of string names
                MemoryStream[] attachmentStreams = extractor.GetAttachment(); // parallel array of streams

                // Process each attachment
                for (int i = 0; i < attachmentStreams.Length; i++)
                {
                    string? name = attachmentNames[i];
                    if (string.IsNullOrEmpty(name))
                    {
                        // Skip entries with no name to avoid Path.Combine null warnings
                        attachmentStreams[i].Dispose();
                        continue;
                    }

                    string outPath = Path.Combine(outputDir, name);

                    // Save the attachment to disk
                    using (FileStream fileStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        // Reset stream position just in case
                        attachmentStreams[i].Position = 0;
                        attachmentStreams[i].CopyTo(fileStream);
                    }

                    // Compute SHA‑256 hash of the saved file
                    using (FileStream fileStream = new FileStream(outPath, FileMode.Open, FileAccess.Read))
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] hashBytes = sha256.ComputeHash(fileStream);
                        string hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                        Console.WriteLine($"Attachment: {name}  SHA‑256: {hashHex}");
                    }

                    // Dispose the memory stream for this attachment
                    attachmentStreams[i].Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while extracting attachments: {ex.Message}");
        }
    }
}
