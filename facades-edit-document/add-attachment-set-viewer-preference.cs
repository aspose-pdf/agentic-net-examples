using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Usage:
    //   AttachAndSetViewer.exe <inputPdf> <attachmentFile> <attachmentDescription> <outputPdf>
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <attachmentFile> <attachmentDescription> <outputPdf>");
            return;
        }

        string inputPdfPath      = args[0];
        string attachmentPath    = args[1];
        string attachmentDesc    = args[2];
        string outputPdfPath     = args[3];

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // PdfContentEditor is a disposable facade; use using for deterministic cleanup
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF document
                editor.BindPdf(inputPdfPath);

                // Add the attachment (no visual annotation)
                editor.AddDocumentAttachment(attachmentPath, attachmentDesc);

                // Set viewer preference to show the attachment panel
                editor.ChangeViewerPreference(ViewerPreference.PageModeUseAttachment);

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added and viewer preference set. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}