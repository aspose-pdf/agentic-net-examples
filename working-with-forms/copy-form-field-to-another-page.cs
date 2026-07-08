using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";   // name of the existing field
        const int targetPage = 2;                 // page where the copy will be placed (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the form contains the specified field
            if (!doc.Form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Retrieve the original field instance – the Form indexer returns a WidgetAnnotation,
            // so we need an explicit cast (or 'as') to Aspose.Pdf.Forms.Field.
            Field originalField = doc.Form[fieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' could not be cast to a form Field.");
                return;
            }

            // Create a copy of the field on the target page.
            // The Add(Field, string, int) overload creates a new field based on the supplied one.
            string copyPartialName = fieldName + "_Copy";
            Field copiedField = doc.Form.Add(originalField, copyPartialName, targetPage);

            // Optionally set the position of the copied field.
            // Here we reuse the original rectangle; you can adjust coordinates as needed.
            Rectangle origRect = originalField.Rect;
            Rectangle newRect = new Rectangle(
                origRect.LLX,
                origRect.LLY,
                origRect.URX,
                origRect.URY);

            // Add the appearance of the copied field on the target page.
            doc.Form.AddFieldAppearance(copiedField, targetPage, newRect);

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} and saved as '{outputPath}'.");
    }
}
