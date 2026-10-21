using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // original PDF
        const string outputPdf = "output.pdf"; // final PDF with new attachments

        // Files to be attached to the PDF
        string[] newAttachments = { "file1.txt", "image.png", "data.xml" };

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify that each attachment file exists
        foreach (string att in newAttachments)
        {
            if (!File.Exists(att))
            {
                Console.Error.WriteLine($"Attachment file not found: {att}");
                return;
            }
        }

        // Load the PDF document using Aspose.Pdf.Document
        Document pdfDoc = new Document(inputPdf);

        // -----------------------------------------------------------------
        // 1. Remove all existing embedded files (if any).
        // -----------------------------------------------------------------
        if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
        {
            // EmbeddedFileCollection uses 1‑based indexing. Delete by name starting from the end.
            for (int i = pdfDoc.EmbeddedFiles.Count; i >= 1; i--)
            {
                var existingSpec = pdfDoc.EmbeddedFiles[i];
                if (existingSpec != null && !string.IsNullOrEmpty(existingSpec.Name))
                {
                    pdfDoc.EmbeddedFiles.Delete(existingSpec.Name);
                }
            }
        }

        // -----------------------------------------------------------------
        // 2. Add the fresh set of attachments as embedded files.
        // -----------------------------------------------------------------
        foreach (string attPath in newAttachments)
        {
            // Create a FileSpecification for the file (name only constructor).
            var fileSpec = new FileSpecification(Path.GetFileName(attPath));
            // Load the file content into a memory stream.
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attPath));
            // Optionally set MIME type (helps some PDF viewers).
            // fileSpec.MIMEType = MimeMapping.GetMimeMapping(attPath);
            // Add to the EmbeddedFiles collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec);
        }

        // Save the updated PDF
        if (File.Exists(outputPdf))
            File.Delete(outputPdf);
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"All attachments refreshed. Output saved to '{outputPdf}'.");
    }
}
