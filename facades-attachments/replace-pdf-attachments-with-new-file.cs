using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF using the high‑level Document API.
        Document pdf = new Document(inputPdf);

        // ------------------------------------------------------------
        // Remove all existing embedded file attachments.
        // EmbeddedFileCollection uses 1‑based indexing and does not expose a Clear() method.
        // We iterate backwards and delete each attachment by its name.
        // ------------------------------------------------------------
        if (pdf.EmbeddedFiles != null && pdf.EmbeddedFiles.Count > 0)
        {
            for (int i = pdf.EmbeddedFiles.Count; i >= 1; i--)
            {
                FileSpecification spec = pdf.EmbeddedFiles[i];
                if (spec != null && !string.IsNullOrEmpty(spec.Name))
                {
                    pdf.EmbeddedFiles.Delete(spec.Name);
                }
            }
        }

        // ------------------------------------------------------------
        // Create a FileSpecification for the new attachment and add it.
        // ------------------------------------------------------------
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
        {
            Description = "Sample attachment",
            // Contents expects a Stream; we wrap the file bytes in a MemoryStream.
            Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
        };
        pdf.EmbeddedFiles.Add(fileSpec);

        // Save the modified PDF.
        pdf.Save(outputPdf);

        Console.WriteLine($"PDF saved with new attachment to '{outputPdf}'.");
    }
}
