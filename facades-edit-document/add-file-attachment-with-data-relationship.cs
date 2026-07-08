using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for source PDF, attachment file and the resulting PDF
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "attachment.pdf";
        const string outputPdfPath = "output.pdf";

        // Description for the attachment
        const string attachmentDescription = "Data attachment for better organization";

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a PdfContentEditor facade to modify the document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(pdfDoc);

                // Add the attachment without a visible annotation (embedded file)
                editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

                // ------------------------------------------------------------
                // Create a visible file‑attachment annotation and set its AFRelationship to "Data"
                // ------------------------------------------------------------
                // 1. Build a FileSpecification with the desired relationship type
                var fileSpec = new FileSpecification(attachmentPath, attachmentDescription);
                fileSpec.AFRelationship = AFRelationship.Data; // use enum, not string

                // 2. Define a small rectangle for the annotation icon on page 1 (using Aspose.Pdf.Rectangle)
                var iconRect = new Aspose.Pdf.Rectangle(0, 0, 20, 20);

                // 3. Create the annotation using the page, rectangle and the prepared FileSpecification
                var fileAnnot = new FileAttachmentAnnotation(pdfDoc.Pages[1], iconRect, fileSpec);

                // 4. Add the annotation to the first page
                pdfDoc.Pages[1].Annotations.Add(fileAnnot);

                // Save the modified PDF to the output path
                editor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with attachment and relationship type set: {outputPdfPath}");
    }
}
