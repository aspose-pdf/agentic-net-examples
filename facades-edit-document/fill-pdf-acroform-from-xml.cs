using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string xmlDataPath = "data.xml";
        const string outputPdfPath = "filled.pdf";

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

        // Load the XML document once
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlDataPath);

        // Use the Form facade (implements IDisposable) to work with AcroForm fields
        using (Form form = new Form())
        {
            // Bind the PDF that contains the form fields
            form.BindPdf(pdfTemplatePath);

            // Retrieve all field names present in the PDF via the FieldNames property
            string[] fieldNames = form.FieldNames;

            foreach (string fieldName in fieldNames)
            {
                // Locate the corresponding XML node using XPath.
                // This example assumes XML elements are named exactly like the PDF fields.
                XmlNode node = xmlDoc.SelectSingleNode($"//*[local-name()='{fieldName}']");

                if (node != null)
                {
                    // Fill the PDF field with the text from the XML node
                    form.FillField(fieldName, node.InnerText);
                }
            }

            // Save the populated PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"AcroForm fields populated and saved to '{outputPdfPath}'.");
    }
}
