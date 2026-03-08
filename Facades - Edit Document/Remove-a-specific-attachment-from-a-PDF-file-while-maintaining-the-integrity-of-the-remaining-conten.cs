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
        const string attachmentName = "attachment_file.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the PdfContentEditor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Remove the specific attachment by its name
            // The EmbeddedFiles collection is accessed via the Document property
            editor.Document.EmbeddedFiles.Delete(attachmentName);

            // Save the updated PDF preserving all other content
            editor.Save(outputPath);
        }

        Console.WriteLine($"Attachment '{attachmentName}' removed. Saved to '{outputPath}'.");
    }
}