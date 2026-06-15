using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Step 1: Create a simple PDF document (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("sample.pdf");
        }

        // Step 2: Load the PDF, embed dummy ZUGFeRD XML, and convert to PDF/A‑3b
        using (Document doc = new Document("sample.pdf"))
        {
            // Create dummy ZUGFeRD XML content
            string xmlContent = "<Invoice><ID>12345</ID></Invoice>";
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                // Create a file specification for the XML and embed it
                FileSpecification fileSpec = new FileSpecification(xmlStream, "invoice.xml");
                fileSpec.Description = "ZUGFeRD invoice data";
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Convert the document to PDF/A‑3b (required for ZUGFeRD)
            string conversionLogPath = "conversion.log";
            doc.Convert(conversionLogPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
            doc.Save("sample_zugferd.pdf");

            Console.WriteLine("PDF/A‑3b conversion completed and ZUGFeRD XML embedded successfully.");
        }
    }
}