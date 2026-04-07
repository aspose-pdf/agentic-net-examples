using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class ExportConditionalVisibility
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

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Iterate through pages to find a widget annotation (form field)
            foreach (Page page in pdfDocument.Pages)
            {
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation annotation = page.Annotations[i];
                    if (annotation is WidgetAnnotation widget)
                    {
                        // Example condition: toggle Exportable based on current value
                        widget.Exportable = !widget.Exportable;
                        // You could also set other visibility‑related properties, e.g.:
                        // widget.Hidden = true; // hides the annotation in the viewer
                        // widget.IsHidden = true; // using HideAction if applicable
                    }
                }
            }

            // Prepare XML save options
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document model (including annotation visibility states) to XML
            pdfDocument.Save(outputXmlPath, xmlOptions);
        }

        Console.WriteLine($"Conditional visibility state exported to '{outputXmlPath}'.");
    }
}