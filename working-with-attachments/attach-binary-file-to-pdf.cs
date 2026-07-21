using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and sample data to embed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        byte[] fileData = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF }; // example byte array

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification from the byte array using a MemoryStream
            using (MemoryStream ms = new MemoryStream(fileData))
            {
                // The second argument is the name that will appear in the attachment list
                FileSpecification fileSpec = new FileSpecification(ms, "sample.bin", "Sample binary data");

                // Optionally add the file to the document's embedded files collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Choose the page to place the attachment annotation (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the rectangle that represents the annotation's border
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

                // Create the FileAttachmentAnnotation using the constructor that accepts the FileSpecification
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional visual settings
                    // The Icon property expects a FileIcon enum value.
                    Icon = FileIcon.Paperclip,
                    Color = Aspose.Pdf.Color.Blue,
                    Contents = "Embedded sample file"
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF (save inside the using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with embedded file attachment: {outputPdfPath}");
    }
}
