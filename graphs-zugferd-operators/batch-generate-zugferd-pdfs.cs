using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a simple CSV file with invoice data
        string csvPath = "invoices.csv";
        using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("InvoiceNumber,CustomerName,Amount,XmlFile");
            writer.WriteLine("INV001,Acme Corp,1000,inv001.xml");
            writer.WriteLine("INV002,Globex Inc,2500,inv002.xml");
            writer.WriteLine("INV003,Initech,1500,inv003.xml");
        }

        // Create sample ZUGFeRD XML files referenced in the CSV
        CreateSampleXml("inv001.xml", "INV001");
        CreateSampleXml("inv002.xml", "INV002");
        CreateSampleXml("inv003.xml", "INV003");

        // Build a minimal PDF template that will be reused for each invoice
        string templatePath = "template.pdf";
        using (Document templateDoc = new Document())
        {
            Page page = templateDoc.Pages.Add();
            TextFragment placeholder = new TextFragment("Invoice Placeholder");
            placeholder.Position = new Position(100, 700);
            page.Paragraphs.Add(placeholder);
            templateDoc.Save(templatePath);
        }

        // Read the CSV and generate a PDF for each record, embedding the matching XML
        using (StreamReader reader = new StreamReader(csvPath))
        {
            // Skip header line
            string header = reader.ReadLine();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length < 4)
                {
                    continue;
                }
                string invoiceNumber = parts[0];
                string xmlFileName = parts[3];

                // Load the template PDF
                using (Document pdfDoc = new Document(templatePath))
                {
                    // Add invoice number text to the first page
                    Page firstPage = pdfDoc.Pages[1];
                    TextFragment invoiceText = new TextFragment("Invoice: " + invoiceNumber);
                    invoiceText.Position = new Position(100, 650);
                    firstPage.Paragraphs.Add(invoiceText);

                    // Embed the corresponding XML file as an embedded file
                    byte[] xmlBytes = File.ReadAllBytes(xmlFileName);
                    FileSpecification fileSpec = new FileSpecification(Path.GetFileName(xmlFileName), "ZUGFeRD XML data");
                    fileSpec.Contents = new MemoryStream(xmlBytes);
                    pdfDoc.EmbeddedFiles.Add(fileSpec);

                    // Save the resulting PDF using the invoice number as the file name
                    string outputPdf = invoiceNumber + ".pdf";
                    pdfDoc.Save(outputPdf);
                }
            }
        }
    }

    static void CreateSampleXml(string fileName, string invoiceNumber)
    {
        string xmlContent = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><Invoice><Number>{invoiceNumber}</Number></Invoice>";
        File.WriteAllText(fileName, xmlContent, Encoding.UTF8);
    }
}
