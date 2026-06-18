using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the destination XML file.
        const string pdfPath  = "input.pdf";
        const string xmlPath  = "form_data.xml";

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // OPTIONAL: Fill some form fields programmatically.
            // Example: set a text field named "Name" to "John Doe".
            // The core Form API works with WidgetAnnotation objects.
            // Uncomment and adjust field names as needed.
            /*
            var field = pdfDoc.Form["Name"] as TextBoxField;
            if (field != null)
                field.Value = "John Doe";
            */

            // Export the entire document model (including form data) to an XML file.
            // SaveXml writes the PDF structure as XML; this can be used for offline
            // data collection or further processing.
            pdfDoc.SaveXml(xmlPath);
        }

        Console.WriteLine($"Form data exported to XML file: {xmlPath}");
    }
}