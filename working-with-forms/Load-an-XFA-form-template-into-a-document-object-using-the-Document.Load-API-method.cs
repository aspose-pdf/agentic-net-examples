using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "xfa_template.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"File not found: {templatePath}");
            return;
        }

        // Use a using block for deterministic disposal (lifecycle rule)
        // Load the XFA form template via the Document constructor (Document.Load API does not exist)
        using (Document doc = new Document(templatePath))
        {
            // Check if the loaded document contains an XFA form
            if (doc.Form != null && doc.Form.HasXfa)
            {
                // Access the XFA template XML node
                XmlNode templateNode = doc.Form.XFA.Template;
                Console.WriteLine("XFA template loaded successfully.");
                Console.WriteLine($"Root element: {templateNode.Name}");
            }
            else
            {
                Console.WriteLine("The document does not contain an XFA form.");
            }
        }
    }
}
