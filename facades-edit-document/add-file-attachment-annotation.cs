using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string fileToAttachPath = "attachment.txt";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fileToAttachPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {fileToAttachPath}");
            return;
        }

        // Load the existing PDF using the high‑level Document API
        Document pdfDocument = new Document(inputPdfPath);

        // Define the annotation rectangle (llx, lly, urx, ury) – coordinates are in points
        // Fully qualify Rectangle to avoid ambiguity with System.Drawing
        Aspose.Pdf.Rectangle annotationRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

        // Page number is 1‑based; add the annotation to the first page
        int pageNumber = 1;
        Page page = pdfDocument.Pages[pageNumber];

        // Create a FileSpecification for the file we want to embed
        // The first argument is the path to the file, the second is a description (often the file name)
        FileSpecification fileSpec = new FileSpecification(fileToAttachPath, Path.GetFileName(fileToAttachPath));
        // Optional: set modification date or other parameters
        fileSpec.Params.ModDate = DateTime.UtcNow;

        // Add the FileSpecification to the document's EmbeddedFiles collection (good practice)
        pdfDocument.EmbeddedFiles.Add(fileSpec);

        // Create a file‑attachment annotation that opens the attached file when clicked
        FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, annotationRect, fileSpec)
        {
            // Optional properties
            Title = "Attached File",
            Contents = "Click to open the attached file.",
            Flags = AnnotationFlags.Print
        };

        // Add the annotation to the page
        page.Annotations.Add(fileAttachment);

        // Save the modified PDF
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"PDF saved with attached file link: {outputPdfPath}");
    }
}
