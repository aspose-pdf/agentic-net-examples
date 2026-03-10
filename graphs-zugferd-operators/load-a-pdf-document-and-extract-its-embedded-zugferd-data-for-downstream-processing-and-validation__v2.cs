using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "invoice.pdf";          // PDF containing ZUGFeRD data
        const string outputXmlPath = "ZUGFeRD.xml";         // Destination for extracted XML

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ZUGFeRD data is stored as an embedded XML file attached to the PDF.
            // In recent Aspose.Pdf versions the attachment is represented by a
            // FileAttachmentAnnotation. The attached file is accessed via the
            // FileSpecification returned by the annotation's File property.
            bool zugferdFound = false;

            foreach (Page page in pdfDoc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        FileSpecification spec = fileAnn.File;
                        // The property that holds the attachment name is "Name" in the current API.
                        string fileName = spec?.Name;

                        if (!string.IsNullOrEmpty(fileName) &&
                            fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            // Write the embedded XML to the desired location.
                            using (FileStream outStream = File.Create(outputXmlPath))
                            using (Stream contentStream = spec.Contents)
                            {
                                contentStream.CopyTo(outStream);
                            }

                            Console.WriteLine($"ZUGFeRD data extracted to: {outputXmlPath}");
                            zugferdFound = true;
                            break; // Assuming only one ZUGFeRD XML is present.
                        }
                    }
                }
                if (zugferdFound) break;
            }

            if (!zugferdFound)
            {
                Console.WriteLine("No ZUGFeRD XML attachment found in the PDF.");
            }
        }
    }
}
