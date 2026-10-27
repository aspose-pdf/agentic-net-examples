using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";
        const string xmlPath = "tax_output.xml";
        const double taxRate = 0.07; // 7% tax rate

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF form not found: {pdfPath}");
            return;
        }

        // Load the PDF form
        using (Document doc = new Document(pdfPath))
        {
            // ----- Read the "Amount" field -----
            // The Form indexer returns a Field (Aspose.Pdf.Forms.Field). Use its Value property.
            Field amountField = doc.Form["Amount"] as Field;
            string amountStr = amountField?.Value?.ToString() ?? "0";
            if (!double.TryParse(amountStr, out double amount))
            {
                Console.Error.WriteLine($"Invalid amount value: {amountStr}");
                amount = 0;
            }

            // ----- Calculate tax -----
            double tax = Math.Round(amount * taxRate, 2);

            // ----- Write the tax back to a field named "Tax" (if it exists) -----
            if (doc.Form["Tax"] is Field taxField)
            {
                taxField.Value = tax.ToString("F2");
            }

            // ----- Build XML containing the calculation result -----
            XDocument xmlDoc = new XDocument(
                new XElement("TaxResult",
                    new XElement("Amount", amount.ToString("F2")),
                    new XElement("TaxRate", taxRate.ToString("P")),
                    new XElement("Tax", tax.ToString("F2"))
                )
            );

            // Save the XML file
            xmlDoc.Save(xmlPath);
        }

        Console.WriteLine($"Tax XML saved to '{xmlPath}'.");
    }
}
