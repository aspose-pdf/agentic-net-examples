using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string attachFile1 = "attachment1.txt";
        const string attachFile2 = "attachment2.jpg";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachFile1))
        {
            Console.Error.WriteLine($"Attachment not found: {attachFile1}");
            return;
        }
        if (!File.Exists(attachFile2))
        {
            Console.Error.WriteLine($"Attachment not found: {attachFile2}");
            return;
        }

        // Bind the PDF with PdfContentEditor (required by the task)
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            editor.BindPdf(inputPdf);

            // Use the high‑level Document API to manage attachments
            Document doc = new Document(inputPdf);

            // Add first attachment
            var fileSpec1 = new FileSpecification(Path.GetFileName(attachFile1))
            {
                Description = "First attachment",
                Contents = new MemoryStream(File.ReadAllBytes(attachFile1))
            };
            doc.EmbeddedFiles.Add(fileSpec1);

            // Add second attachment
            var fileSpec2 = new FileSpecification(Path.GetFileName(attachFile2))
            {
                Description = "Second attachment",
                Contents = new MemoryStream(File.ReadAllBytes(attachFile2))
            };
            doc.EmbeddedFiles.Add(fileSpec2);

            // Delete all attachments (including the ones just added)
            // EmbeddedFileCollection does not expose Clear(); use Delete() to remove all.
            doc.EmbeddedFiles.Delete();

            // Save the modified PDF. Using Document.Save ensures the attachment changes are persisted.
            doc.Save(outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Dispose if the editor implements IDisposable (some versions do)
            if (editor is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
