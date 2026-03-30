using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "base.pdf";
        const string outputPdf = "output.pdf";

        // Files to be attached to the PDF
        string[] attachments = new string[] { "file1.pdf", "file2.docx", "image.png" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Base PDF not found: {inputPdf}");
            return;
        }

        foreach (string attachPath in attachments)
        {
            if (!File.Exists(attachPath))
            {
                Console.Error.WriteLine($"Attachment not found: {attachPath}");
                return;
            }
        }

        // Use PdfContentEditor to add attachments
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        foreach (string attachPath in attachments)
        {
            string description = Path.GetFileName(attachPath);
            editor.AddDocumentAttachment(attachPath, description);
        }

        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Attachments added. Saved to '{outputPdf}'.");
    }
}
