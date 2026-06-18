using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_attachments.pdf";

        // Define the maximum allowed attachment size (e.g., 5 MB)
        const long maxAttachmentSizeBytes = 5L * 1024 * 1024;

        // List of files to attach
        string[] filesToAttach = { "doc1.pdf", "image.png", "large_file.zip" };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use the first page for the attachment annotations (adjust as needed)
            Page targetPage = pdfDoc.Pages[1];

            // Position for the first attachment annotation
            double llx = 100, lly = 700, urx = 150, ury = 750;

            foreach (string filePath in filesToAttach)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Skipping missing file: {filePath}");
                    continue;
                }

                // Validate attachment size before adding
                long fileSize = new FileInfo(filePath).Length;
                if (fileSize > maxAttachmentSizeBytes)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(filePath)}' (size {fileSize} bytes exceeds limit).");
                    continue;
                }

                // Create a FileSpecification that describes the attached file
                FileSpecification fileSpec = new FileSpecification(filePath);

                // Define the rectangle for the annotation (adjust position for each attachment)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the FileAttachment annotation and add it to the page
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(targetPage, rect, fileSpec)
                {
                    // Optional: set a tooltip (icon is left as default to avoid version‑specific enum issues)
                    Contents = $"Attachment: {Path.GetFileName(filePath)}"
                };
                targetPage.Annotations.Add(attachment);

                // Move the next annotation downwards to avoid overlap
                lly -= 60;
                ury -= 60;
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdfPath}");
    }
}
