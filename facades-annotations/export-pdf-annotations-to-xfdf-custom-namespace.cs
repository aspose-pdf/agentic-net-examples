using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input PDF, temporary XFDF, and final XFDF with custom namespace
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string customNamespace = "http://myenterprise.com/xfdf";

        // Ensure the input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for Document)
        using (Document doc = new Document(pdfPath))
        {
            // Initialize the PdfAnnotationEditor facade and bind the PDF
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Export all annotations to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(ms);
                ms.Position = 0; // Reset stream position for reading

                // Load the exported XFDF XML
                XmlDocument xfdfXml = new XmlDocument();
                xfdfXml.Load(ms);

                // Change the default namespace to the custom one
                // Create a new XmlNamespaceManager for the original namespace
                string originalNs = xfdfXml.DocumentElement.NamespaceURI;
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xfdfXml.NameTable);
                nsmgr.AddNamespace("old", originalNs);

                // Rename the root element with the custom namespace
                XmlElement oldRoot = xfdfXml.DocumentElement;
                XmlElement newRoot = xfdfXml.CreateElement(oldRoot.Prefix, oldRoot.LocalName, customNamespace);

                // Copy attributes (if any) from old root to new root
                foreach (XmlAttribute attr in oldRoot.Attributes)
                {
                    XmlAttribute newAttr = (XmlAttribute)attr.CloneNode(true);
                    newRoot.Attributes.Append(newAttr);
                }

                // Move all child nodes to the new root
                while (oldRoot.HasChildNodes)
                {
                    XmlNode child = oldRoot.FirstChild;
                    oldRoot.RemoveChild(child);
                    newRoot.AppendChild(child);
                }

                // Replace the old root with the new one
                xfdfXml.ReplaceChild(newRoot, oldRoot);

                // Update namespace declarations for all descendant elements
                // (If elements use the default namespace, they will inherit the new one automatically)

                // Save the modified XFDF to the final file
                using (FileStream outFs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfXml.Save(outFs);
                }
            }

            // Close the editor (PdfAnnotationEditor does not implement IDisposable)
            editor.Close();
        }

        Console.WriteLine($"Annotations exported with custom namespace to '{xfdfPath}'.");
    }
}