using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExportVisibilityState
{
    static void Main()
    {
        // Paths to the source PDF and the destination XML file
        const string pdfPath = "input.pdf";
        const string xmlPath = "visibility_state.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Iterate through all pages and their annotations
            foreach (Page page in pdfDocument.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    // If the annotation itself supports a Visibility property, set it.
                    // Most annotation types expose a boolean Visibility flag.
                    try
                    {
                        // Use reflection to avoid compile‑time dependency on a specific type.
                        var visibilityProp = annotation.GetType().GetProperty("Visibility");
                        if (visibilityProp != null && visibilityProp.PropertyType == typeof(bool) && visibilityProp.CanWrite)
                        {
                            visibilityProp.SetValue(annotation, false);
                        }
                    }
                    catch { /* ignore if property is not present */ }

                    // Example: handle WidgetAnnotation exportable flag
                    if (annotation is WidgetAnnotation widget)
                    {
                        widget.Exportable = false;
                    }

                    // NOTE: The generic Annotation class does not expose an 'Action' property.
                    // If you need to work with actions, use the 'Actions' collection which is
                    // available on specific annotation types (e.g., WidgetAnnotation).
                    // The original code attempted to access annotation.Action, which caused
                    // the CS1061 compile error. This block has been removed.
                }
            }

            // Initialize XmlSaveOptions (required for non‑PDF formats)
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document model, including annotation visibility states, to XML
            pdfDocument.Save(xmlPath, xmlOptions);
        }

        Console.WriteLine($"Conditional visibility state exported to XML: {xmlPath}");
    }
}
