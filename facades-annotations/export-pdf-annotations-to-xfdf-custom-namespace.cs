using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputXfdf = "annotations.xfdf";
        const string customNs   = "http://enterprise.example.com/xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdf))
            {
                // Export annotations to XFDF using the filename‑only overload.
                // This works with older Aspose.Pdf versions that do not expose SaveFormat.Xfdf.
                string tempXfdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xfdf");
                doc.Save(tempXfdfPath); // format inferred from .xfdf extension

                // Load the generated XFDF XML.
                XDocument xfdfDoc = XDocument.Load(tempXfdfPath);

                // Replace the default XFDF namespace with the custom namespace required by the enterprise standard.
                XNamespace custom = customNs;
                XElement oldRoot = xfdfDoc.Root;
                if (oldRoot != null)
                {
                    // Preserve existing attributes (except the old xmlns) and child nodes.
                    var newRoot = new XElement(custom + oldRoot.Name.LocalName,
                                               // Keep all attributes that are not the default xmlns.
                                               oldRoot.Attributes(),
                                               oldRoot.Nodes());
                    xfdfDoc.Root.ReplaceWith(newRoot);
                }

                // Save the modified XFDF to the target file.
                xfdfDoc.Save(outputXfdf);

                // Clean up the temporary file.
                File.Delete(tempXfdfPath);
            }

            Console.WriteLine($"Annotations exported to XFDF with custom namespace: {outputXfdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
