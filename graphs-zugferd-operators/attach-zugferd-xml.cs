using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class Program
{
    public static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // 1. Create a sample PDF document (self‑contained example)
        // ------------------------------------------------------------
        using (Document samplePdf = new Document())
        {
            // Add a single blank page – evaluation mode allows up to 4 pages
            samplePdf.Pages.Add();
            samplePdf.Save("sample.pdf");
        }

        // ------------------------------------------------------------
        // 2. Create a simple ZUGFeRD XML file on disk
        // ------------------------------------------------------------
        string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Invoice><ID>12345</ID></Invoice>";
        File.WriteAllText("invoice.xml", xmlContent);

        // ------------------------------------------------------------
        // 3. Open the PDF and attach the XML as a file‑attachment annotation
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document("sample.pdf"))
        {
            // Create a file specification for the XML file.
            // Use the overload that accepts only the file path (no Document argument).
            FileSpecification fileSpec = new FileSpecification("invoice.xml");
            // Embed the file contents – required for proper attachment in PDF/A/ZUGFeRD.
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes("invoice.xml"));

            // Define the rectangle where the annotation icon will appear.
            // The Rectangle class expects float values; use explicit casts if necessary.
            Rectangle rect = new Rectangle(100f, 500f, 120f, 520f);

            // Get the first page (1‑based indexing as required by the rules).
            Page page = pdfDoc.Pages[1];

            // Create the file‑attachment annotation.
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
            attachment.Name = "ZUGFeRD Invoice";
            attachment.Contents = "Embedded ZUGFeRD XML invoice";

            // Add the annotation to the page.
            page.Annotations.Add(attachment);

            // Save the resulting PDF.
            pdfDoc.Save("output.pdf");
        }
    }
}
