using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";   // source PDF with form fields
        const string xmlPath    = "data.xml";    // XML containing a subset of field values
        const string outputPath = "output.pdf";  // destination PDF

        // Validate input files
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

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(pdfPath))
        {
            // Load the XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Iterate over all form fields in the PDF
            foreach (Field field in doc.Form.Fields)
            {
                // Try to locate an XML element whose name matches the field name.
                // Field.FullName includes hierarchical names; fallback to field.Name if FullName is null.
                string fieldName = field.FullName ?? field.Name;

                // XPath selects the first matching element anywhere in the XML.
                XmlNode node = xmlDoc.SelectSingleNode($"//{fieldName}");
                if (node != null)
                {
                    // Update the field value with the XML element's inner text.
                    field.Value = node.InnerText;
                }
            }

            // Save the updated PDF (lifecycle rule: Save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}