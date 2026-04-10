using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "OldReport.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor does not implement IDisposable, so use try/finally
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Remove the specific attachment (if it exists)
            // EmbeddedFiles.Delete removes an attachment by name
            editor.Document.EmbeddedFiles.Delete(attachmentName);

            // Save the modified PDF
            editor.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"Attachment '{attachmentName}' removed. Saved to '{outputPath}'.");
    }
}