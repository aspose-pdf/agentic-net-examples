using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document (ensures proper disposal of resources)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor facade
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the loaded document to the editor
            editor.BindPdf(doc);

            // Delete all embedded file attachments
            editor.DeleteAttachments();

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Attachments removed. Saved file: '{outputPath}'.");
    }
}