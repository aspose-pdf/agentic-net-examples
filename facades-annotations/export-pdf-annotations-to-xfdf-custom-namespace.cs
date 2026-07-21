using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing annotations
        const string inputPdfPath = "input.pdf";

        // Desired output XFDF file path
        const string outputXfdfPath = "custom_namespace.xfdf";

        // Custom XFDF namespace required by enterprise standards
        const string customNamespace = "http://mycompany.com/xfdf";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Use PdfAnnotationEditor facade to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPdfPath);

            // Export annotations to an in‑memory stream (default XFDF)
            using (MemoryStream tempStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(tempStream);
                tempStream.Position = 0; // Reset for reading

                // Load the XFDF XML
                XDocument xfdfDoc = XDocument.Load(tempStream);

                // Replace the default namespace with the custom one
                // Set the xmlns attribute on the root element
                xfdfDoc.Root.SetAttributeValue("xmlns", customNamespace);

                // If the document contains elements that explicitly use the old namespace,
                // rename them to the new namespace as well
                XNamespace oldNs = xfdfDoc.Root.GetDefaultNamespace();
                XNamespace newNs = customNamespace;

                foreach (XElement elem in xfdfDoc.Descendants())
                {
                    // Change element name to use the new namespace
                    elem.Name = newNs + elem.Name.LocalName;
                }

                // Save the modified XFDF to the target file
                xfdfDoc.Save(outputXfdfPath);
            }

            // Close the editor (optional, as using will dispose it)
            editor.Close();
        }

        Console.WriteLine($"Annotations exported with custom namespace to '{outputXfdfPath}'.");
    }
}