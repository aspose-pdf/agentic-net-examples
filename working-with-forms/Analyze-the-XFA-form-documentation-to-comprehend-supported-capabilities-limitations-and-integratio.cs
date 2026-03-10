using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class XfaDemo
{
    static void Main()
    {
        // Paths to input PDF, output PDF, and optional XFA XML replacement
        const string inputPdfPath = "input_with_xfa.pdf";
        const string outputPdfPath = "output_modified.pdf";
        const string newXfaXmlPath = "new_xfa.xml";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the Form object (represents AcroForm fields)
            Form form = pdfDoc.Form;

            // Check whether the document contains an XFA form
            if (form.HasXfa)
            {
                Console.WriteLine("Document contains an XFA form.");

                // Access the XFA object
                XFA xfa = form.XFA;

                // ----- Capabilities -----
                // 1. Enumerate field names defined in the XFA template
                Console.WriteLine("XFA field names:");
                foreach (string fieldName in xfa.FieldNames)
                {
                    Console.WriteLine($"  {fieldName}");
                }

                // 2. Read a data node value using an XPath expression.
                // Example path: "data/Customer/Name"
                string samplePath = "data/Customer/Name";
                string fieldValue = GetXfaDataNodeValue(xfa, samplePath);
                Console.WriteLine($"Value at '{samplePath}': {fieldValue}");

                // 3. Set a data node value using the same XPath syntax.
                SetXfaDataNodeValue(xfa, samplePath, "John Doe");
                Console.WriteLine($"Updated value at '{samplePath}' to 'John Doe'.");

                // 4. Access individual XFA components (Form, Template, Config, Datasets, XDP)
                XmlNode formNode = xfa.Form;               // Template structure
                XmlNode templateNode = xfa.Template;       // Visual layout
                XmlNode configNode = xfa.Config;           // Configuration settings
                XmlNode datasetsNode = xfa.Datasets;       // Data set container
                XmlDocument xdpDoc = xfa.XDP;              // Whole XDP package

                // Example: output the root element name of the XFA Form component
                Console.WriteLine($"XFA Form root element: {formNode?.Name}");

                // 5. Retrieve all field templates (metadata about each field)
                XmlNodeList fieldTemplates = xfa.GetFieldTemplates();
                Console.WriteLine($"Number of field templates: {fieldTemplates?.Count ?? 0}");

                // 6. Set an image for an XFA image field (if such a field exists)
                // Assume there is an image field named "data/ImageField"
                string imageFieldPath = "data/ImageField";
                if (File.Exists("sample_image.png"))
                {
                    using (FileStream imgStream = File.OpenRead("sample_image.png"))
                    {
                        // SetFieldImage replaces the image content of the specified XFA field
                        xfa.SetFieldImage(imageFieldPath, imgStream);
                        Console.WriteLine($"Image set for XFA field '{imageFieldPath}'.");
                    }
                }

                // ----- Integration Procedure -----
                // Replace the entire XFA package with a new XML document (optional)
                if (File.Exists(newXfaXmlPath))
                {
                    XmlDocument newXfaDoc = new XmlDocument();
                    newXfaDoc.Load(newXfaXmlPath);
                    form.AssignXfa(newXfaDoc);
                    Console.WriteLine("Replaced XFA data with external XML document.");
                }

                // ----- Limitations -----
                // Dynamic XFA forms may require special handling during conversion.
                // If the form is dynamic and you need to convert it to a static PDF/AcroForm,
                // consider setting IgnoreNeedsRendering = true when appropriate.
                // Example (uncomment if needed):
                // form.IgnoreNeedsRendering = true;

                // Flatten the form to make field values part of the page content.
                // This removes interactive fields but preserves visual appearance.
                form.Flatten();
                Console.WriteLine("Form flattened – interactive fields removed.");
            }
            else
            {
                Console.WriteLine("Document does not contain an XFA form.");
            }

            // Save the modified PDF (Document.Save handles PDF output automatically)
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to: {outputPdfPath}");
        }
    }

    // Helper method to read a value from the XFA Datasets node using XPath.
    private static string GetXfaDataNodeValue(XFA xfa, string xpath)
    {
        if (xfa?.Datasets == null)
            return string.Empty;

        XmlNode node = xfa.Datasets.SelectSingleNode(xpath);
        return node?.InnerText ?? string.Empty;
    }

    // Helper method to set a value in the XFA Datasets node using XPath.
    private static void SetXfaDataNodeValue(XFA xfa, string xpath, string newValue)
    {
        if (xfa?.Datasets == null)
            return;

        XmlNode node = xfa.Datasets.SelectSingleNode(xpath);
        if (node != null)
        {
            node.InnerText = newValue;
        }
        else
        {
            // If the node does not exist, create the necessary hierarchy.
            // Simple implementation: create the node under the root Datasets element.
            XmlElement newElem = xfa.Datasets.OwnerDocument.CreateElement(xpath.Replace('/', '_'));
            newElem.InnerText = newValue;
            xfa.Datasets.AppendChild(newElem);
        }
    }
}
