using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input PDF file path
        // 1 - attachment file path
        // 2 - attachment description
        // 3 - output PDF file path (optional)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <attachmentFile> <description> [outputPdf]");
            return;
        }

        string inputPdf = args[0];
        string attachmentFile = args[1];
        string description = args[2];
        string outputPdf = args.Length >= 4 ? args[3] : Path.Combine(
            Path.GetDirectoryName(inputPdf) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPdf) + "_attached.pdf");

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
            // Use PdfContentEditor facade to modify the PDF
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPdf);

                // Add the attachment without a visual annotation
                editor.AddDocumentAttachment(attachmentFile, description);

                // Set viewer preference to show attachment panel on open
                editor.ChangeViewerPreference(Aspose.Pdf.Facades.ViewerPreference.PageModeUseAttachment);

                // Save the modified PDF
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