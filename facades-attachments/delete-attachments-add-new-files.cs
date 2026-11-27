using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath1 = "file1.txt";
        const string attachmentPath2 = "file2.jpg";

        // Verify that the source PDF and attachment files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath1))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath1}");
            return;
        }
        if (!File.Exists(attachmentPath2))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath2}");
            return;
        }

        // Use PdfContentEditor only for the DeleteAttachments operation (as required).
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            editor.DeleteAttachments();
        }

        // Load the PDF with the Document class to add new attachments.
        Document pdf = new Document(inputPdf);

        // Remove any embedded files that may still be present in the in‑memory model.
        // EmbeddedFileCollection does not have a Clear() method; use Delete() to remove all.
        pdf.EmbeddedFiles.Delete();

        // Helper to create a FileSpecification from a file on disk.
        FileSpecification CreateFileSpec(string filePath, string description)
        {
            return new FileSpecification(Path.GetFileName(filePath))
            {
                Description = description,
                Contents = new MemoryStream(File.ReadAllBytes(filePath))
            };
        }

        // Add the two new attachments.
        pdf.EmbeddedFiles.Add(CreateFileSpec(attachmentPath1, "First attachment"));
        pdf.EmbeddedFiles.Add(CreateFileSpec(attachmentPath2, "Second attachment"));

        // Save the modified PDF.
        pdf.Save(outputPdf);

        Console.WriteLine($"Attachments updated and saved to '{outputPdf}'.");
    }
}
