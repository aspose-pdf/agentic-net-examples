using System;
using System.IO;
using System.Xml;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf    = "input_xfa.pdf";
        const string xmlTemplate = "fields_template.xml";
        const string outputPdf   = "output_acro.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlTemplate))
        {
            Console.Error.WriteLine($"XML template not found: {xmlTemplate}");
            return;
        }

        try
        {
            // Load the PDF that contains an XFA form
            using (Document doc = new Document(inputPdf))
            {
                // Replace the XFA form with an AcroForm using field definitions from the XML template
                ReplaceXfaFormWithAcroForm(doc, xmlTemplate);

                // Save the PDF now containing an AcroForm
                doc.Save(outputPdf);
            }

            Console.WriteLine($"AcroForm PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Reads field definitions from an XML template and creates corresponding AcroForm fields.
    /// The method also attempts to clear any existing XFA form data.
    /// Expected XML format (simplified example):
    /// <Fields>
    ///   <Field name="CustomerName" type="TextBox" page="1" x="100" y="700" width="200" height="20" />
    ///   <Field name="Agree" type="CheckBox" page="1" x="100" y="650" width="15" height="15" />
    /// </Fields>
    /// </summary>
    private static void ReplaceXfaFormWithAcroForm(Document doc, string xmlTemplatePath)
    {
        // Load the XML template that describes the form fields
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlTemplatePath);

        // Iterate over each <Field> element and create the appropriate AcroForm field
        foreach (XmlNode fieldNode in xmlDoc.SelectNodes("//Field"))
        {
            string name = fieldNode.Attributes["name"]?.Value;
            if (string.IsNullOrEmpty(name))
                continue; // skip malformed entries

            string type = fieldNode.Attributes["type"]?.Value ?? "TextBox";
            int pageNumber = int.TryParse(fieldNode.Attributes["page"]?.Value, out int p) ? p : 1;
            double x = double.TryParse(fieldNode.Attributes["x"]?.Value, out double xv) ? xv : 0;
            double y = double.TryParse(fieldNode.Attributes["y"]?.Value, out double yv) ? yv : 0;
            double width = double.TryParse(fieldNode.Attributes["width"]?.Value, out double wv) ? wv : 100;
            double height = double.TryParse(fieldNode.Attributes["height"]?.Value, out double hv) ? hv : 20;

            // Ensure the requested page exists
            while (doc.Pages.Count < pageNumber)
                doc.Pages.Add();

            var rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);
            var page = doc.Pages[pageNumber];

            switch (type)
            {
                case "TextBox":
                    var txt = new TextBoxField(page, rect)
                    {
                        PartialName = name
                    };
                    doc.Form.Add(txt, pageNumber);
                    break;

                case "CheckBox":
                    var chk = new CheckboxField(page, rect)
                    {
                        PartialName = name
                    };
                    doc.Form.Add(chk, pageNumber);
                    break;

                case "RadioButton":
                    // Create a radio button group and add a single option using the field name.
                    var rad = new RadioButtonField(page);
                    rad.PartialName = name;
                    rad.AddOption(name, rect);
                    doc.Form.Add(rad, pageNumber);
                    break;

                case "ComboBox":
                    var combo = new ComboBoxField(page, rect)
                    {
                        PartialName = name
                    };
                    doc.Form.Add(combo, pageNumber);
                    break;

                // Add more field types as needed
                default:
                    // Fallback to a generic text box for unknown types
                    var generic = new TextBoxField(page, rect)
                    {
                        PartialName = name
                    };
                    doc.Form.Add(generic, pageNumber);
                    break;
            }
        }

        // Attempt to clear the existing XFA form (if any). The XfaForm property is read‑only in many versions,
        // so we use reflection as a safe fallback.
        try
        {
            var xfaProp = typeof(Document).GetProperty("XfaForm", BindingFlags.Instance | BindingFlags.Public);
            if (xfaProp != null && xfaProp.CanRead)
            {
                var xfaValue = xfaProp.GetValue(doc);
                if (xfaValue != null)
                {
                    // If the XFA object implements IDisposable, dispose it.
                    if (xfaValue is IDisposable disposable)
                        disposable.Dispose();
                    // Try to set the property to null via reflection (works if the setter exists).
                    if (xfaProp.CanWrite)
                        xfaProp.SetValue(doc, null);
                }
            }
        }
        catch
        {
            // Swallow any reflection errors – the PDF will still contain the new AcroForm fields.
        }
    }
}
