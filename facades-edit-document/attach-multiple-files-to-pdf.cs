using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that will receive the attachments
        const string sourcePdfPath = "source.pdf";
        // Path for the resulting PDF with all attachments
        const string outputPdfPath = "output.pdf";

        // Collection of files to attach
        string[] filesToAttach = new string[]
        {
            "document1.txt",
            "image1.png",
            "report.pdf"
        };

        // Validate source PDF existence
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Validate each attachment file existence
        foreach (string file in filesToAttach)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Attachment file not found: {file}");
                return;
            }
        }

        // Create the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(sourcePdfPath);

        // Attach each file to the PDF (no visual annotation)
        foreach (string file in filesToAttach)
        {
            // Use the file name as a simple description
            string description = Path.GetFileName(file);
            editor.AddDocumentAttachment(file, description);
        }

        // Save the PDF with all attachments
        editor.Save(outputPdfPath);

        Console.WriteLine($"All attachments added. Output saved to '{outputPdfPath}'.");
    }
}