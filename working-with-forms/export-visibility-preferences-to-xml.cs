using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF and output XML paths
        const string inputPdf = "input.pdf";
        const string outputXml = "preferences.xml";

        // Example user preferences that determine visibility
        bool showCrossSection = true;   // could be loaded from a config file
        bool exportWidget = false;      // example preference

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Handle WidgetAnnotation exportable flag
                    if (ann is WidgetAnnotation widget)
                    {
                        widget.Exportable = exportWidget;
                    }

                    // NOTE: Aspose.PDF for .NET does not provide a Pdf3DAnnotation class.
                    // The original code attempted to set a 3‑D cross‑section visibility flag.
                    // Since 3‑D annotations are not supported, this block is intentionally omitted.
                    // If future versions add support, the visibility can be set here.
                }
            }

            // Save the document model to XML preserving the modified states
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            doc.Save(outputXml, xmlOptions);
        }

        Console.WriteLine($"Preferences exported to XML: {outputXml}");
    }
}
