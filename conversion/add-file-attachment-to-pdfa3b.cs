using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string attachmentPath = "notes.txt";
        const string outputPdf = "output_pdfa3b.pdf";
        const string logPath = "conversion.log";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(sourcePdf))
        {
            // Create a FileSpecification for the external text file using the file‑path constructor.
            // This avoids keeping a FileStream open during the conversion/saving process.
            FileSpecification fileSpec = new FileSpecification(attachmentPath);

            // Add the file specification to the document's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Create a file attachment annotation on the first page
            Page page = doc.Pages[1];
            // Use the Aspose.Pdf.Rectangle type (not the drawing rectangle) for the annotation bounds
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            FileAttachmentAnnotation attachmentAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                Contents = "Attached notes.txt"
            };
            page.Annotations.Add(attachmentAnnot);

            // Convert the document to PDF/A‑3b compliance
            doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

            // Save the PDF/A‑3b document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑3b file saved to '{outputPdf}'.");
    }
}
