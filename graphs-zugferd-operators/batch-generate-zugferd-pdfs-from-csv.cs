using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths configuration
        const string csvPath = "invoices.csv";          // CSV with invoice data
        const string xmlFolder = "XmlData";             // Folder containing ZUGFeRD XML files
        const string outputFolder = "InvoicesPdf";      // Destination for generated PDFs

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Simple CSV parsing (header + comma‑separated values)
        using (StreamReader reader = new StreamReader(csvPath))
        {
            // Skip header line
            string header = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Expected columns: InvoiceNumber,CustomerName,Amount,XmlFileName
                var fields = line.Split(',');

                if (fields.Length < 4)
                {
                    Console.Error.WriteLine($"Invalid CSV line: {line}");
                    continue;
                }

                string invoiceNumber = fields[0].Trim();
                string customerName = fields[1].Trim();
                string amount = fields[2].Trim();
                string xmlFileName = fields[3].Trim();

                string xmlPath = Path.Combine(xmlFolder, xmlFileName);
                if (!File.Exists(xmlPath))
                {
                    Console.Error.WriteLine($"XML file not found for invoice {invoiceNumber}: {xmlPath}");
                    continue;
                }

                string pdfPath = Path.Combine(outputFolder, $"{invoiceNumber}.pdf");

                // Create a new PDF document and add invoice details
                using (Document pdfDoc = new Document())
                {
                    var page = pdfDoc.Pages.Add();

                    // Invoice number
                    Aspose.Pdf.Text.TextFragment tfInvoice = new Aspose.Pdf.Text.TextFragment($"Invoice #: {invoiceNumber}")
                    {
                        Position = new Aspose.Pdf.Text.Position(50, 750),
                        TextState = { FontSize = 14, Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica") }
                    };
                    page.Paragraphs.Add(tfInvoice);

                    // Customer name
                    Aspose.Pdf.Text.TextFragment tfCustomer = new Aspose.Pdf.Text.TextFragment($"Customer: {customerName}")
                    {
                        Position = new Aspose.Pdf.Text.Position(50, 720),
                        TextState = { FontSize = 12 }
                    };
                    page.Paragraphs.Add(tfCustomer);

                    // Amount
                    Aspose.Pdf.Text.TextFragment tfAmount = new Aspose.Pdf.Text.TextFragment($"Amount: {amount}")
                    {
                        Position = new Aspose.Pdf.Text.Position(50, 690),
                        TextState = { FontSize = 12 }
                    };
                    page.Paragraphs.Add(tfAmount);

                    // Attach ZUGFeRD XML data to the PDF
                    pdfDoc.BindXml(xmlPath);

                    // Save the PDF (ZUGFeRD‑compliant)
                    pdfDoc.Save(pdfPath);
                }

                Console.WriteLine($"Generated PDF: {pdfPath}");
            }
        }
    }
}