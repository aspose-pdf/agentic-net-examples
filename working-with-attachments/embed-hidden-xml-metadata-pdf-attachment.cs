using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Build XML metadata for the attachment
        XDocument metadataXml = new XDocument(
            new XElement("AttachmentMetadata",
                new XElement("Author", "John Doe"),
                new XElement("Created", DateTime.UtcNow.ToString("o"))
            )
        );

        // Convert XML to a memory stream (the embedded file)
        using (MemoryStream xmlStream = new MemoryStream())
        {
            metadataXml.Save(xmlStream);
            xmlStream.Position = 0; // reset for reading

            // Create a FileSpecification from the stream
            FileSpecification fileSpec = new FileSpecification(xmlStream, "Metadata.xml");
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Open the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create a hidden file attachment annotation.
                // The rectangle is set to zero size because the annotation is hidden.
                Aspose.Pdf.Rectangle zeroRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(pdfDoc.Pages[1], zeroRect, fileSpec)
                {
                    // Title is shown in the attachment panel; Contents is a tooltip/description.
                    Title = "Metadata.xml",
                    Contents = "Embedded XML metadata (hidden)"
                };

                // Mark the annotation as hidden so it does not appear in the UI
                attachment.Flags = AnnotationFlags.Hidden;

                // Add the annotation to the first page (any page works for hidden annotations)
                pdfDoc.Pages[1].Annotations.Add(attachment);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF with hidden XML attachment saved to '{outputPdfPath}'.");
    }
}
