using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF, attachment file, description, output PDF
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: AddAttachmentAndSetViewer <inputPdf> <attachmentFile> <description> <outputPdf>");
            return;
        }

        string inputPdf = args[0];
        string attachmentFile = args[1];
        string description = args[2];
        string outputPdf = args[3];

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

            // Add the attachment without an annotation
            editor.AddDocumentAttachment(attachmentFile, description);

            // Set viewer preference to show the attachment panel
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseAttachment);

            // Save the modified PDF
            editor.Save(outputPdf);

            Console.WriteLine($"Successfully added attachment and set viewer preference. Output: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}