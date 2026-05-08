using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "taxrates.xml";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Required input files are missing.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // The Form object is always instantiated for a Document; no need to create it manually.

            // Load and parse the XML file containing tax rates
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Example XML structure:
            // <TaxRates>
            //     <Rate>0.07</Rate>
            // </TaxRates>
            XmlNode rateNode = xmlDoc.SelectSingleNode("//TaxRates/Rate");
            string taxRate = rateNode?.InnerText ?? "0";

            // Attempt to locate an existing hidden field named "TaxRateHidden"
            var existingField = doc.Form["TaxRateHidden"] as TextBoxField;
            if (existingField != null)
            {
                // Update the field's value with the tax rate from XML
                existingField.Value = taxRate;
            }
            else
            {
                // Create a new hidden text box field on the first page.
                // A rectangle with zero size makes the field invisible in the layout.
                var hiddenField = new TextBoxField(doc.Pages[1], new Aspose.Pdf.Rectangle(0, 0, 0, 0))
                {
                    PartialName = "TaxRateHidden",
                    Value = taxRate
                };
                doc.Form.Add(hiddenField);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported tax rate saved to '{outputPath}'.");
    }
}
