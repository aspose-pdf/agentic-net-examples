using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the attachment(s)
        const string outputPdf = "output.pdf";  // PDF after attachment removal

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfContentEditor provides attachment manipulation methods.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPdf);

            // Remove all embedded file attachments from the document.
            // (If you need to delete a specific attachment, you would need to
            //  extract the list, remove the desired one, and re‑add the rest.
            //  The API currently offers DeleteAttachments() for bulk removal.)
            editor.DeleteAttachments();

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment(s) removed. Output saved to '{outputPdf}'.");
    }
}