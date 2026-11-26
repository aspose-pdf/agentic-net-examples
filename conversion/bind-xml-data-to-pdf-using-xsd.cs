using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfTemplate = "template.pdf";
        const string xmlPath = "data.xml";
        const string xsdPath = "schema.xsd";
        const string outputPdf = "output.pdf";

        // Ensure required files exist
        if (!File.Exists(pdfTemplate))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplate}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsdPath))
        {
            Console.Error.WriteLine($"XSD file not found: {xsdPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfTemplate))
        {
            // Bind the XML data to the PDF using the schema for validation
            // Correct method name is BindXml (case‑sensitive)
            doc.BindXml(xmlPath, xsdPath);

            // Save the bound PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated: {outputPdf}");
    }
}
