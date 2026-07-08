using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document (in memory)
        using (Document pdfDoc = new Document())
        {
            // Add a blank page so the PDF is not empty
            pdfDoc.Pages.Add();

            // ----- Prepare attachment data in a MemoryStream -----
            // Example: attachment content is a simple text string
            byte[] attachmentBytes = System.Text.Encoding.UTF8.GetBytes(
                "This is the content of the attachment stored in memory."
            );

            // MemoryStream holds the attachment without touching the file system
            using (MemoryStream attachmentStream = new MemoryStream(attachmentBytes))
            {
                // Create a FileSpecification with a display name and optional description.
                // The constructor does NOT accept a Stream, so we set the Contents property
                // after construction.
                FileSpecification fileSpec = new FileSpecification("Attachment.txt", "In‑memory attachment");
                fileSpec.Contents = attachmentStream; // assign the stream containing the file data

                // Add the file specification to the PDF's embedded files collection.
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // ----- Save the PDF (still without any intermediate files) -----
            // The resulting PDF will contain the in‑memory attachment.
            pdfDoc.Save("output_with_attachment.pdf");
        }
    }
}
