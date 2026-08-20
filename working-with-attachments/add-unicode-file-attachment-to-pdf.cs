using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output_with_attachment.pdf";
        const string fileToAttach = "sample.txt";

        // Ensure the file to attach exists; create a simple one if missing.
        if (!File.Exists(fileToAttach))
        {
            File.WriteAllText(fileToAttach, "Sample attachment content.");
        }

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a blank page (first page, 1‑based indexing).
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (position and size).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a FileSpecification for the attachment using the file path.
            FileSpecification fileSpec = new FileSpecification(fileToAttach)
            {
                // Set a Unicode filename (e.g., Chinese characters).
                UnicodeName = "示例文件.txt"
            };

            // Create the file attachment annotation.
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Icon assignment is optional; omitted to avoid version‑specific enum issues.
                Contents = "Attached file with Unicode name", // tooltip text
                Title = "Unicode Attachment"                 // title shown in popup
            };

            // Add the annotation to the page.
            page.Annotations.Add(attachment);

            // Save the PDF document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. Open it in a viewer to verify the attachment.");
    }
}
