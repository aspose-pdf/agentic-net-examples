using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "progress.xml";
        const string progressFieldName = "ProgressBar"; // name of the progress bar field in the PDF form

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Retrieve the form field safely – the Form indexer returns a WidgetAnnotation,
                // so we need to cast it to Aspose.Pdf.Forms.Field.
                Field progressField = pdfDoc.Form[progressFieldName] as Field;

                if (progressField == null)
                {
                    Console.Error.WriteLine($"Form field '{progressFieldName}' not found in the PDF.");
                    return;
                }

                // Retrieve the current value of the progress bar (as string)
                string progressValue = progressField.Value?.ToString() ?? string.Empty;

                // Create an XML document to store the progress information
                XmlDocument xmlDoc = new XmlDocument();

                // Root element
                XmlElement root = xmlDoc.CreateElement("FormProgress");
                xmlDoc.AppendChild(root);

                // Child element representing the progress field
                XmlElement fieldElement = xmlDoc.CreateElement("Field");
                fieldElement.SetAttribute("Name", progressFieldName);
                fieldElement.InnerText = progressValue;
                root.AppendChild(fieldElement);

                // Save the XML to the specified path
                xmlDoc.Save(outputXmlPath);
                Console.WriteLine($"Progress value exported to XML file: {outputXmlPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
