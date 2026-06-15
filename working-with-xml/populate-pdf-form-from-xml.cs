using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Forms;        // For the Form class (core API)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "TemplateForm.pdf";   // PDF containing the form (AcroForm or XFA)
        const string sourceXmlPath   = "Data.xml";          // XML file with one or more records
        const string outputFolder    = "FilledForms";

        // Verify input files/folders
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(sourceXmlPath))
        {
            Console.Error.WriteLine($"Source XML not found: {sourceXmlPath}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Load the XML document that contains multiple records
        XmlDocument masterXml = new XmlDocument();
        masterXml.Load(sourceXmlPath);

        // Assume each record is represented by a <Record> element
        XmlNodeList records = masterXml.SelectNodes("//Record");
        if (records == null || records.Count == 0)
        {
            Console.Error.WriteLine("No <Record> elements found in the XML.");
            return;
        }

        int recordIndex = 1;
        foreach (XmlNode recordNode in records)
        {
            // Create a fresh copy of the PDF template for each record
            using (Document pdfDoc = new Document(templatePdfPath))
            {
                // The Form object is accessed via the core API (Aspose.Pdf.Forms.Form)
                Form pdfForm = pdfDoc.Form;

                // If the PDF contains an XFA form, AssignXfa will populate it directly.
                // For AcroForm fields, we can set values manually (example shown below).
                if (pdfForm.HasXfa)
                {
                    // Build a temporary XmlDocument that contains only the current record.
                    XmlDocument singleRecordXml = new XmlDocument();
                    singleRecordXml.LoadXml(recordNode.OuterXml);

                    // Populate the XFA form with the XML data.
                    pdfForm.AssignXfa(singleRecordXml);
                }
                else
                {
                    // Example of filling standard AcroForm fields:
                    // Expect XML structure like <Field name="FirstName">John</Field>
                    foreach (XmlNode fieldNode in recordNode.SelectNodes("Field"))
                    {
                        string fieldName = fieldNode.Attributes["name"]?.Value;
                        string fieldValue = fieldNode.InnerText;

                        if (string.IsNullOrEmpty(fieldName))
                            continue; // Skip malformed entries

                        // Retrieve the widget annotation representing the field.
                        // The indexer returns a WidgetAnnotation; we set its Contents.
                        if (pdfForm.HasField(fieldName))
                        {
                            var widget = pdfForm[fieldName];
                            // For text fields, set the Contents property.
                            widget.Contents = fieldValue;
                        }
                    }
                }

                // Save the filled PDF with a unique name.
                string outputPath = Path.Combine(outputFolder, $"FilledForm_{recordIndex}.pdf");
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Saved filled form: {outputPath}");
            }

            recordIndex++;
        }
    }
}