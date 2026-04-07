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
        // 1. Create a PDF document (if you have an existing one, load it).
        // -----------------------------------------------------------------
        Document doc = CreateOrLoadDocument();

        // -----------------------------------------------------------------
        // 2. Create XML metadata (example content)
        // -----------------------------------------------------------------
        XDocument xmlMeta = new XDocument(
            new XElement("Metadata",
                new XElement("Author", "John Doe"),
                new XElement("Created", DateTime.UtcNow.ToString("o")),
                new XElement("Description", "Sample attachment metadata")
            )
        );

        // Write XML to a memory stream (no temporary file needed)
        using (MemoryStream xmlStream = new MemoryStream())
        {
            xmlMeta.Save(xmlStream);
            xmlStream.Position = 0; // reset for reading

            // -----------------------------------------------------------------
            // 3. Create a FileSpecification that embeds the XML stream
            // -----------------------------------------------------------------
            // Use the overload that accepts a Stream and a file name.
            FileSpecification fileSpec = new FileSpecification(xmlStream, "metadata.xml");

            // -----------------------------------------------------------------
            // 4. Create a hidden FileAttachment annotation on the first page
            // -----------------------------------------------------------------
            // Use a zero‑size rectangle so the annotation is not visible.
            var rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Page indexing in Aspose.Pdf is 1‑based.
            Page page = doc.Pages[1];

            var attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Mark the annotation as hidden (does not appear in the UI)
                Flags = AnnotationFlags.Hidden,

                // Optional: give it a name for programmatic access
                Name = "MetadataAttachment"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(attachment);
        }

        // -----------------------------------------------------------------
        // 5. Save the modified PDF
        // -----------------------------------------------------------------
        doc.Save(outputPdf);
        doc.Dispose();

        Console.WriteLine($"PDF saved with embedded XML metadata: {outputPdf}");
    }

    /// <summary>
    /// Loads an existing PDF if it exists; otherwise creates a new one with a single blank page.
    /// </summary>
    private static Document CreateOrLoadDocument()
    {
        const string inputPdf = "input.pdf";
        if (File.Exists(inputPdf))
        {
            return new Document(inputPdf);
        }
        else
        {
            // Create a minimal PDF with one empty page so the example is self‑contained.
            Document doc = new Document();
            doc.Pages.Add();
            return doc;
        }
    }
}
