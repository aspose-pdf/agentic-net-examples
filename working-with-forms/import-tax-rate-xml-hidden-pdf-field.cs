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
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load tax rate from the XML file
        string taxRate = string.Empty;
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            // Expected XML format: <TaxRates><Rate>0.07</Rate></TaxRates>
            XmlNode rateNode = xmlDoc.SelectSingleNode("//Rate");
            if (rateNode != null)
                taxRate = rateNode.InnerText.Trim();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XML: {ex.Message}");
            return;
        }

        // Load PDF, set (or create) hidden field, and save
        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Try to find an existing field named "TaxRate"
                if (doc.Form != null && doc.Form["TaxRate"] != null)
                {
                    // The indexer returns a Field; cast to TextBoxField to access the Value property
                    if (doc.Form["TaxRate"] is TextBoxField existingField)
                    {
                        existingField.Value = taxRate;
                    }
                }
                else
                {
                    // Create a new hidden text box field on the first page
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                    TextBoxField hiddenField = new TextBoxField(doc.Pages[1], rect)
                    {
                        PartialName = "TaxRate",
                        Value = taxRate
                        // No Export property in current API; using a zero‑size rectangle makes the field effectively hidden
                    };
                    doc.Form.Add(hiddenField);
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with tax rate '{taxRate}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
