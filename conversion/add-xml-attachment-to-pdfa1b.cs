using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";      // Input PDF
        const string xmlAttachmentPath = "data.xml";    // XML file to attach
        const string outputPdfPath = "output_pdfa1b.pdf"; // Resulting PDF/A‑1b file

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(xmlAttachmentPath))
        {
            Console.Error.WriteLine($"XML attachment not found: {xmlAttachmentPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(sourcePdfPath))
        {
            // Convert to PDF/A‑1b and log conversion messages
            string conversionLog = "conversion_log.txt";
            doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Create a file specification for the XML attachment
            FileSpecification fileSpec = new FileSpecification(xmlAttachmentPath);

            // Choose a page to host the attachment annotation (first page)
            Page page = doc.Pages[1];

            // Define a tiny invisible rectangle for the annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create the file attachment annotation and add it to the page
            // Note: The Icon enum is not available in the current Aspose.PDF version, so we omit it.
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: set a description
                Contents = "External XML data attached."
            };
            page.Annotations.Add(attachment);

            // Save the PDF/A‑1b document with the attachment
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑1b file with XML attachment saved to '{outputPdfPath}'.");
    }
}
