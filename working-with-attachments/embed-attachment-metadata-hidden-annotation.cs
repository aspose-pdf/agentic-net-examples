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
        const string outputPdfPath = "output_with_metadata.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Build XML containing metadata of all embedded files (attachments)
            XElement root = new XElement("Attachments");

            // Iterate over each embedded file in the document
            foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
            {
                XElement fileElem = new XElement("File",
                    new XAttribute("Name", fileSpec.Name ?? string.Empty),
                    new XAttribute("Description", fileSpec.Description ?? string.Empty),
                    new XAttribute("MimeType", fileSpec.MIMEType ?? string.Empty));

                // Optional parameters (may be null)
                if (fileSpec.Params != null)
                {
                    fileElem.Add(new XAttribute("Size", fileSpec.Params.Size));

                    // CreationDate and ModDate are non‑nullable DateTime values in Aspose.Pdf.
                    // Use a sentinel check (DateTime.MinValue) to decide whether to output them.
                    string creationDateStr = fileSpec.Params.CreationDate != DateTime.MinValue
                        ? fileSpec.Params.CreationDate.ToString("o")
                        : string.Empty;
                    string modDateStr = fileSpec.Params.ModDate != DateTime.MinValue
                        ? fileSpec.Params.ModDate.ToString("o")
                        : string.Empty;

                    fileElem.Add(new XAttribute("CreationDate", creationDateStr));
                    fileElem.Add(new XAttribute("ModDate", modDateStr));
                    fileElem.Add(new XAttribute("CheckSum", fileSpec.Params.CheckSum ?? string.Empty));
                }

                root.Add(fileElem);
            }

            // Create the XML document in memory
            XDocument metadataXml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);

            // Write XML to a temporary file (required for FileSpecification constructor)
            string tempXmlPath = Path.Combine(Path.GetTempPath(), $"AttachmentMetadata_{Guid.NewGuid()}.xml");
            metadataXml.Save(tempXmlPath);

            // Create a FileSpecification for the XML file
            FileSpecification xmlFileSpec = new FileSpecification(tempXmlPath, "Attachment metadata XML");

            // Define a small invisible rectangle for the hidden annotation (e.g., 0x0 size at page origin)
            Aspose.Pdf.Rectangle hiddenRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create the file attachment annotation on the first page
            Page firstPage = pdfDoc.Pages[1];
            FileAttachmentAnnotation hiddenAnnotation = new FileAttachmentAnnotation(firstPage, hiddenRect, xmlFileSpec)
            {
                // Set the annotation to be hidden using the Flags property
                Flags = AnnotationFlags.Hidden
            };

            // Add the hidden annotation to the page
            firstPage.Annotations.Add(hiddenAnnotation);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with hidden metadata annotation: {outputPdfPath}");
    }
}
