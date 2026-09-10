using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF (should be PDF/A‑3b compatible)
        const string attachmentFilePath = "note.txt";    // external text file to attach
        const string outputPdfPath = "output_pdfa3b.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // Load the PDF inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Create a FileSpecification for the external text file
            // ------------------------------------------------------------
            // Use the overload that takes a file path and a description, then assign the file contents via a stream.
            var fileSpec = new FileSpecification(attachmentFilePath, "Attached Text File");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));

            // ------------------------------------------------------------
            // 2. Add the file to the document's EmbeddedFiles collection.
            //    This step is required for PDF/A‑3 compliance.
            // ------------------------------------------------------------
            doc.EmbeddedFiles.Add(fileSpec);

            // ------------------------------------------------------------
            // 3. Create a FileAttachment annotation on the first page.
            //    The annotation provides a visual cue for the attached file.
            // ------------------------------------------------------------
            var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
            var attachmentAnnot = new FileAttachmentAnnotation(
                doc.Pages[1],   // target page (1‑based indexing)
                rect,           // annotation rectangle
                fileSpec)       // linked file specification
            {
                Title = "Attached Text File",
                Contents = "This annotation embeds an external text file."
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(attachmentAnnot);

            // ------------------------------------------------------------
            // 4. Convert the document to PDF/A‑3b compliance.
            //    Use Document.Convert with the appropriate PdfFormat enum.
            // ------------------------------------------------------------
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑3b document with attachment saved to '{outputPdfPath}'.");
    }
}
