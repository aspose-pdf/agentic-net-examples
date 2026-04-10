using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";        // existing PDF
        const string attachmentFilePath = "attachment.pdf";   // file to attach
        const string outputPdfPath      = "output.pdf";       // result PDF

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // -----------------------------------------------------------------
        // Add the attachment using PdfContentEditor (Facades API)
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            // Add the attachment without an annotation
            editor.AddDocumentAttachment(attachmentFilePath, "Audit attachment");

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        // -----------------------------------------------------------------
        // Set the document's modification date to the current UTC timestamp
        // -----------------------------------------------------------------
        using (Document doc = new Document(outputPdfPath))
        {
            // DocumentInfo.ModDate expects a DateTime; UTC is used for auditing
            doc.Info.ModDate = DateTime.UtcNow;

            // Overwrite the file with the updated metadata
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and ModDate updated. Output saved to '{outputPdfPath}'.");
    }
}