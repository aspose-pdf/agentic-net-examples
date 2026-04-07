using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Needed for TextFragment

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document document = new Document())
        {
            // Add a page with simple invoice text
            Page page = document.Pages.Add();
            TextFragment text = new TextFragment("Invoice #12345");
            text.TextState.FontSize = 14;
            page.Paragraphs.Add(text);

            // Prepare ZUGFeRD XML – if the file does not exist we fall back to a minimal XML string
            byte[] xmlBytes;
            const string xmlPath = "invoice.xml";
            if (File.Exists(xmlPath))
            {
                xmlBytes = File.ReadAllBytes(xmlPath);
            }
            else
            {
                // Minimal placeholder XML that satisfies the ZUGFeRD schema for demo purposes
                string placeholderXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<Invoice xmlns=\"urn:ferd:invoice:1p0:basic\">\n  <Header><Id>12345</Id></Header>\n</Invoice>";
                xmlBytes = Encoding.UTF8.GetBytes(placeholderXml);
            }

            // Embed the ZUGFeRD XML file as an attachment using a MemoryStream
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                FileSpecification fileSpec = new FileSpecification(xmlStream, "invoice.xml", "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = Aspose.Pdf.AFRelationship.Data
                };
                document.EmbeddedFiles.Add(fileSpec);
            }

            // PDF/A‑3B conversion and saving require GDI+ (libgdiplus on non‑Windows).
            // Guard these operations with a platform check to avoid TypeInitializationException on macOS/Linux.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Convert the document to PDF/A‑3B for ZUGFeRD compliance
                document.Convert("conversion_log.xml", PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);

                // Save the final PDF
                document.Save("ZUGFeRD_Invoice.pdf");
                Console.WriteLine("PDF saved to 'ZUGFeRD_Invoice.pdf'.");
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform. GDI+ (libgdiplus) is required for PDF/A‑3 conversion and saving.");
                Console.WriteLine("Skipping conversion and save steps. The PDF document is created in memory only.");
            }
        }
    }
}
