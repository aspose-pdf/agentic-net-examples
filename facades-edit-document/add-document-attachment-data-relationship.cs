using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";                     // source PDF
        const string outputPdf = "output_with_attachment.pdf";   // result PDF
        const string attachmentFile = "data_file.bin";          // file to attach
        const string description = "Data attachment for organization";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Use PdfContentEditor (Facade) to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Add the attachment without an annotation
            editor.AddDocumentAttachment(attachmentFile, description);

            // Retrieve the underlying Document to set the AFRelationship
            Document doc = editor.Document;

            // The newly added attachment is stored in the EmbeddedFiles collection
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection is 1‑based, so use Count as the last index
                FileSpecification lastSpec = doc.EmbeddedFiles[doc.EmbeddedFiles.Count];
                // Set the relationship type to Data (AFRelationship enum)
                lastSpec.AFRelationship = Aspose.Pdf.AFRelationship.Data;
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
