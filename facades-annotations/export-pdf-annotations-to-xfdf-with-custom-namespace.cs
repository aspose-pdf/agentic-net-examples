using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Input PDF, output XFDF, and the custom namespace to apply
        const string pdfPath = "input.pdf";
        const string xfdfPath = "output.xfdf";
        const string customNamespace = "http://mycompany.com/customxfdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Export annotations using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);
            using (MemoryStream ms = new MemoryStream())
            {
                // Export all annotations to the memory stream
                editor.ExportAnnotationsToXfdf(ms);
                ms.Position = 0; // Reset stream for reading

                // Load the XFDF XML
                XDocument xfdfDoc = XDocument.Load(ms);

                // Replace the default XFDF namespace with the custom one (guard against empty document)
                if (xfdfDoc.Root != null)
                {
                    XNamespace newNs = customNamespace;
                    ReplaceNamespace(xfdfDoc.Root, newNs);
                }

                // Save the modified XFDF to the target file
                using (FileStream fs = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfDoc.Save(fs);
                }
            }
        }

        Console.WriteLine($"Annotations exported with custom namespace to '{xfdfPath}'.");
    }

    // Recursively updates element and attribute namespaces
    static void ReplaceNamespace(XElement element, XNamespace newNs)
    {
        if (element == null) return;

        // Update element name
        element.Name = newNs + element.Name.LocalName;

        // Update attributes that have a namespace (skip namespace declarations)
        foreach (var attr in element.Attributes())
        {
            if (!attr.IsNamespaceDeclaration && attr.Name.Namespace != XNamespace.None)
            {
                XName newName = newNs + attr.Name.LocalName;
                string value = attr.Value;
                attr.Remove();
                element.SetAttributeValue(newName, value);
            }
        }

        // Process child elements
        foreach (var child in element.Elements())
        {
            ReplaceNamespace(child, newNs);
        }
    }
}
