using System;
using System.IO;
using System.Xml.Serialization;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAttachmentMetadata
{
    // Sample metadata class to be serialized to XML
    public class AttachmentMetadata
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public long SizeBytes { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath  = "input.pdf";
            const string outputPdfPath = "output_with_metadata.pdf";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
                return;
            }

            // Create sample metadata
            AttachmentMetadata metadata = new AttachmentMetadata {
                FileName    = "example.txt",
                Description = "Sample attachment metadata",
                SizeBytes   = 12345
            };

            // Serialize metadata to XML string
            string xmlMetadata;
            XmlSerializer serializer = new XmlSerializer(typeof(AttachmentMetadata));
            using (StringWriter stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, metadata);
                xmlMetadata = stringWriter.ToString();
            }

            // Load the PDF and embed the XML as a hidden annotation
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Ensure we have at least one page
                if (pdfDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The PDF has no pages.");
                    return;
                }

                // Create a zero‑size rectangle (position does not matter for hidden annotation)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                // Create a text annotation that will hold the XML
                TextAnnotation hiddenAnnotation = new TextAnnotation(pdfDoc.Pages[1], rect)
                {
                    // Store the XML in the Contents property
                    Contents = xmlMetadata,
                    // Mark the annotation as hidden so it does not appear in viewers
                    Flags    = AnnotationFlags.Hidden,
                    // Optional: give it a title for identification
                    Title    = "AttachmentMetadata"
                };

                // Add the annotation to the first page
                pdfDoc.Pages[1].Annotations.Add(hiddenAnnotation);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved with embedded metadata: {outputPdfPath}");
        }
    }
}