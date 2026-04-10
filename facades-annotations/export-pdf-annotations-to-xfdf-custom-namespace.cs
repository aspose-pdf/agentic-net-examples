using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing annotations
        const string inputPdfPath = "input.pdf";

        // Desired output XFDF file
        const string outputXfdfPath = "custom_namespace.xfdf";

        // Custom XFDF namespace required by the enterprise
        const string customNamespace = "http://example.com/customxfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (creation rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor facade (creation rule)
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfDoc); // bind the document to the facade

            // Export annotations to a memory stream (save rule)
            using (MemoryStream tempStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(tempStream);
                tempStream.Position = 0; // reset for reading

                // Load the exported XFDF XML
                XDocument xfdfDoc = XDocument.Load(tempStream);

                // Change the default namespace to the custom one
                // The XFDF root element is typically <xfdf>
                XElement root = xfdfDoc.Root;
                if (root != null)
                {
                    // Preserve existing elements but replace the namespace
                    XNamespace newNs = customNamespace;
                    root.Name = newNs + root.Name.LocalName;

                    // Update all descendant elements to use the new namespace
                    foreach (XElement elem in root.Descendants())
                    {
                        elem.Name = newNs + elem.Name.LocalName;
                    }

                    // Optionally add the xmlns attribute if not present
                    root.SetAttributeValue("xmlns", customNamespace);
                }

                // Save the modified XFDF to the final file (save rule)
                using (FileStream outFile = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfDoc.Save(outFile);
                }
            }

            // No need to call editor.Close() as it does not implement IDisposable
        }

        Console.WriteLine($"Annotations exported with custom namespace to '{outputXfdfPath}'.");
    }
}