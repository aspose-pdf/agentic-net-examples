using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AttachmentRemovalDemo
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF that contains attachments
        const string outputPdfPath = "output_removed.pdf"; // PDF after removal
        const string attachmentToRemove = "sample.txt";    // Name of the attachment to delete

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Example 1: Remove a specific attachment using PdfContentEditor
            // ------------------------------------------------------------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the PDF document into the editor
                editor.BindPdf(inputPdfPath);

                // Access the embedded files collection of the underlying Document
                EmbeddedFileCollection embeddedFiles = editor.Document.EmbeddedFiles;

                // Delete the named attachment (if it does not exist an exception is caught)
                try
                {
                    embeddedFiles.Delete(attachmentToRemove);
                    Console.WriteLine($"Attachment '{attachmentToRemove}' removed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not delete attachment '{attachmentToRemove}': {ex.Message}");
                }

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }

            // ------------------------------------------------------------
            // Example 2: Remove all attachments from a PDF (using the same API)
            // ------------------------------------------------------------
            const string outputAllRemoved = "output_all_attachments_removed.pdf";

            using (PdfContentEditor editorAll = new PdfContentEditor())
            {
                editorAll.BindPdf(inputPdfPath);

                // Delete every embedded file in the document
                editorAll.Document.EmbeddedFiles.Delete();

                editorAll.Save(outputAllRemoved);
                Console.WriteLine("All attachments have been removed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}