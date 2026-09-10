using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "metadata.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the XML metadata
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Prepare namespace manager for the XMP PDF/A field namespace
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
            const string defaultFieldNsUri = XmpPdfAExtensionSchema.DefaultFieldNamespaceUri;
            const string defaultFieldNsPrefix = XmpPdfAExtensionSchema.DefaultFieldNamespacePrefix;
            nsMgr.AddNamespace(defaultFieldNsPrefix, defaultFieldNsUri);

            // Example list of required fields
            string[] requiredFields = { "Creator", "Producer", "Title" };

            // Ensure each required field exists; if not, create it with a fallback value
            foreach (string fieldName in requiredFields)
            {
                string xpath = $"//{defaultFieldNsPrefix}:{fieldName}";
                XmlNode node = xmlDoc.SelectSingleNode(xpath, nsMgr);

                if (node == null)
                {
                    // Create a new element using the default namespace URI
                    XmlElement newElem = xmlDoc.CreateElement(defaultFieldNsPrefix, fieldName, defaultFieldNsUri);
                    newElem.InnerText = $"Default {fieldName}";
                    // Append to the root element if it exists
                    if (xmlDoc.DocumentElement != null)
                    {
                        xmlDoc.DocumentElement.AppendChild(newElem);
                    }
                }
            }

            // Register the custom namespace in the PDF XMP metadata
            pdfDoc.Metadata.RegisterNamespaceUri(defaultFieldNsPrefix, defaultFieldNsUri);

            // Transfer the (now guaranteed) field values into the PDF's XMP metadata
            foreach (string fieldName in requiredFields)
            {
                string xpath = $"//{defaultFieldNsPrefix}:{fieldName}";
                XmlNode node = xmlDoc.SelectSingleNode(xpath, nsMgr);
                if (node != null)
                {
                    // Store under the standard xmp: prefix – Aspose.Pdf will map it correctly
                    pdfDoc.Metadata[$"xmp:{fieldName}"] = node.InnerText;
                }
            }

            // Save the modified PDF (lifecycle rule: use using, then Save)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with fallback metadata to '{outputPdf}'.");
    }
}
