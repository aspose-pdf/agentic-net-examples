using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "output_custom.xfdf";
        const string customNamespace = "http://example.com/customxfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor and bind the PDF
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfDoc);

            // Export annotations to a memory stream (XFDF format)
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset stream position for reading

                // Load the exported XFDF XML
                XDocument xfdfXml = XDocument.Load(xfdfStream);

                // Change the default namespace to the custom one
                // Create a new root element with the custom namespace and copy existing content
                XElement oldRoot = xfdfXml.Root;
                XNamespace ns = customNamespace;
                XElement newRoot = new XElement(ns + oldRoot.Name.LocalName,
                    // Preserve attributes (if any) without namespace changes
                    oldRoot.Attributes(),
                    // Preserve child elements, updating their namespace as well
                    UpdateNamespaceRecursive(oldRoot, ns));

                // Replace the document's root
                xfdfXml = new XDocument(newRoot);

                // Save the modified XFDF to the desired file
                xfdfXml.Save(outputXfdfPath);
            }

            // Close the editor (optional, as it does not hold unmanaged resources)
            editor.Close();
        }

        Console.WriteLine($"Annotations exported with custom namespace to '{outputXfdfPath}'.");
    }

    // Recursively updates the namespace of an element and its descendants
    private static object[] UpdateNamespaceRecursive(XElement element, XNamespace ns)
    {
        var updatedChildren = new System.Collections.Generic.List<object>();
        foreach (var node in element.Nodes())
        {
            if (node is XElement child)
            {
                XElement newChild = new XElement(ns + child.Name.LocalName,
                    child.Attributes(),
                    UpdateNamespaceRecursive(child, ns));
                updatedChildren.Add(newChild);
            }
            else
            {
                // Preserve text nodes, comments, etc.
                updatedChildren.Add(node);
            }
        }
        return updatedChildren.ToArray();
    }
}