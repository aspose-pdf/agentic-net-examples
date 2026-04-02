using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace ExtractTextFromXmlPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input XML file path (replace with your actual file)
            string xmlPath = "sample.xml";
            // Optional: path to save the generated PDF for verification
            string outputPdfPath = "generated.pdf";

            // Resolve the XML path to an absolute file system path.
            string absoluteXmlPath = Path.GetFullPath(xmlPath);

            // If the XML file does not exist, create a minimal XML document in memory.
            // This prevents a FileNotFoundException at runtime and still demonstrates the workflow.
            MemoryStream xmlStream;
            if (File.Exists(absoluteXmlPath))
            {
                // Load the existing file into a stream.
                byte[] xmlBytes = File.ReadAllBytes(absoluteXmlPath);
                xmlStream = new MemoryStream(xmlBytes);
            }
            else
            {
                // Create a simple XML that Aspose.Pdf can bind to.
                const string sampleXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<document>\n    <section>\n        <paragraph>Hello, world! This is a generated PDF from XML.</paragraph>\n    </section>\n</document>";
                byte[] xmlBytes = Encoding.UTF8.GetBytes(sampleXml);
                xmlStream = new MemoryStream(xmlBytes);
                Console.WriteLine($"[Info] XML file not found at '{absoluteXmlPath}'. Using in‑memory sample XML.");
            }

            // Create a new PDF document and bind the XML content.
            using (Document pdfDocument = new Document())
            {
                // Use the stream overload of BindXml – it works with both file‑based and in‑memory XML.
                pdfDocument.BindXml(xmlStream);

                // Save the PDF (optional – useful for visual verification)
                pdfDocument.Save(outputPdfPath);

                // Set extraction options to get plain (raw) text.
                TextExtractionOptions extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Raw);
                TextAbsorber absorber = new TextAbsorber(extractionOptions);

                // Extract text from all pages.
                pdfDocument.Pages.Accept(absorber);
                string extractedText = absorber.Text;

                // Output the extracted text (e.g., to console or further processing).
                Console.WriteLine("Extracted Text:");
                Console.WriteLine(extractedText);
            }
        }
    }
}
