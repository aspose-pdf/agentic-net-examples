using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string sourcePdfPath = "source.pdf";                 // original PDF
        const string attachmentFilePath = "attachment.txt";       // file to embed
        const string outputPdfWithAttachment = "with_attachment.pdf"; // PDF after adding attachment
        const string extractedAttachmentPath = "extracted_attachment.txt"; // where to extract

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the source PDF using Document (PdfFileEditor does not support attachments)
        // ------------------------------------------------------------
        Document pdfDoc = new Document(sourcePdfPath);

        // ------------------------------------------------------------
        // 2. Add the attachment to the PDF using EmbeddedFiles collection
        // ------------------------------------------------------------
        string attachmentName = Path.GetFileName(attachmentFilePath);
        var fileSpec = new FileSpecification(attachmentName);
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the PDF that now contains the attachment
        pdfDoc.Save(outputPdfWithAttachment);

        // ------------------------------------------------------------
        // 3. Extract the attachment back from the modified PDF
        // ------------------------------------------------------------
        Document pdfWithAttachment = new Document(outputPdfWithAttachment);
        // Retrieve the attachment by its name (case‑sensitive)
        FileSpecification extractedSpec = pdfWithAttachment.EmbeddedFiles[attachmentName];
        if (extractedSpec == null)
        {
            Console.Error.WriteLine($"Attachment '{attachmentName}' not found in the PDF.");
            return;
        }
        // Write the embedded file bytes to the extraction path
        using (FileStream outStream = File.Create(extractedAttachmentPath))
        {
            extractedSpec.Contents.CopyTo(outStream);
        }

        // ------------------------------------------------------------
        // 4. Verify that the extracted file matches the original attachment
        // ------------------------------------------------------------
        bool filesMatch = false;
        if (File.Exists(extractedAttachmentPath))
        {
            byte[] originalBytes = File.ReadAllBytes(attachmentFilePath);
            byte[] extractedBytes = File.ReadAllBytes(extractedAttachmentPath);
            if (originalBytes.Length == extractedBytes.Length)
            {
                filesMatch = true;
                for (int i = 0; i < originalBytes.Length; i++)
                {
                    if (originalBytes[i] != extractedBytes[i])
                    {
                        filesMatch = false;
                        break;
                    }
                }
            }
        }
        else
        {
            Console.Error.WriteLine("Extraction failed: extracted file not found.");
        }

        Console.WriteLine(filesMatch
            ? "Success: extracted attachment matches the original."
            : "Failure: extracted attachment does NOT match the original.");
    }
}
