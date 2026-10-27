using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "formdata.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(inputPdfPath);

        // If the document does not contain an AcroForm, exit gracefully.
        if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
        {
            Console.WriteLine("No AcroForm fields found in the PDF.");
            return;
        }

        // Create an XML structure that holds each field's name and value.
        XElement root = new XElement("AcroFormData");
        foreach (Field field in pdfDoc.Form)
        {
            string name = field.PartialName ?? string.Empty;
            string value = field.Value?.ToString() ?? string.Empty;
            XElement fieldElement = new XElement("Field",
                new XAttribute("Name", name),
                new XAttribute("Value", value));
            root.Add(fieldElement);
        }

        // Save the XML document to the requested file.
        XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        xmlDoc.Save(outputXmlPath);

        Console.WriteLine($"AcroForm data exported to '{outputXmlPath}'.");
    }
}
