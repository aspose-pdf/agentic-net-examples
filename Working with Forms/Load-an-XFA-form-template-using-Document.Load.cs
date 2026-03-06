using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xfaTemplatePath = "xfa_template.pdf";

        if (!File.Exists(xfaTemplatePath))
        {
            Console.Error.WriteLine($"File not found: {xfaTemplatePath}");
            return;
        }

        // Load the PDF document that contains an XFA form.
        using (Document doc = new Document(xfaTemplatePath))
        {
            // Verify that the document actually contains an XFA form.
            if (!doc.Form.HasXfa)
            {
                Console.WriteLine("The loaded PDF does not contain an XFA form.");
                return;
            }

            // Access the XFA object.
            XFA xfa = doc.Form.XFA;

            // Retrieve the XFA template (XML node) from the form.
            XmlNode templateNode = xfa.Template;

            // Example: output the outer XML of the template to the console.
            Console.WriteLine("XFA Template XML:");
            Console.WriteLine(templateNode?.OuterXml ?? "Template node is null.");

            // (Optional) Modify the template or assign a new XFA XML document.
            // Here we demonstrate assigning a new XFA XML document.
            // XmlDocument newXfaXml = new XmlDocument();
            // newXfaXml.Load("new_xfa_data.xml");
            // doc.Form.AssignXfa(newXfaXml);

            // Save the (potentially modified) document.
            const string outputPath = "output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}