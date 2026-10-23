using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "invoice.pdf";
        const string xmlPath   = "invoice.xml";

        // Verify that the ZUGFeRD XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page for the readable invoice content
            Page page = doc.Pages.Add();

            // Create a simple text fragment with invoice details
            TextFragment tf = new TextFragment(
                "Invoice #12345\nDate: 2023-10-07\nTotal: $1,234.56");
            tf.Position = new Position(50, 750);
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Color.Black;
            page.Paragraphs.Add(tf);

            // Read the XML data into a memory stream
            byte[] xmlBytes = File.ReadAllBytes(xmlPath);
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                // Create a file specification for the embedded XML
                // Use the constructor that takes a file name and a description,
                // then assign the file contents via the Contents property.
                FileSpecification fileSpec = new FileSpecification("invoice.xml", "ZUGFeRD XML invoice");
                fileSpec.Contents = xmlStream; // embed the XML bytes

                // Add the file specification to the document's embedded files collection
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF (plain PDF; for full ZUGFeRD compliance PDF/A‑3 would be required)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Invoice PDF created: {outputPdf}");
    }
}
