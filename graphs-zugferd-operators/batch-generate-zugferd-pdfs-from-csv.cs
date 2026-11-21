using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // needed for AFRelationship enum

class ZugferdBatchGenerator
{
    static void Main()
    {
        const string csvPath = "invoices.csv";          // CSV source
        const string outputFolder = "GeneratedInvoices"; // Output folder for PDFs

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Read all lines, skip header
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV contains no data rows.");
            return;
        }

        // Expected CSV columns: InvoiceNumber,Date,CustomerName,TotalAmount,XmlFilePath
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');

            if (parts.Length < 5)
            {
                Console.Error.WriteLine($"Invalid CSV format at line {i + 1}");
                continue;
            }

            string invoiceNumber = parts[0].Trim();
            string invoiceDate   = parts[1].Trim();
            string customerName  = parts[2].Trim();
            string totalAmount   = parts[3].Trim();
            string xmlPath       = parts[4].Trim();

            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"XML file not found for invoice {invoiceNumber}: {xmlPath}");
                continue;
            }

            string pdfFileName = $"Invoice_{invoiceNumber}.pdf";
            string pdfPath = Path.Combine(outputFolder, pdfFileName);
            string logPath = Path.Combine(outputFolder, $"convert_{invoiceNumber}.log");

            // Create PDF/A-3 (ZUGFeRD) document
            using (Document pdfDoc = new Document())
            {
                // Add a single page
                Page page = pdfDoc.Pages.Add();

                // Build simple invoice content
                TextFragment tf = new TextFragment($"Invoice #: {invoiceNumber}\nDate: {invoiceDate}\nCustomer: {customerName}\nTotal: {totalAmount}");
                tf.Position = new Position(50, 750);
                tf.TextState.FontSize = 12;
                page.Paragraphs.Add(tf);

                // Embed the ZUGFeRD XML as an embedded file (AFRelationship.Data)
                var xmlSpec = new FileSpecification(xmlPath, "ZUGFeRD Invoice XML")
                {
                    MIMEType = "application/xml",
                    AFRelationship = AFRelationship.Data,
                    Contents = new MemoryStream(File.ReadAllBytes(xmlPath))
                };
                pdfDoc.EmbeddedFiles.Add(xmlSpec);

                // Convert to PDF/A-3B (required for ZUGFeRD)
                // Use the overload that accepts a log file path and an error‑handling action.
                bool conversionResult = pdfDoc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
                if (!conversionResult)
                {
                    Console.Error.WriteLine($"PDF/A conversion failed for invoice {invoiceNumber}. See log: {logPath}");
                }

                // Save the resulting PDF
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"Generated ZUGFeRD PDF: {pdfPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
