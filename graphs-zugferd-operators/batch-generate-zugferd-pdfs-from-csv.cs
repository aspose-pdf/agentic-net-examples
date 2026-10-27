using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string csvPath = "invoices.csv";          // CSV with invoice data
        const string outputDir = "GeneratedInvoices";   // Folder for PDFs

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Expected CSV columns: InvoiceNumber,CustomerName,Amount,XmlFilePath
        foreach (var line in File.ReadLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                Console.Error.WriteLine($"Invalid line (expected 4 columns): {line}");
                continue;
            }

            string invoiceNumber = parts[0].Trim();
            string customerName  = parts[1].Trim();
            string amountStr     = parts[2].Trim();
            string xmlPath       = parts[3].Trim();

            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"XML file not found for invoice {invoiceNumber}: {xmlPath}");
                continue;
            }

            string outputPdfPath = Path.Combine(outputDir, $"Invoice_{invoiceNumber}.pdf");

            // Create PDF, add simple invoice content
            using (Document doc = new Document())
            {
                // Add a page
                Page page = doc.Pages.Add();

                // Build invoice text
                TextFragment tf = new TextFragment($"Invoice Number: {invoiceNumber}\nCustomer: {customerName}\nAmount: {amountStr}");
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Arial");
                page.Paragraphs.Add(tf);

                // Convert to PDF/A-3U (required for ZUGFeRD)
                // Simple overload is sufficient; no need for PUA strategy
                doc.Convert("conversion_log.xml", PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

                // Embed the ZUGFeRD XML file
                FileSpecification xmlFileSpec = new FileSpecification(xmlPath);
                doc.EmbeddedFiles.Add(xmlFileSpec);

                // Add ZUGFeRD-specific metadata (minimal set)
                doc.Info["ZUGFeRDVersion"] = "1.0";
                doc.Info["DocumentType"]   = "INVOICE";
                doc.Info["Profile"]        = "ZUGFeRD";
                doc.Info["CreatorTool"]    = "Aspose.Pdf";

                // Save the PDF/A-3U document (extension .pdf is fine)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Generated ZUGFeRD PDF: {outputPdfPath}");
        }
    }
}