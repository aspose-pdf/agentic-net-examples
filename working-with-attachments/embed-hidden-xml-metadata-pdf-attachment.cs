using System;
using System.IO;
using System.Xml.Serialization;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAttachmentMetadata
{
    // Sample class representing the metadata to be serialized.
    public class AttachmentMetadata
    {
        // Initialise with safe defaults to satisfy non‑nullable warnings.
        public string DocumentId { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "output_with_metadata.pdf";

            // ---------------------------------------------------------------------
            // Ensure the input PDF exists – create a minimal placeholder if it does not.
            // ---------------------------------------------------------------------
            if (!File.Exists(inputPdfPath))
            {
                using (var placeholder = new Document())
                {
                    placeholder.Pages.Add();
                    placeholder.Save(inputPdfPath);
                }
            }

            // Prepare the metadata instance.
            AttachmentMetadata metadata = new AttachmentMetadata
            {
                DocumentId = Guid.NewGuid().ToString(),
                Author = "John Doe",
                Created = DateTime.UtcNow,
                Description = "Sample attachment metadata serialized to XML."
            };

            // Serialize the metadata to an in‑memory XML stream.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AttachmentMetadata));
                serializer.Serialize(xmlStream, metadata);
                xmlStream.Position = 0; // Reset for reading.

                // Load the existing PDF document.
                using (Document pdfDoc = new Document(inputPdfPath))
                {
                    // Choose the page where the hidden annotation will be placed (first page).
                    Page page = pdfDoc.Pages[1];

                    // Create a FileSpecification that embeds the XML stream.
                    // The second argument is the name that will appear in the attachment list.
                    FileSpecification fileSpec = new FileSpecification(xmlStream, "metadata.xml");

                    // Define a zero‑size rectangle; the annotation will not be visible.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                    // Create the file attachment annotation.
                    FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                    {
                        // Mark the annotation as hidden so it does not appear in the UI.
                        Flags = AnnotationFlags.Hidden,

                        // Provide a title and optional contents for completeness.
                        Title = "Metadata",
                        Contents = "Embedded XML metadata (hidden)."
                    };

                    // Add the annotation to the page.
                    page.Annotations.Add(fileAttachment);

                    // Save the modified PDF.
                    pdfDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"PDF saved with hidden XML attachment: {outputPdfPath}");
        }
    }
}
