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
        const string outputPdfPath  = "invoice_with_xml.pdf"; // result PDF

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

        // Load the existing PDF
        Document pdfDoc = new Document(inputPdfPath);

        // Create a FileSpecification for the XML attachment using the (filePath, description) constructor
        FileSpecification fileSpec = new FileSpecification(zugferdXmlPath, Path.GetFileName(zugferdXmlPath));

        // Create an (invisible) file‑attachment annotation on the first page
        // Use Aspose.Pdf.Rectangle (not System.Drawing.Rectangle) and set size to zero
        var rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
        FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(pdfDoc.Pages[1], rect, fileSpec)
        {
            Flags = AnnotationFlags.Hidden
        };

        // Add the annotation to the page
        pdfDoc.Pages[1].Annotations.Add(attachment);

        // Save the PDF with the embedded XML
        pdfDoc.Save(outputPdfPath);

        // Verify that the XML attachment is present using the document's EmbeddedFiles collection
        Document verifyDoc = new Document(outputPdfPath);
        bool found = false;
        EmbeddedFileCollection embeddedFiles = verifyDoc.EmbeddedFiles;
        for (int i = 1; i <= embeddedFiles.Count; i++)
        {
            FileSpecification spec = embeddedFiles[i];
            if (string.Equals(spec.Name, Path.GetFileName(zugferdXmlPath), StringComparison.OrdinalIgnoreCase))
            {
                found = true;
                break;
            }
        }

        Console.WriteLine(found
            ? $"ZUGFeRD XML successfully embedded in '{outputPdfPath}'."
            : $"Failed to embed ZUGFeRD XML in '{outputPdfPath}'.");
    }
}
