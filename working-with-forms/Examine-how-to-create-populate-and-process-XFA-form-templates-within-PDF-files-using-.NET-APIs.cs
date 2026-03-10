using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newXfaTemplatePath = "new_template.xfa";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains an XFA form
            if (!doc.Form.HasXfa)
            {
                Console.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Access the XFA object
            XFA xfa = doc.Form.XFA;

            // List all field names present in the XFA template
            Console.WriteLine("XFA field names:");
            foreach (string fieldName in xfa.FieldNames)
            {
                Console.WriteLine($"- {fieldName}");
            }

            // Example: set a value for a specific field using the Item indexer.
            // The path uses the "data" namespace defined by Aspose.Pdf for XFA data.
            // Adjust the field path according to your template.
            string fieldPath = "data.CustomerName";
            xfa[fieldPath] = "John Doe";

            // Export the current XDP (XML Data Package) to an external file (optional)
            XmlDocument xdp = xfa.XDP;
            xdp.Save("current_xfa_data.xml");

            // If you have a new XFA template (XML) you can assign it to the form.
            if (File.Exists(newXfaTemplatePath))
            {
                XmlDocument newXfaDoc = new XmlDocument();
                newXfaDoc.Load(newXfaTemplatePath);
                // Replace the existing XFA data with the new template
                doc.Form.AssignXfa(newXfaDoc);
            }

            // Save the modified PDF back to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Modified PDF saved to '{outputPath}'.");
    }
}