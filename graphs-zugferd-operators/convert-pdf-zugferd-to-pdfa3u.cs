using System;
using System.IO;
using Aspose.Pdf;

namespace ConvertZugferdToPdfA3u
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF file.
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Create a simple ZUGFeRD XML file.
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Invoice><ID>12345</ID></Invoice>";
            File.WriteAllText("invoice.xml", xmlContent);

            // Step 3: Open the PDF, attach the XML, and convert to PDF/A-3u.
            using (Document doc = new Document("input.pdf"))
            {
                // Attach the XML file.
                FileSpecification attachment = new FileSpecification("invoice.xml", "ZUGFeRD Invoice XML");
                doc.EmbeddedFiles.Add(attachment);

                // Convert to PDF/A-3u.
                string logFile = "convert.log";
                doc.Convert(logFile, PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

                // Save the PDF/A-3u document.
                doc.Save("output.pdf");
            }
        }
    }
}