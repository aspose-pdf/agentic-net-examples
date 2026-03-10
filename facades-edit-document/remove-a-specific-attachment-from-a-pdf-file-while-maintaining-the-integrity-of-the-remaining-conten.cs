using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentNameToRemove = "attachment_to_remove.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Create and bind the PDF using PdfContentEditor (Facade API)
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdfPath);

                // Remove the specific attachment by name via the underlying Document's EmbeddedFiles collection
                // Delete(string) removes the named embedded file; if the name does not exist an exception is thrown,
                // which we catch to avoid breaking the process.
                try
                {
                    editor.Document.EmbeddedFiles.Delete(attachmentNameToRemove);
                    Console.WriteLine($"Attachment '{attachmentNameToRemove}' removed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attachment '{attachmentNameToRemove}' could not be removed: {ex.Message}");
                }

                // Save the modified PDF; this preserves all other content and structure.
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}