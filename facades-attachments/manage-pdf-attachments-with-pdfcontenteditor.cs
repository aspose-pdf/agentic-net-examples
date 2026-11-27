using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string attachmentName = "sample.txt";          // name that will appear in the PDF
        const string attachmentFile = "sample.txt";          // local file to be attached

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Bind the PDF with PdfContentEditor (as required by the task)
            // ------------------------------------------------------------
            var editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // ------------------------------------------------------------
            // 2. Work with the high‑level Document API to manage attachments
            // ------------------------------------------------------------
            // The PdfContentEditor facade exposes the underlying Document via the
            // "Document" property.
            Document pdfDoc = editor.Document;

            // Remove all existing attachments (if any)
            if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
            {
                // EmbeddedFileCollection uses 1‑based indexing.
                for (int i = pdfDoc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    FileSpecification spec = pdfDoc.EmbeddedFiles[i];
                    if (spec != null && !string.IsNullOrEmpty(spec.Name))
                    {
                        pdfDoc.EmbeddedFiles.Delete(spec.Name);
                    }
                }
            }

            // Add a new attachment from a stream
            using (FileStream attStream = File.OpenRead(attachmentFile))
            {
                // NOTE: The constructor expects the stream first, then the name.
                var fileSpec = new FileSpecification(attStream, attachmentName);
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // ------------------------------------------------------------
            // 3. Save the modified PDF
            // ------------------------------------------------------------
            // Using the editor to save ensures any facade‑specific state is persisted.
            editor.Save(outputPdfPath);

            Console.WriteLine($"PDF processed successfully. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
