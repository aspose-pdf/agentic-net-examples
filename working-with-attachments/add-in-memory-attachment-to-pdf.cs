using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.AI; // Attachment class (kept for completeness, not used directly)

class Program
{
    static void Main()
    {
        // Prepare the attachment content in a memory stream (no disk I/O)
        using (MemoryStream attachmentStream = new MemoryStream())
        {
            // Example content – write some text into the stream
            using (StreamWriter writer = new StreamWriter(attachmentStream, leaveOpen: true))
            {
                writer.Write("This is an attachment created entirely in memory.");
                writer.Flush();
                // Reset position so it can be read from the beginning later
                attachmentStream.Position = 0;
            }

            // Create a new PDF document (in‑memory)
            using (Document pdfDoc = new Document())
            {
                // Add a blank page so the PDF is not empty
                pdfDoc.Pages.Add();

                // The FileSpecification overload that accepts a Stream comes first,
                // followed by the file name (string).
                FileSpecification fileSpec = new FileSpecification(attachmentStream, "SampleAttachment.txt");

                // Add the file specification to the document's embedded files collection
                pdfDoc.EmbeddedFiles.Add("SampleAttachment.txt", fileSpec);

                // Save the resulting PDF into another memory stream (still no disk writes)
                using (MemoryStream outputPdf = new MemoryStream())
                {
                    pdfDoc.Save(outputPdf);

                    // For demonstration purposes only: write the PDF bytes to disk
                    // In a real scenario you could return the stream, send it over a network, etc.
                    File.WriteAllBytes("ResultWithAttachment.pdf", outputPdf.ToArray());
                }
            }
        }
    }
}