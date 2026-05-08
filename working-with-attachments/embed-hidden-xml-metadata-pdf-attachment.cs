using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPdf = "output_with_metadata.pdf";

        // Build simple XML metadata
        XDocument xmlMeta = new XDocument(
            new XElement("Metadata",
                new XElement("Author", "John Doe"),
                new XElement("Created", DateTime.UtcNow.ToString("o"))
            )
        );

        // Serialize XML to a memory stream (no file on disk)
        using (MemoryStream xmlStream = new MemoryStream())
        {
            xmlMeta.Save(xmlStream);
            xmlStream.Position = 0; // reset for reading

            // Create a new PDF document (self‑contained, no external input file required)
            using (Document pdfDoc = new Document())
            {
                // Add a blank page so we have a page to host the annotation
                Page page = pdfDoc.Pages.Add();

                // Create a FileSpecification for the XML stream
                // Use the constructor that accepts a file name, then assign the stream to Contents
                FileSpecification fileSpec = new FileSpecification("metadata.xml");
                fileSpec.Contents = xmlStream;

                // Define a zero‑size rectangle; the annotation will be invisible
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                // Create the file attachment annotation
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Mark the annotation as hidden so it does not appear in viewers
                    Flags = AnnotationFlags.Hidden,
                    // Optional: give it a name for programmatic access
                    Name = "DocumentMetadata"
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);

                // Save the modified PDF
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved with hidden XML metadata: {outputPdf}");
    }
}
