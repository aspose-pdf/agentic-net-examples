using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - path to the source PDF
        // 1 - path to the file to attach
        // 2 - description for the attachment
        // 3 - path for the output PDF
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <inputPdf> <attachmentFile> <description> <outputPdf>");
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
            // PdfContentEditor implements IDisposable, so use a using block.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the source PDF.
                editor.BindPdf(inputPdf);

                // Add the attachment without an annotation.
                editor.AddDocumentAttachment(attachmentFile, description);

                // Set the viewer preference to show attachments pane.
                editor.ChangeViewerPreference(ViewerPreference.PageModeUseAttachment);

                // Save the modified PDF.
                editor.Save(outputPdf);
            }

            Console.WriteLine($"Attachment added and viewer preference set. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}