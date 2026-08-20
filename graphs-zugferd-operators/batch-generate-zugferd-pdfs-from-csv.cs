using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Simple DTO for CSV rows
    private class InvoiceRecord
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string Amount { get; set; }
        public string XmlPath { get; set; }
    }

    static void Main()
    {
        const string csvPath = "invoices.csv";          // CSV with invoice data
        const string outputFolder = "GeneratedInvoices"; // Folder for PDFs

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        List<InvoiceRecord> records = ParseCsv(csvPath);
        foreach (var rec in records)
        {
            // Validate XML file existence
            if (!File.Exists(rec.XmlPath))
            {
                Console.Error.WriteLine($"XML file not found for invoice {rec.InvoiceNumber}: {rec.XmlPath}");
                continue;
            }

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a page
                Page page = pdfDoc.Pages.Add();

                // Prepare invoice text
                string invoiceText = $"Invoice #: {rec.InvoiceNumber}\n" +
                                     $"Customer: {rec.CustomerName}\n" +
                                     $"Amount: {rec.Amount}";

                // Add text to the page
                TextFragment tf = new TextFragment(invoiceText);
                tf.Position = new Position(100, 700); // Position near top-left
                page.Paragraphs.Add(tf);

                // Attach ZUGFeRD XML data to the PDF
                pdfDoc.BindXml(rec.XmlPath);

                // Save the PDF with a name based on the invoice number
                string outputPdfPath = Path.Combine(outputFolder, $"{rec.InvoiceNumber}.pdf");
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"Generated PDF: {outputPdfPath}");
            }
        }
    }

    // Parses a CSV file where each line is:
    // InvoiceNumber,CustomerName,Amount,XmlFilePath
    private static List<InvoiceRecord> ParseCsv(string csvFilePath)
    {
        var list = new List<InvoiceRecord>();
        foreach (var line in File.ReadLines(csvFilePath))
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Simple split by comma; assumes no commas inside fields
            var parts = line.Split(',');
            if (parts.Length < 4)
                continue; // malformed line

            InvoiceRecord record = new InvoiceRecord {
                InvoiceNumber = parts[0].Trim(),
                CustomerName = parts[1].Trim(),
                Amount = parts[2].Trim(),
                XmlPath = parts[3].Trim()
            };
            list.Add(record);
        }
        return list;
    }
}