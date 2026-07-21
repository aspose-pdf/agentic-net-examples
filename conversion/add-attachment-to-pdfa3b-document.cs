using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output_pdfa3b.pdf";
        const string attachmentPath = "attachment.txt";

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        using (Document doc = new Document())
        {
            // Ensure the document has at least one page
            doc.Pages.Add();

            // Create a FileSpecification for the attachment and assign its contents via a stream
            var fileSpec = new FileSpecification(attachmentPath, "Attachment");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
            doc.EmbeddedFiles.Add(fileSpec);

            // Convert the document to PDF/A‑3b compliance using Document.Convert
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the compliant PDF/A‑3b file
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑3b document with attachment saved to '{outputPdfPath}'.");
    }
}
