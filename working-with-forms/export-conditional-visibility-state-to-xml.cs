using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExportVisibilityState
{
    static void Main()
    {
        // Paths for input PDF and output XML
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "visibility_state.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];

                // Iterate through all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Link annotation that carries a HideAction – toggle hidden state
                    if (annotation is LinkAnnotation link && link.Action is HideAction hideAction)
                    {
                        hideAction.IsHidden = !hideAction.IsHidden;
                    }

                    // Widget (form field) – ensure it is exportable
                    if (annotation is WidgetAnnotation widget)
                    {
                        widget.Exportable = true;
                    }
                }
            }

            // Export the entire document model (including annotation states) to XML.
            // Document.SaveXml writes the PDF structure to an XML file.
            pdfDocument.SaveXml(outputXmlPath);
        }

        Console.WriteLine($"Visibility state exported to XML: {outputXmlPath}");
    }
}
