using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    // Maximum allowed file size in bytes (5 MB)
    const long MaxFileSizeBytes = 5L * 1024 * 1024;

    static void Main()
    {
        const string pdfTemplatePath = "form_template.pdf";   // existing PDF with a FileSelectBoxField
        const string outputPdfPath   = "form_with_attachment.pdf";
        const string fileToAttach    = "user_document.pdf";   // file user wants to submit

        // Verify the file exists
        if (!File.Exists(fileToAttach))
        {
            Console.Error.WriteLine($"File not found: {fileToAttach}");
            return;
        }

        // Validate file size (must not exceed 5 MB)
        long fileSize = new FileInfo(fileToAttach).Length;
        if (fileSize > MaxFileSizeBytes)
        {
            Console.Error.WriteLine($"File size {fileSize / (1024 * 1024)} MB exceeds the 5 MB limit.");
            return;
        }

        // Load the PDF form (using the prescribed lifecycle rule)
        using (Document doc = new Document(pdfTemplatePath))
        {
            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(fileToAttach);

            // Define the rectangle where the attachment annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the file attachment annotation on the first page
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec)
            {
                // Optional visual settings
                Icon = FileIcon.Paperclip, // Use the enum value instead of a string
                Color = Aspose.Pdf.Color.Blue,
                Contents = $"Attached file: {Path.GetFileName(fileToAttach)}"
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(attachment);

            // Save the updated PDF (using the prescribed lifecycle rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"File attached successfully. Output saved to '{outputPdfPath}'.");
    }
}
