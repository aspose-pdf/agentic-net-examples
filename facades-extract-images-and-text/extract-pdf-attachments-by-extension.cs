using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Prepare a self‑contained input PDF (the sandbox has no files).
        // ---------------------------------------------------------------------
        const string pdfPath = "input.pdf";
        const string sampleAttachmentPath = "sample.txt";

        // Create a tiny text file that will be embedded as an attachment.
        File.WriteAllText(sampleAttachmentPath, "This is a sample attachment created for the demo.");

        // Build a minimal PDF and embed the text file.
        using (var doc = new Document())
        {
            // Add a single blank page – the PDF must contain at least one page.
            doc.Pages.Add();

            // Create a FileSpecification for the attachment.
            using (FileStream attStream = File.OpenRead(sampleAttachmentPath))
            {
                var fileSpec = new FileSpecification(attStream, Path.GetFileName(sampleAttachmentPath))
                {
                    // Optional: set a description and MIME type.
                    Description = "Sample text attachment",
                    MIMEType = "text/plain",
                    // Mark the relationship as Data (generic attachment).
                    AFRelationship = AFRelationship.Data
                };
                // Add the attachment to the PDF.
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF so the later extraction code can load it.
            doc.Save(pdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Extract attachments and organise them by file extension.
        // ---------------------------------------------------------------------
        const string tempFolder = "Attachments";
        const string organizedFolder = "AttachmentsOrganized";

        Directory.CreateDirectory(tempFolder);
        Directory.CreateDirectory(organizedFolder);

        // Load the PDF that now definitely exists.
        Document pdfDoc = new Document(pdfPath);

        // Extract all embedded file attachments.
        if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
        {
            foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
            {
                // Preserve the original attachment name; fall back to a GUID if missing.
                string attachmentName = string.IsNullOrEmpty(fileSpec.Name) ? Guid.NewGuid().ToString() : fileSpec.Name;
                string destPath = Path.Combine(tempFolder, attachmentName);

                // Write the attachment stream to the temporary folder.
                using (FileStream outStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                {
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;
                    fileSpec.Contents.CopyTo(outStream);
                }
            }
        }
        else
        {
            Console.WriteLine("No embedded attachments found in the PDF.");
        }

        // Move each extracted file into a sub‑folder named after its extension.
        foreach (string filePath in Directory.GetFiles(tempFolder))
        {
            string ext = Path.GetExtension(filePath).TrimStart('.').ToLowerInvariant();
            if (string.IsNullOrEmpty(ext))
                ext = "no_ext";

            string destDir = Path.Combine(organizedFolder, ext);
            Directory.CreateDirectory(destDir);

            string destPath = Path.Combine(destDir, Path.GetFileName(filePath));
            // Overwrite if a file with the same name already exists.
            File.Move(filePath, destPath, true);
        }

        // Clean up the now‑empty temporary folder.
        if (Directory.GetFiles(tempFolder).Length == 0 && Directory.GetDirectories(tempFolder).Length == 0)
            Directory.Delete(tempFolder, true);

        Console.WriteLine("Attachments have been extracted and organized.");
    }
}
