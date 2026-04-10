using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string csvPath = "invoices.csv";
        const string outputDirectory = "Invoices";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        foreach (string line in File.ReadLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue; // skip empty lines

            // Expected CSV columns: InvoiceNumber,CustomerName,Amount,XmlFilePath
            string[] fields = line.Split(',');
            if (fields.Length < 4)
            {
                Console.Error.WriteLine($"Invalid CSV line (expected 4 columns): {line}");
                continue;
            }

            string invoiceNumber = fields[0].Trim();
            string customerName  = fields[1].Trim();
            string amount        = fields[2].Trim();
            string xmlPath       = fields[3].Trim();

            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"XML file not found for invoice {invoiceNumber}: {xmlPath}");
                continue;
            }

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a page
                Page page = pdfDoc.Pages.Add();

                // Add simple invoice text
                TextFragment fragment = new TextFragment(
                    $"Invoice: {invoiceNumber}\n" +
                    $"Customer: {customerName}\n" +
                    $"Amount: {amount}"
                );
                fragment.TextState.FontSize = 12;
                fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                page.Paragraphs.Add(fragment);

                // Attach the ZUGFeRD XML data
                pdfDoc.BindXml(xmlPath);

                // Save the PDF
                string outputPath = Path.Combine(outputDirectory, $"{invoiceNumber}.pdf");
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Generated PDF: {outputPath}");
            }
        }
    }
}