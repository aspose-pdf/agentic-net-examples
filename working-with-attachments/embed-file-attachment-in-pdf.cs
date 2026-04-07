using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Example byte array representing the file to embed
        byte[] fileData = System.Text.Encoding.UTF8.GetBytes("Sample embedded file content");
        string fileName = "sample.txt";

        // Load the existing PDF document if it exists; otherwise create a new blank PDF
        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            // Create a new document with a single blank page so the example can run without an external file
            doc = new Document();
            doc.Pages.Add();
        }

        // Ensure the document is disposed correctly
        using (doc)
        {
            // Create a FileSpecification from the byte array using a MemoryStream
            using (MemoryStream ms = new MemoryStream(fileData))
            {
                FileSpecification fileSpec = new FileSpecification(ms, fileName)
                {
                    Description = "Embedded sample file"
                };

                // Select the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Define the rectangle for the attachment annotation (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

                // Create the FileAttachmentAnnotation with the page, rectangle, and file specification
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    Icon = FileIcon.PushPin,
                    Contents = "Click to open embedded file"
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPath}");
    }
}
