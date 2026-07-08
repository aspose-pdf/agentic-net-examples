using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class ReplaceXfaWithAcroForm
{
    static void Main()
    {
        const string inputPdfPath   = "input_with_xfa.pdf";
        const string xmlTemplatePath = "fields_template.xml";
        const string outputPdfPath  = "output_acroform.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlTemplatePath))
        {
            Console.Error.WriteLine($"XML template not found: {xmlTemplatePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // If the document contains an XFA form, switch it to a standard AcroForm
            if (doc.Form.HasXfa)
            {
                // Changing the form type removes the dynamic XFA part
                doc.Form.Type = FormType.Standard;
            }

            // Load the external XML template that defines the AcroForm fields
            XmlDocument xmlTemplate = new XmlDocument();
            xmlTemplate.Load(xmlTemplatePath);

            // Example XML structure:
            // <fields>
            //   <field name="FirstName" x="100" y="700" width="200" height="20" />
            //   <field name="LastName"  x="100" y="660" width="200" height="20" />
            // </fields>

            XmlNodeList fieldNodes = xmlTemplate.SelectNodes("//field");
            if (fieldNodes == null)
            {
                Console.Error.WriteLine("No <field> elements found in the XML template.");
                return;
            }

            // Create a simple TextBoxField for each <field> node
            foreach (XmlNode node in fieldNodes)
            {
                // Retrieve required attributes; fall back to defaults if missing
                string fieldName = node.Attributes["name"]?.Value ?? Guid.NewGuid().ToString();
                double x = double.TryParse(node.Attributes["x"]?.Value, out double xv) ? xv : 100;
                double y = double.TryParse(node.Attributes["y"]?.Value, out double yv) ? yv : 700;
                double width  = double.TryParse(node.Attributes["width"]?.Value, out double wv) ? wv : 200;
                double height = double.TryParse(node.Attributes["height"]?.Value, out double hv) ? hv : 20;

                // Define the rectangle for the field (lower‑left x,y and upper‑right x+width, y+height)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                // Create a TextBoxField on the first page (adjust as needed)
                TextBoxField txtField = new TextBoxField(doc.Pages[1], rect)
                {
                    PartialName = fieldName,
                    Value = string.Empty // initial empty value
                };

                // Add the field to the document's form collection
                doc.Form.Add(txtField);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"AcroForm PDF saved to '{outputPdfPath}'.");
    }
}