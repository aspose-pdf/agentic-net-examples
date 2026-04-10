using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that will be edited
        const string inputPdf = "input.pdf";
        // Output PDF with refreshed attachments
        const string outputPdf = "output.pdf";

        // Files to be attached (ensure they exist on disk)
        string[] attachmentPaths = { "file1.txt", "image.png", "doc.pdf" };
        // Corresponding description for each attachment
        string[] attachmentDescriptions = { "Text file", "Image file", "PDF document" };

        // Validate input PDF
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Validate each attachment file
        for (int i = 0; i < attachmentPaths.Length; i++)
        {
            if (!File.Exists(attachmentPaths[i]))
            {
                Console.Error.WriteLine($"Attachment not found: {attachmentPaths[i]}");
                return;
            }
        }

        // Initialize the PdfContentEditor facade (does NOT implement IDisposable)
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF file
        editor.BindPdf(inputPdf);

        // Remove all existing attachments
        editor.DeleteAttachments();

        // Add the new set of attachments
        for (int i = 0; i < attachmentPaths.Length; i++)
        {
            editor.AddDocumentAttachment(attachmentPaths[i], attachmentDescriptions[i]);
        }

        // Save the modified PDF to the output path
        editor.Save(outputPdf);

        // Close the facade (optional but recommended)
        editor.Close();

        Console.WriteLine($"All attachments refreshed. Output saved to '{outputPdf}'.");
    }
}