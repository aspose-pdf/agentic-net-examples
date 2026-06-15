using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "flattened_xfa.pdf";
        const string xfaTemplatePath = "template.xfa.xml"; // XFA template (XDP) file

        // Ensure the XFA template exists
        if (!File.Exists(xfaTemplatePath))
        {
            Console.Error.WriteLine($"XFA template not found: {xfaTemplatePath}");
            return;
        }

        // Load the XFA XML definition
        XmlDocument xfaXml = new XmlDocument();
        xfaXml.Load(xfaTemplatePath);

        // Create a new PDF document (adds a blank page by default)
        using (Document doc = new Document())
        {
            // Assign the XFA data to the document's form
            doc.Form.AssignXfa(xfaXml);

            // OPTIONAL: set a value for a field defined in the XFA template.
            // The field name must match the fully‑qualified name in the template.
            // Example: data.myField
            // doc.Form.XFA.Item["data.myField"] = "Sample value";

            // Flatten the XFA form – all fields become static content.
            doc.Form.Flatten();

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with flattened XFA form saved to '{outputPdf}'.");
    }
}