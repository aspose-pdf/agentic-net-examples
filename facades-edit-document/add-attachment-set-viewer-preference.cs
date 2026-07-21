using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes: PdfContentEditor, ViewerPreference

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input PDF path
        // 1 - file to attach (any type)
        // 2 - description for the attachment
        // 3 - output PDF path
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <input.pdf> <attachment.file> <description> <output.pdf>");
            return;
        }

        string inputPdf      = args[0];
        string attachmentFile = args[1];
        string description    = args[2];
        string outputPdf      = args[3];

        // Validate files
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

        try
        {
            // Initialize the facade and bind the source PDF
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdf);

            // Add the attachment without creating a visible annotation
            editor.AddDocumentAttachment(attachmentFile, description);

            // Set viewer preference to show attachments in the PDF viewer UI
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseAttachment);

            // Save the modified PDF to the specified output path
            editor.Save(outputPdf);

            // Close the facade (optional, but releases resources)
            editor.Close();

            Console.WriteLine($"Attachment added and viewer preference set. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}