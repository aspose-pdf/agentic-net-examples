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

        // -----------------------------------------------------------------
        // 1. Create a new PDF document (self‑contained, no external input file)
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page – the annotation will be attached to this page.
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 2. Build the XML metadata that we want to embed
            // -----------------------------------------------------------------
            XDocument metadataXml = new XDocument(
                new XElement("DocumentMetadata",
                    new XElement("CreatedOn", DateTime.UtcNow.ToString("o")),
                    new XElement("Author", "John Doe"),
                    new XElement("Description", "Sample hidden attachment")
                )
            );

            // Serialize the XML into a memory stream (no temporary file needed)
            using (MemoryStream xmlStream = new MemoryStream())
            {
                metadataXml.Save(xmlStream);
                xmlStream.Position = 0; // reset for reading

                // -----------------------------------------------------------------
                // 3. Create a FileSpecification that points to the XML stream
                // -----------------------------------------------------------------
                // Use the constructor that accepts a file name and description, then assign the stream to Contents.
                FileSpecification fileSpec = new FileSpecification("metadata.xml", "XML metadata");
                fileSpec.Contents = xmlStream;

                // -----------------------------------------------------------------
                // 4. Create a hidden FileAttachment annotation on the first page
                // -----------------------------------------------------------------
                // A zero‑size rectangle makes the annotation invisible.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    Flags = AnnotationFlags.Hidden,
                    Contents = "Hidden XML metadata attachment"
                };

                // Add the annotation to the page.
                page.Annotations.Add(attachment);
            }

            // -----------------------------------------------------------------
            // 5. Save the modified PDF (using the required lifecycle pattern)
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with hidden XML attachment: {outputPdf}");
    }
}
