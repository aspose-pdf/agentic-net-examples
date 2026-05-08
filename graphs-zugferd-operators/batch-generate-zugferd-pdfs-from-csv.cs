using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths configuration
        const string csvPath = "invoices.csv";          // CSV with invoice data
        const string xmlFolder = "XmlData";             // Folder containing ZUGFeRD XML files
        const string outputFolder = "InvoicesPdf";      // Destination folder for generated PDFs

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each line of the CSV (simple comma‑separated format)
        foreach (string line in File.ReadLines(csvPath))
        {
            // Expected columns: InvoiceNumber,CustomerName,Amount,...
            string[] fields = line.Split(',');
            if (fields.Length == 0) continue;

            string invoiceNumber = fields[0].Trim();
            if (string.IsNullOrEmpty(invoiceNumber)) continue;

            // Locate the corresponding ZUGFeRD XML file
            string xmlPath = Path.Combine(xmlFolder, $"{invoiceNumber}.xml");
            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"XML file not found for invoice {invoiceNumber}");
                continue;
            }

            // Create a new PDF document (using the lifecycle rule for disposal)
            using (Document pdfDoc = new Document())
            {
                // Add a single page
                Page page = pdfDoc.Pages.Add();

                // Insert a simple text fragment with the invoice number
                TextFragment tf = new TextFragment($"Invoice #{invoiceNumber}");
                tf.Position = new Position(100, 700); // Position within the page
                page.Paragraphs.Add(tf);

                // Embed the ZUGFeRD XML as an attachment (core API)
                // Create a FileSpecification with a description and assign its Contents via a stream
                var fileSpec = new FileSpecification($"{invoiceNumber}.xml", $"ZUGFeRD data for invoice {invoiceNumber}");
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(xmlPath));
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Convert to PDF/A‑3U (required for ZUGFeRD compliance)
                pdfDoc.Convert("conversion.log", PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

                // Save the resulting PDF
                string outputPath = Path.Combine(outputFolder, $"{invoiceNumber}.pdf");
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Generated PDF: {outputPath}");
            }
        }
    }
}
