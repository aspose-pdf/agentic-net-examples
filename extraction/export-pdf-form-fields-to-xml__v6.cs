using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Form field types are in this namespace

class Program
{
    static void Main()
    {
        const string pdfPath = "FormE.pdf";
        const string xmlPath = "FormE_fields.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(pdfPath))
        {
            // Check that the PDF actually contains form fields
            if (doc.Form == null || doc.Form.Fields.Length == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Prepare an XML writer with indentation for readability
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
            using (XmlWriter writer = XmlWriter.Create(xmlPath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FormFields");

                // Enumerate each field in the form
                foreach (Field field in doc.Form.Fields)
                {
                    writer.WriteStartElement("Field");
                    writer.WriteAttributeString("Name", field.FullName ?? string.Empty);
                    writer.WriteAttributeString("Type", field.GetType().Name);

                    // Extract the field's value based on its concrete type
                    string value = string.Empty;
                    switch (field)
                    {
                        case TextBoxField txt:
                            value = txt.Value;
                            break;
                        case CheckboxField chk:
                            value = chk.Checked ? "true" : "false";
                            break;
                        case RadioButtonField rad:
                            // Selected is an int (1‑based). If > 0 a value is chosen.
                            value = rad.Selected > 0 ? rad.Value : string.Empty;
                            break;
                        case ListBoxField list:
                            // SelectedItems returns a 1‑based int[] of selected indices.
                            if (list.SelectedItems != null && list.SelectedItems.Length > 0)
                            {
                                var selected = new List<string>();
                                foreach (int idx in list.SelectedItems)
                                {
                                    // Options collection is 1‑based.
                                    if (idx >= 1 && idx <= list.Options.Count)
                                    {
                                        Option opt = list.Options[idx];
                                        // Use the export value (Option.Value). If you prefer display text, use opt.Name.
                                        selected.Add(opt.Value);
                                    }
                                }
                                value = string.Join(",", selected);
                            }
                            break;
                        case ComboBoxField combo:
                            value = combo.Value;
                            break;
                        default:
                            // Fallback for any other field types that expose a Value property
                            value = field.Value?.ToString() ?? string.Empty;
                            break;
                    }

                    writer.WriteElementString("Value", value);
                    writer.WriteEndElement(); // </Field>
                }

                writer.WriteEndElement(); // </FormFields>
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Form field data exported to '{xmlPath}'.");
        }
    }
}
