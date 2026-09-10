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
        const string inputPdfPath = "input_with_attachments.pdf";
        const string outputDir = "ExtractedAttachments";

        // ------------------------------------------------------------
        // 1️⃣ Create a self‑contained PDF that already contains an
        //    attachment. This guarantees the file exists in the sandbox.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            // a) Create a temporary file that will be embedded.
            const string tempAttachmentName = "sample.txt";
            File.WriteAllText(tempAttachmentName, "This is a sample attachment.");

            // b) Build a minimal PDF document.
            using (Document doc = new Document())
            {
                // Add a single blank page – the document must have at least one page.
                doc.Pages.Add();

                // c) Embed the temporary file.
                using (FileStream fs = File.OpenRead(tempAttachmentName))
                {
                    var fileSpec = new FileSpecification(fs, tempAttachmentName, "Sample attachment")
                    {
                        MIMEType = "text/plain",
                        AFRelationship = AFRelationship.Data
                    };
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // d) Save the PDF that will be used as input for extraction.
                doc.Save(inputPdfPath);
            }

            // Clean up the temporary attachment file – it is now stored inside the PDF.
            File.Delete(tempAttachmentName);
        }

        // ------------------------------------------------------------
        // 2️⃣ Ensure the output folder exists.
        // ------------------------------------------------------------
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // 3️⃣ Extract all attachments and compute a SHA‑256 hash for each.
        // ------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractAttachment();
            extractor.GetAttachment(outputDir);

            IList<string> attachmentNames = extractor.GetAttachNames();

            foreach (string name in attachmentNames)
            {
                if (string.IsNullOrEmpty(name))
                    continue;

                string filePath = Path.Combine(outputDir, name);
                using (FileStream fs = File.OpenRead(filePath))
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(fs);
                    string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                    Console.WriteLine($"{name}: {hashString}");
                }
            }
        }
    }
}
