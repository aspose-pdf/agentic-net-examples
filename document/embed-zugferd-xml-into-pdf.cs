using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";          // existing PDF to embed into
        const string xmlPath      = "invoice.xml";        // ZUGFeRD XML file
        const string outputPdf    = "output_with_zugferd.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the PDF, embed the XML as an attachment, and save.
        using (Document doc = new Document(pdfPath))
        {
            // Create a file specification for the XML attachment.
            // The first argument is the file name as it will appear in the PDF.
            // The second argument is a description.
            FileSpecification xmlAttachment = new FileSpecification(Path.GetFileName(xmlPath), "ZUGFeRD Invoice");
            // Set MIME type for proper identification.
            xmlAttachment.MIMEType = "application/xml";

            // Add the attachment to the PDF.
            doc.EmbeddedFiles.Add(xmlAttachment);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        // Verify that the attachment is present.
        using (Document verifyDoc = new Document(outputPdf))
        {
            if (verifyDoc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            bool found = false;
            foreach (FileSpecification spec in verifyDoc.EmbeddedFiles)
            {
                // Compare by file name (case‑insensitive) or MIME type.
                if (string.Equals(spec.Name, Path.GetFileName(xmlPath), StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(spec.MIMEType, "application/xml", StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    Console.WriteLine($"Attachment found: {spec.Name}");
                    Console.WriteLine($"Description : {spec.Description}");
                    Console.WriteLine($"MIME Type   : {spec.MIMEType}");
                    break;
                }
            }

            Console.WriteLine(found
                ? "ZUGFeRD XML attachment successfully embedded."
                : "ZUGFeRD XML attachment not found.");
        }
    }
}