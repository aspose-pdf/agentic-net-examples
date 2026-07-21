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

        // Ensure a source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add();
                seed.Save(inputPdfPath);
            }
        }

        // Load the existing PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Build XML metadata (example structure)
            XDocument xmlMeta = new XDocument(
                new XElement("AttachmentMetadata",
                    new XElement("Author", "John Doe"),
                    new XElement("Created", DateTime.UtcNow.ToString("o")),
                    new XElement("Description", "Sample hidden attachment")
                )
            );

            // Serialize XML to a memory stream
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlMeta.Save(xmlStream);
                xmlStream.Position = 0; // reset for reading

                // Create a file specification that points to the XML stream
                FileSpecification fileSpec = new FileSpecification(xmlStream, "metadata.xml");

                // Define a tiny (invisible) rectangle on the first page
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                // Create the file attachment annotation and mark it as hidden
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(pdfDoc.Pages[1], rect, fileSpec)
                {
                    Flags = AnnotationFlags.Hidden,
                    Name = "HiddenMetadata"
                };

                // Add the annotation to the page
                pdfDoc.Pages[1].Annotations.Add(attachment);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with hidden XML attachment: {outputPdfPath}");
    }
}
