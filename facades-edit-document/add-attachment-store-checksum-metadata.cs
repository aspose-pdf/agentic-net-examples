using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";          // source PDF
        const string attachmentPath    = "attachment.bin";     // file to attach
        const string outputPdfPath     = "output_with_checksum.pdf";

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

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use PdfContentEditor to add the attachment (no annotation)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(pdfDoc);
            editor.AddDocumentAttachment(attachmentPath, "Attached file for integrity check");
            // At this point the attachment is part of the document's EmbeddedFiles collection

            // Retrieve the newly added attachment (last entry in the collection)
            FileSpecification attachedFileSpec = pdfDoc.EmbeddedFiles[pdfDoc.EmbeddedFiles.Count];
            // Access the checksum (MD5) of the embedded file
            string checksum = attachedFileSpec.Params?.CheckSum ?? string.Empty;

            // Store the checksum in a standard metadata field (Keywords) for later verification
            pdfDoc.Info.Keywords = checksum;

            // Save the modified PDF (save rule)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and checksum stored in Keywords. Output saved to '{outputPdfPath}'.");
    }
}