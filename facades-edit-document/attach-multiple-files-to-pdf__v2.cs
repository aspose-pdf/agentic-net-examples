using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the original PDF that will receive the attachments
        const string basePdfPath = "base.pdf";

        // Directory containing files to attach
        const string attachmentsDir = "attachments";

        // Final output PDF with all attachments
        const string finalPdfPath = "result.pdf";

        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }

        if (!Directory.Exists(attachmentsDir))
        {
            Console.Error.WriteLine($"Attachments directory not found: {attachmentsDir}");
            return;
        }

        // Collect all files to be attached
        string[] filesToAttach = Directory.GetFiles(attachmentsDir);
        if (filesToAttach.Length == 0)
        {
            Console.WriteLine("No files to attach.");
            return;
        }

        // Load the base PDF using Document (PdfFileEditor does not support AttachFile)
        Document pdfDoc = new Document(basePdfPath);

        // Attach each file as an embedded file using FileSpecification
        foreach (string attachmentPath in filesToAttach)
        {
            // The file name that will appear inside the PDF
            string attachmentName = Path.GetFileName(attachmentPath);

            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentPath, attachmentName);
            // Optional: set modification date (helps with PDF/A compliance)
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Add the specification to the document's EmbeddedFiles collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);
        }

        // Save the result
        if (File.Exists(finalPdfPath))
        {
            File.Delete(finalPdfPath);
        }
        pdfDoc.Save(finalPdfPath);

        Console.WriteLine($"All files attached. Output saved to '{finalPdfPath}'.");
    }
}
