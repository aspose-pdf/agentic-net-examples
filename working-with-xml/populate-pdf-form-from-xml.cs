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
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        XDocument xdoc = XDocument.Load(xmlPath);
        var records = xdoc.Root.Elements("Record");

        int index = 1;
        foreach (var record in records)
        {
            // Load a fresh copy of the template for each XML record
            using (Document pdfDoc = new Document(templatePath))
            {
                // Access the AcroForm attached to the document
                Form pdfForm = pdfDoc.Form;

                // Iterate over every field defined in the form
                foreach (Field field in pdfForm.Fields)
                {
                    string fieldName = field.FullName;
                    var xmlElement = record.Element(fieldName);
                    if (xmlElement != null)
                    {
                        string value = xmlElement.Value ?? string.Empty;

                        // Checkbox fields need special handling
                        if (field is CheckboxField checkBox)
                        {
                            checkBox.Checked = !string.IsNullOrEmpty(value) &&
                                                value != "0" &&
                                                !value.Equals("false", StringComparison.OrdinalIgnoreCase);
                        }
                        else
                        {
                            // Most other field types expose a Value property that accepts a string
                            field.Value = value;
                        }
                    }
                }

                // Build the output file name – use the Id element if it exists, otherwise fall back to an index
                string id = record.Element("Id")?.Value ?? index.ToString();
                string outPath = Path.Combine(outputDir, $"filled_{id}.pdf");

                pdfDoc.Save(outPath);
                Console.WriteLine($"Saved filled PDF: {outPath}");
            }

            index++;
        }
    }
}
