using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string xmlDataPath     = "data.xml";
        const string outputPdfPath   = "filled.pdf";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load the XML document once.
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlDataPath);
        var navigator = xmlDoc.CreateNavigator();

        // Load the PDF and bind it to the Form facade.
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            using (Form pdfForm = new Form(pdfDoc))
            {
                // Iterate over every AcroForm field.
                foreach (string fieldName in pdfForm.FieldNames)
                {
                    // Assume the XML element name matches the field name.
                    string xpath = $"//{fieldName}";
                    var node = navigator.SelectSingleNode(xpath);
                    if (node != null)
                    {
                        string value = node.Value;
                        // Fill the field with the extracted value.
                        pdfForm.FillField(fieldName, value);
                    }
                }

                // Save the updated PDF.
                pdfForm.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form fields filled and saved to '{outputPdfPath}'.");
    }
}