using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfTemplate = "template.pdf";   // PDF with form fields
        const string xmlData     = "data.xml";       // XML containing multiple records
        const string outputDir   = "Output";

        if (!File.Exists(pdfTemplate))
        {
            Console.Error.WriteLine($"Template not found: {pdfTemplate}");
            return;
        }

        if (!File.Exists(xmlData))
        {
            Console.Error.WriteLine($"XML data not found: {xmlData}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the XML document that holds one or more <Record> elements
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlData);
        XmlNodeList records = xmlDoc.SelectNodes("//Record");

        if (records == null || records.Count == 0)
        {
            Console.Error.WriteLine("No <Record> elements found in the XML file.");
            return;
        }

        int recordIndex = 1;
        foreach (XmlNode recordNode in records)
        {
            // Open a fresh copy of the PDF template for each record
            using (Document pdfDoc = new Document(pdfTemplate))
            {
                // If the PDF uses an XFA form, assign the XML fragment directly
                if (pdfDoc.Form.HasXfa)
                {
                    // Build a minimal XFA XML that contains only the current record
                    XmlDocument xfaFragment = new XmlDocument();
                    XmlElement datasets = xfaFragment.CreateElement("datasets");
                    xfaFragment.AppendChild(datasets);
                    XmlNode imported = xfaFragment.ImportNode(recordNode, true);
                    datasets.AppendChild(imported);

                    // Apply the XFA data to the form
                    pdfDoc.Form.AssignXfa(xfaFragment);
                }
                else
                {
                    // For standard AcroForm fields, map XML element values to field names
                    foreach (Field field in pdfDoc.Form.Fields)
                    {
                        // Try to find an element whose name matches the field name
                        XmlNode valueNode = recordNode.SelectSingleNode($"*[@name='{field.Name}']") ??
                                            recordNode.SelectSingleNode($"{field.Name}");

                        if (valueNode != null)
                        {
                            field.Value = valueNode.InnerText;
                        }
                    }
                }

                // Save the filled PDF for this record
                string outPath = Path.Combine(outputDir, $"filled_{recordIndex}.pdf");
                pdfDoc.Save(outPath);
                Console.WriteLine($"Saved: {outPath}");
            }

            recordIndex++;
        }
    }
}