using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string xmlPath = "data.xml";
        const string outputDir = "Output";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        XDocument xdoc;
        try
        {
            xdoc = XDocument.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load XML: {ex.Message}");
            return;
        }

        // Expect each record to be a <Record> element whose child element names match PDF field names
        var records = xdoc.Root?.Elements("Record");
        if (records == null)
        {
            Console.Error.WriteLine("No <Record> elements found in XML.");
            return;
        }

        int index = 1;
        foreach (var record in records)
        {
            // Lifecycle rule: wrap Document in a using block for deterministic disposal
            using (Document pdfDoc = new Document(templatePath))
            {
                var form = pdfDoc.Form;
                if (form != null && form.Fields != null)
                {
                    foreach (Field field in form.Fields)
                    {
                        // Determine the field name – try the most common properties
                        string fieldName = field.Name ?? field.FullName ?? field.PartialName ?? string.Empty;
                        if (string.IsNullOrEmpty(fieldName))
                            continue;

                        // Find matching XML element for the current field
                        var xmlElement = record.Element(fieldName);
                        if (xmlElement == null)
                            continue;

                        // Set the field value based on its concrete type
                        if (field is TextBoxField txt)
                        {
                            txt.Value = xmlElement.Value;
                        }
                        else if (field is CheckboxField chk)
                        {
                            chk.Checked = string.Equals(xmlElement.Value, "true", StringComparison.OrdinalIgnoreCase);
                        }
                        else if (field is RadioButtonOptionField rad)
                        {
                            rad.Value = xmlElement.Value;
                        }
                        else if (field is ListBoxField list)
                        {
                            list.Value = xmlElement.Value;
                        }
                        // Additional field types can be handled here as needed
                    }
                }

                string outPath = Path.Combine(outputDir, $"filled_{index}.pdf");
                pdfDoc.Save(outPath); // Save as PDF (no SaveOptions needed)
                Console.WriteLine($"Saved record {index} to {outPath}");
            }
            index++;
        }
    }
}
