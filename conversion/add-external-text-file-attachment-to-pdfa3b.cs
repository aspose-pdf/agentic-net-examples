using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputPdfPath = "output_pdfa3b.pdf"; // PDF/A‑3b result
        const string attachmentPath = "notes.txt";        // external text file to embed
        const string conversionLogPath = "conversion_log.xml"; // log for PDF/A conversion

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF, embed the external file, convert to PDF/A‑3b, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Embed the external text file using the EmbeddedFiles collection
            using (FileStream attachmentStream = File.OpenRead(attachmentPath))
            {
                // The constructor expects the stream first, then the file name.
                var fileSpec = new FileSpecification(attachmentStream, Path.GetFileName(attachmentPath));
                // Optionally set MIME type or description if needed
                // fileSpec.MIMEType = "text/plain";
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3b compliance
            doc.Convert(conversionLogPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the PDF/A‑3b document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑3b file with attachment saved to '{outputPdfPath}'.");
    }
}
