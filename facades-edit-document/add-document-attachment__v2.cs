using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string description = "Additional attachment";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPdf);

            // Add a new attachment without removing existing ones
            editor.AddDocumentAttachment(attachmentPath, description);

            // Save the result to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added. Output saved to '{outputPdf}'.");
    }
}