using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "invoice.pdf";          // existing PDF
        const string zugferdXmlPath = "invoice.xml";          // ZUGFeRD XML file
        const string outputPdfPath  = "invoice_with_xml.pdf"; // output PDF

        // Verify source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a file specification for the XML attachment
            FileSpecification xmlFileSpec = new FileSpecification(zugferdXmlPath)
            {
                Description = "ZUGFeRD Invoice XML"
            };

            // Define a rectangle for the attachment annotation (position on page)
            Rectangle rect = new Rectangle(100, 500, 200, 600);

            // Create the file attachment annotation on the first page (note the required FileSpecification argument)
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(pdfDoc.Pages[1], rect, xmlFileSpec)
            {
                Name  = "ZUGFeRD",
                Color = Aspose.Pdf.Color.LightGray
                // Icon can be set if the enum is available; omitted here to keep compatibility
            };

            // Add the annotation to the page
            pdfDoc.Pages[1].Annotations.Add(attachment);

            // Save the modified PDF (lifecycle rule: Save inside using)
            pdfDoc.Save(outputPdfPath);
        }

        // Verify that the XML attachment is present in the saved PDF
        using (Document verifyDoc = new Document(outputPdfPath))
        {
            bool xmlFound = false;
            foreach (Page page in verifyDoc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is FileAttachmentAnnotation fileAnn && fileAnn.File != null)
                    {
                        // Compare file names (case‑insensitive) using the Name property of FileSpecification
                        string attachedName = Path.GetFileName(fileAnn.File.Name);
                        string expectedName = Path.GetFileName(zugferdXmlPath);
                        if (string.Equals(attachedName, expectedName, StringComparison.OrdinalIgnoreCase))
                        {
                            xmlFound = true;
                            break;
                        }
                    }
                }
                if (xmlFound) break;
            }

            Console.WriteLine(xmlFound
                ? "ZUGFeRD XML successfully embedded."
                : "Failed to embed ZUGFeRD XML.");
        }
    }
}
