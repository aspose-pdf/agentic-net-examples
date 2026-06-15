using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "output_custom_namespace.xfdf";
        const string customNamespace = "http://example.com/customxfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Bind the document to the annotation editor
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Export annotations to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(ms);
                ms.Position = 0; // Reset stream for reading

                // Load the XFDF XML and replace the default namespace
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ms);

                if (xmlDoc.DocumentElement != null)
                {
                    // Set the custom namespace (overwrites the existing xmlns attribute)
                    xmlDoc.DocumentElement.SetAttribute("xmlns", customNamespace);
                }

                // Save the modified XFDF to the target file
                using (FileStream fs = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
                {
                    xmlDoc.Save(fs);
                }
            }
        }

        Console.WriteLine($"Annotations exported with custom namespace to '{outputXfdf}'.");
    }
}