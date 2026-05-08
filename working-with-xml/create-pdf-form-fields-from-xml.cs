using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "fields.xml";          // XML that defines the form fields
        const string outputPdf = "output_form.pdf";   // Resulting PDF with fields

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML definition not found: {xmlPath}");
            return;
        }

        // Load the XML document that contains field definitions
        XmlDocument fieldDef = new XmlDocument();
        fieldDef.Load(xmlPath);

        // Create a new PDF document with a single blank page (you can load an existing template instead)
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add(); // page index will be 1 (Aspose.Pdf uses 1‑based indexing)

            // Use FormEditor (Facade) to add fields according to the XML definition
            using (FormEditor editor = new FormEditor(pdfDoc))
            {
                XmlNodeList nodes = fieldDef.SelectNodes("//Field");
                foreach (XmlNode node in nodes)
                {
                    // Required attributes
                    string name      = node.Attributes["name"]?.Value;
                    string typeStr   = node.Attributes["type"]?.Value;
                    string pageStr   = node.Attributes["page"]?.Value;
                    string llxStr    = node.Attributes["llx"]?.Value;
                    string llyStr    = node.Attributes["lly"]?.Value;
                    string urxStr    = node.Attributes["urx"]?.Value;
                    string uryStr    = node.Attributes["ury"]?.Value;

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(typeStr) ||
                        string.IsNullOrEmpty(pageStr) || string.IsNullOrEmpty(llxStr) ||
                        string.IsNullOrEmpty(llyStr) || string.IsNullOrEmpty(urxStr) ||
                        string.IsNullOrEmpty(uryStr))
                    {
                        Console.Error.WriteLine("Skipping a field with incomplete definition.");
                        continue;
                    }

                    // Parse numeric values
                    int    pageNum = int.Parse(pageStr);
                    float  llx     = float.Parse(llxStr);
                    float  lly     = float.Parse(llyStr);
                    float  urx     = float.Parse(urxStr);
                    float  ury     = float.Parse(uryStr);

                    // Map string to FieldType enum
                    FieldType fieldType;
                    switch (typeStr.Trim().ToLowerInvariant())
                    {
                        case "text":
                            fieldType = FieldType.Text;
                            break;
                        case "multilinetext":
                            fieldType = FieldType.MultiLineText;
                            break;
                        case "checkbox":
                            fieldType = FieldType.CheckBox;
                            break;
                        case "listbox":
                            fieldType = FieldType.ListBox;
                            break;
                        case "combobox":
                            fieldType = FieldType.ComboBox;
                            break;
                        case "radiobutton":
                            fieldType = FieldType.Radio;
                            break;
                        case "button":
                            fieldType = FieldType.PushButton;
                            break;
                        default:
                            Console.Error.WriteLine($"Unsupported field type '{typeStr}'. Skipping field '{name}'.");
                            continue;
                    }

                    // Add the field to the PDF
                    bool added = editor.AddField(fieldType, name, pageNum, llx, lly, urx, ury);
                    if (!added)
                    {
                        Console.Error.WriteLine($"Failed to add field '{name}'.");
                    }
                }

                // Commit the added fields (FormEditor.Save writes changes to the underlying Document)
                editor.Save();
            }

            // After fields are created, set their default values using the Form facade
            using (Form form = new Form(pdfDoc))
            {
                XmlNodeList nodes = fieldDef.SelectNodes("//Field");
                foreach (XmlNode node in nodes)
                {
                    string name        = node.Attributes["name"]?.Value;
                    string typeStr     = node.Attributes["type"]?.Value;
                    string defaultVal  = node.Attributes["defaultValue"]?.Value;

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(typeStr) || defaultVal == null)
                        continue; // No default value to set

                    // Determine how to fill based on field type
                    switch (typeStr.Trim().ToLowerInvariant())
                    {
                        case "text":
                        case "multilinetext":
                        case "listbox":
                        case "combobox":
                        case "button":
                            // All these accept a string value
                            form.FillField(name, defaultVal);
                            break;

                        case "checkbox":
                            // Expect "true"/"false" (case‑insensitive)
                            if (bool.TryParse(defaultVal, out bool boolVal))
                                form.FillField(name, boolVal);
                            break;

                        case "radiobutton":
                            // Expect an integer index (0‑based) indicating the selected option
                            if (int.TryParse(defaultVal, out int intVal))
                                form.FillField(name, intVal);
                            break;
                    }
                }

                // Save the final PDF with fields and default values
                form.Save(outputPdf);
            }

            Console.WriteLine($"PDF with form fields created: {outputPdf}");
        }
    }
}