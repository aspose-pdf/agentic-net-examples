using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            // Add a file attachment without a visible annotation
            editor.AddDocumentAttachment(attachmentFile, "Sample attachment");
            // Change viewer preference to display the attachment panel on open
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseAttachment);
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and viewer preference set. Saved as '{outputPdf}'.");
    }
}
