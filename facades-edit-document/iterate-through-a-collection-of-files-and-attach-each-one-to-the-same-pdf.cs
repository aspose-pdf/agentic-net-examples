using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Path to the PDF that will receive the attachments
        const string basePdfPath = "base.pdf";

        // Path where the resulting PDF will be saved
        const string outputPdfPath = "output.pdf";

        // Directory containing files to be attached
        const string attachmentsDirectory = "attachments";

        // Validate inputs
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }

        if (!Directory.Exists(attachmentsDirectory))
        {
            Console.Error.WriteLine($"Attachments directory not found: {attachmentsDirectory}");
            return;
        }

        // Gather all files to attach
        string[] filesToAttach = Directory.GetFiles(attachmentsDirectory);

        // Initialize the content editor and bind the base PDF
        Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor();
        editor.BindPdf(basePdfPath);

        // Attach each file to the PDF
        foreach (string attachmentPath in filesToAttach)
        {
            string description = $"Attachment: {Path.GetFileName(attachmentPath)}";
            editor.AddDocumentAttachment(attachmentPath, description);
        }

        // Save the PDF with all attachments
        editor.Save(outputPdfPath);
        Console.WriteLine($"All attachments added. Saved to '{outputPdfPath}'.");
    }
}