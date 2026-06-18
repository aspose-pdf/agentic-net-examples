using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF with attachments
        const string outputPdf = "output_with_new_attachment.pdf";
        const string attachmentPath = "new_attachment.docx";
        const string description = "Additional document attachment";

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

        // PdfContentEditor edits the PDF without removing existing attachments
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);                                   // load PDF
        editor.AddDocumentAttachment(attachmentPath, description); // add new attachment
        editor.Save(outputPdf);                                     // save result
        editor.Close();                                            // release resources

        Console.WriteLine($"Attachment added. Saved to '{outputPdf}'.");
    }
}