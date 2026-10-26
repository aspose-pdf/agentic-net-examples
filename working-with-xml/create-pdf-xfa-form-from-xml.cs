using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath = "formDefinition.xml";   // XML describing the fields
        const string outputPdf = "outputForm.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML definition not found: {xmlPath}");
            return;
        }

        // Load the XML that defines the form fields
        XDocument xDoc = XDocument.Load(xmlPath);
        var fieldElements = xDoc.Root?.Elements("Field");

        // Create a new PDF document (blank) and ensure proper disposal
        using (Document pdfDoc = new Document())
        {
            // Ensure at least one page exists (Aspose.Pdf uses 1‑based page indexing)
            if (pdfDoc.Pages.Count == 0)
                pdfDoc.Pages.Add();

            if (fieldElements != null)
            {
                foreach (var fld in fieldElements)
                {
                    string type = (string)fld.Attribute("type");
                    string name = (string)fld.Attribute("name");
                    string value = (string)fld.Attribute("value") ?? string.Empty;
                    int page = (int?)fld.Attribute("page") ?? 1;

                    double llx = double.Parse((string)fld.Attribute("llx"));
                    double lly = double.Parse((string)fld.Attribute("lly"));
                    double urx = double.Parse((string)fld.Attribute("urx"));
                    double ury = double.Parse((string)fld.Attribute("ury"));

                    // Fully qualified rectangle to avoid ambiguity
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                    // Resolve the page object (Aspose.Pdf pages are 1‑based)
                    Page pdfPage = pdfDoc.Pages[page];

                    switch (type?.Trim().ToLowerInvariant())
                    {
                        case "textbox":
                            // TextBoxField ctor: (Page, Rectangle)
                            TextBoxField txtField = new TextBoxField(pdfPage, rect);
                            txtField.PartialName = name;
                            txtField.Value = value;               // set default value
                            pdfDoc.Form.Add(txtField);
                            break;

                        case "checkbox":
                            // CheckboxField ctor: (Page, Rectangle) – note the lowercase 'b'
                            CheckboxField chkField = new CheckboxField(pdfPage, rect);
                            chkField.PartialName = name;
                            // Use dynamic to set the Checked property (available on CheckboxField)
                            dynamic dynChk = chkField;
                            dynChk.Checked = value.Equals("true", StringComparison.OrdinalIgnoreCase);
                            pdfDoc.Form.Add(chkField);
                            break;

                        case "radiobutton":
                            // RadioButtonOptionField ctor: (Page, Rectangle)
                            RadioButtonOptionField rbField = new RadioButtonOptionField(pdfPage, rect);
                            rbField.PartialName = name;
                            dynamic dynRb = rbField;
                            dynRb.Checked = value.Equals("true", StringComparison.OrdinalIgnoreCase);
                            pdfDoc.Form.Add(rbField);
                            break;

                        case "listbox":
                            // ListBoxField ctor: (Page, Rectangle)
                            ListBoxField listField = new ListBoxField(pdfPage, rect);
                            listField.PartialName = name;
                            // Populate options from child <Item> elements
                            foreach (var item in fld.Elements("Item"))
                            {
                                string itemValue = (string)item.Attribute("value") ?? string.Empty;
                                // AddOption is the correct API for ChoiceField derived types
                                listField.AddOption(itemValue);
                            }
                            // Set the default selected value (single‑select list)
                            listField.Value = value;
                            pdfDoc.Form.Add(listField);
                            break;

                        case "combobox":
                            // ComboBoxField ctor: (Page, Rectangle)
                            ComboBoxField comboField = new ComboBoxField(pdfPage, rect);
                            comboField.PartialName = name;
                            foreach (var item in fld.Elements("Item"))
                            {
                                string itemValue = (string)item.Attribute("value") ?? string.Empty;
                                comboField.AddOption(itemValue);
                            }
                            comboField.Value = value; // pre‑select
                            pdfDoc.Form.Add(comboField);
                            break;

                        default:
                            Console.WriteLine($"Unsupported field type: {type}");
                            break;
                    }
                }
            }

            // Save the PDF (inside the using block as per lifecycle rule)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Form PDF created: {outputPdf}");
    }
}
