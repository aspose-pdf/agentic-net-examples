using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string pdfPath = "input_form.pdf";
        const string xmlOutputPath = "tax_output.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document containing the form
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Retrieve the submitted amount from a form field named "Amount"
            // Use the Forms API – the indexer returns a Field object that has a Value property
            Field amountField = pdfDoc.Form["Amount"] as Field;
            string amountText = amountField?.Value?.ToString() ?? "0";

            // Parse the amount; fallback to 0 on parse failure
            if (!decimal.TryParse(amountText, out decimal amount))
                amount = 0m;

            // Example tax calculation: 10% of the amount
            decimal taxValue = amount * 0.10m;

            // Build the XML document with the calculated tax
            XDocument taxXml = new XDocument(
                new XElement("TaxInfo",
                    new XElement("OriginalAmount", amount),
                    new XElement("TaxValue", taxValue)
                )
            );

            // Save the XML to the specified path
            taxXml.Save(xmlOutputPath);
            Console.WriteLine($"Tax XML saved to '{xmlOutputPath}'.");
        }
    }
}
