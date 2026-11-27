using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "attachment_free.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF.
            Document pdf = new Document(inputPath);

            // Remove all embedded file attachments.
            if (pdf.EmbeddedFiles != null && pdf.EmbeddedFiles.Count > 0)
            {
                // The EmbeddedFileCollection does not have a Clear() method.
                // Use Delete() to remove all attachments in one call.
                pdf.EmbeddedFiles.Delete();
            }

            // Optional: using PdfContentEditor (if you prefer the high‑level editor).
            // PdfContentEditor editor = new PdfContentEditor();
            // editor.BindPdf(pdf);
            // editor.DeleteEmbeddedFiles(); // removes all attachments

            // Save the cleaned PDF.
            pdf.Save(outputPath);

            Console.WriteLine($"Attachment‑free PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
