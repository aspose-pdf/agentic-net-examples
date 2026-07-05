using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string xmlFile   = "attachment.xml";
        const string outputPdf = "output_pdfa.pdf";

        if (!File.Exists(sourcePdf) || !File.Exists(xmlFile))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(sourcePdf))
        {
            // Convert to PDF/A‑1b (log file is temporary)
            string logPath = Path.GetTempFileName();
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Add a file attachment annotation on the first page
            Page page = doc.Pages[1];

            // Rectangle defining the annotation icon position
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // File specification for the external XML file
            FileSpecification fileSpec = new FileSpecification(xmlFile);

            // Create the attachment annotation
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Icon property expects a FileIcon enum value
                Icon = FileIcon.Graph,
                Title = "External XML",
                Contents = "Attached XML data"
            };

            // Add the annotation to the page
            page.Annotations.Add(attachment);

            // Save the PDF/A‑1b document with the attachment
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑1b document saved with XML attachment: {outputPdf}");
    }
}
