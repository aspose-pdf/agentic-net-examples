using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // PDF containing attachments
        const string outputDirectory = "ExtractedAttachments"; // Folder to store renamed files
        const string tempAttachmentPath = "sample.txt";        // Temporary file to embed as attachment

        // ------------------------------------------------------------
        // 1. Create a sample PDF with an embedded attachment (self‑contained).
        // ------------------------------------------------------------
        // Ensure the temporary attachment file exists.
        File.WriteAllText(tempAttachmentPath, "This is a sample attachment created for extraction demo.");

        // Create a minimal PDF document.
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // add a blank page so the PDF is valid

            // Attach the temporary file to the PDF using the EmbeddedFiles collection.
            FileSpecification fileSpec = new FileSpecification(tempAttachmentPath, "Sample attachment");
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF that will be used as the input source.
            doc.Save(inputPdfPath);
        }

        // ------------------------------------------------------------
        // 2. Ensure the output folder exists.
        // ------------------------------------------------------------
        Directory.CreateDirectory(outputDirectory);

        // ------------------------------------------------------------
        // 3. Extract attachments, rename them with a timestamp prefix, and save.
        // ------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF (the one we just created).
            extractor.BindPdf(inputPdfPath);

            // Extract all attachments from the document.
            extractor.ExtractAttachment();

            // Retrieve attachment names (original file names).
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Retrieve attachment streams (one MemoryStream per attachment).
            MemoryStream[] attachmentStreams = extractor.GetAttachment();

            // Build a timestamp prefix (e.g., 20231130104530).
            string timestampPrefix = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Iterate over each attachment, rename with timestamp, and save to disk.
            for (int i = 0; i < attachmentStreams.Length; i++)
            {
                string originalName = attachmentNames[i];
                string newFileName = $"{timestampPrefix}_{originalName}";
                string outputPath = Path.Combine(outputDirectory, newFileName);

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    attachmentStreams[i].Position = 0; // reset stream position before copying
                    attachmentStreams[i].CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine("Attachments extracted and renamed successfully.");
    }
}
