using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF with AcroForm
        const string xmlPath   = "data.xml";    // XML file containing data
        const string outputPath = "filled.pdf"; // result PDF

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document once – it will be queried with XPath.
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);
        XPathNavigator navigator = xmlDoc.CreateNavigator();

        // Open the PDF and bind the Form facade.
        using (Document pdfDoc = new Document(pdfPath))
        using (Form form = new Form(pdfDoc))
        {
            // Iterate through every field defined in the PDF.
            foreach (string fieldName in form.FieldNames)
            {
                // Build an XPath expression that selects the element whose name matches the field.
                // Adjust the XPath pattern to match your XML schema if needed.
                string xpath = $"//{fieldName}";
                XPathExpression expr = navigator.Compile(xpath);
                XPathNodeIterator iterator = navigator.Select(expr);

                if (iterator.MoveNext())
                {
                    string value = iterator.Current.Value;
                    // Fill the AcroForm field with the extracted value.
                    form.FillField(fieldName, value);
                }
            }

            // Save the updated PDF. Form.Save writes the changes to a new file.
            form.Save(outputPath);
        }

        Console.WriteLine($"AcroForm fields populated and saved to '{outputPath}'.");
    }
}