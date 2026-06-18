using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF (can be any PDF)
        const string attachmentPath = "notes.txt";   // external text file to attach
        const string outputPath = "output_pdfa3b.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileSpecification for the external file and add it to the embedded files collection
            FileSpecification fileSpec = new FileSpecification(attachmentPath);
            doc.EmbeddedFiles.Add(fileSpec);

            // Optionally add a visible file‑attachment annotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
            FileAttachmentAnnotation attachmentAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Icon property can be omitted or set to a valid enum value if needed
                Contents = "Attached notes.txt"
            };
            page.Annotations.Add(attachmentAnnot);

            // Convert the document to PDF/A‑3b compliance
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the compliant document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF/A‑3b with attachment to '{outputPath}'.");
    }
}
