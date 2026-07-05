using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Usage: AttachAndSetViewer <inputPdf> <attachmentFile> <attachmentDescription> <outputPdf>
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: AttachAndSetViewer <inputPdf> <attachmentFile> <attachmentDescription> <outputPdf>");
            return;
        }

        string inputPdfPath = args[0];
        string attachmentPath = args[1];
        string attachmentDescription = args[2];
        string outputPdfPath = args[3];

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
            // Create the PdfContentEditor facade and bind the source PDF
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdfPath);

                // Add the attachment without an annotation
                editor.AddDocumentAttachment(attachmentPath, attachmentDescription);

                // Set viewer preference to show attachments in the side panel
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