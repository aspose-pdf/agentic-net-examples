using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to which files will be attached
        const string basePdfPath = "base.pdf";

        // Output PDF after all attachments are added
        const string finalPdfPath = "output.pdf";

        // Collection of files to attach
        List<string> filesToAttach = new List<string>
        {
            "attachment1.docx",
            "attachment2.xlsx",
            "attachment3.jpg"
        };

        // Verify that the base PDF exists
        if (!File.Exists(basePdfPath))
        {
            Console.Error.WriteLine($"Base PDF not found: {basePdfPath}");
            return;
        }

        // Verify that each attachment exists
        foreach (string attachPath in filesToAttach)
        {
            if (!File.Exists(attachPath))
            {
                Console.Error.WriteLine($"Attachment not found: {attachPath}");
                return;
            }
        }

        // Load the base PDF once
        Document pdfDoc = new Document(basePdfPath);

        // Attach each file using the EmbeddedFiles collection
        foreach (string attachPath in filesToAttach)
        {
            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(attachPath, Path.GetFileName(attachPath))
            {
                // Optional: set a description or MIME type if needed
                Description = $"Embedded attachment: {Path.GetFileName(attachPath)}"
            };

            // Add the file specification to the document's embedded files collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);
        }

        // Save the updated document to the desired output location
        pdfDoc.Save(finalPdfPath);
        Console.WriteLine($"All files attached. Result saved to '{finalPdfPath}'.");
    }
}
