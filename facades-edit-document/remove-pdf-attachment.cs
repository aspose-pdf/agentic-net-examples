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

        // Bind the PDF using the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Remove the specific attachment (if it exists) via the document's EmbeddedFiles collection
        editor.Document.EmbeddedFiles.Delete(attachmentName);

        // Save the updated PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Attachment '{attachmentName}' removed. Saved to '{outputPath}'.");
    }
}