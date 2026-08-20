using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";
        const string xmlPath = "taxrates.xml";
        const string outputPath = "invoice_with_tax.pdf";
        const string hiddenFieldName = "TaxRateHidden";

        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Required input files are missing.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the XML file and extract the tax rate value
            decimal taxRate = 0m;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Example XML structure: <TaxRates><Rate>0.07</Rate></TaxRates>
            XmlNode rateNode = xmlDoc.SelectSingleNode("//Rate");
            if (rateNode != null && decimal.TryParse(rateNode.InnerText, out decimal parsedRate))
            {
                taxRate = parsedRate;
            }

            // Retrieve the hidden form field safely – the Form indexer returns a WidgetAnnotation,
            // so we cast it to Aspose.Pdf.Forms.Field before accessing field‑specific members.
            Field? hiddenField = pdfDoc.Form[hiddenFieldName] as Field;
            if (hiddenField != null)
            {
                hiddenField.Value = taxRate.ToString("0.##");
            }
            else
            {
                Console.Error.WriteLine($"Hidden field '{hiddenFieldName}' not found in the PDF.");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported tax rate saved to '{outputPath}'.");
    }
}
