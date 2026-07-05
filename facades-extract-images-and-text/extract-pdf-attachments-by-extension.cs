using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // Path to source PDF
        const string outputRoot = "Attachments";      // Root folder for extracted files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Use PdfExtractor (Facade) to work with attachments
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Extract all attachments from the document
            extractor.ExtractAttachment();

            // Retrieve attachment names and their corresponding streams
            IList<string> attachNames = extractor.GetAttachNames();
            MemoryStream[] attachStreams = extractor.GetAttachment();

            // Safety check: names and streams should match
            if (attachNames.Count != attachStreams.Length)
            {
                Console.Error.WriteLine("Attachment count mismatch.");
                return;
            }

            // Process each attachment
            for (int i = 0; i < attachNames.Count; i++)
            {
                string name = attachNames[i];
                string ext = Path.GetExtension(name);
                // Determine subfolder based on file extension (use a fallback for no extension)
                string extFolder = string.IsNullOrEmpty(ext) ? "no_extension" : ext.TrimStart('.').ToLowerInvariant();
                string targetDir = Path.Combine(outputRoot, extFolder);
                Directory.CreateDirectory(targetDir);

                string targetPath = Path.Combine(targetDir, name);

                // Write the attachment stream to the appropriate file
                using (MemoryStream ms = attachStreams[i])
                using (FileStream fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
                {
                    ms.Position = 0;               // Ensure stream is at the beginning
                    ms.CopyTo(fs);                 // Copy bytes to file
                }

                Console.WriteLine($"Extracted: {targetPath}");
            }
        }
    }
}