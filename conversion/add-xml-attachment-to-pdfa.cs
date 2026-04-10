using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlFile = "data.xml";
        const string outputPdf = "output_pdfa.pdf";
        const string conversionLog = "conversion.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Convert to PDF/A‑1b; log conversion errors
                doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Create a file specification for the XML attachment (the file will be embedded automatically)
                var fileSpec = new FileSpecification(xmlFile, "External XML data");

                // Define a rectangle for the attachment annotation on the first page
                // Aspose.Pdf.Rectangle expects (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

                // Create the file attachment annotation
                Page firstPage = doc.Pages[1];
                var attachment = new FileAttachmentAnnotation(firstPage, rect, fileSpec)
                {
                    Title = "XML Attachment",
                    Contents = "Attached XML file"
                };

                // Add the annotation to the page
                firstPage.Annotations.Add(attachment);

                // Save the PDF/A‑1b document with the attachment
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A‑1b file with XML attachment saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
