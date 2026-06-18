using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the result PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Example byte array to embed (e.g., a simple text file)
        byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes("This is the content of the embedded file.");
        const string embeddedFileName = "sample.txt";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Open the PDF, add the file attachment, and save
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification from the byte array using a MemoryStream
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                FileSpecification fileSpec = new FileSpecification(ms, embeddedFileName);
                fileSpec.Description = "Embedded sample text file";

                // Define the rectangle where the annotation will appear (coordinates are in points)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

                // Create the FileAttachmentAnnotation on the first page
                Page page = doc.Pages[1]; // Aspose.Pdf uses 1‑based page indexing
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    Title    = "Sample Attachment",
                    Contents = "Click to open the embedded file."
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with embedded file: {outputPdf}");
    }
}