using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF with a form
        const string outputPdf = "output.pdf"; // PDF after manipulation
        const string jsonPath = "fields.json"; // exported field data

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // -----------------------------------------------------------------
        // 1. Retrieve and display existing field names.
        // -----------------------------------------------------------------
        Console.WriteLine("Existing form fields:");
        foreach (var annotation in pdfDocument.Form)
        {
            // The collection returns WidgetAnnotation objects; cast to Field to access form‑specific members.
            if (annotation is Field field)
            {
                Console.WriteLine($" - {field.PartialName}");
            }
        }

        // -----------------------------------------------------------------
        // 2. Fill an existing text field (if it exists).
        // -----------------------------------------------------------------
        const string existingField = "TextBox1"; // adjust to a real field name
        // The indexer returns a WidgetAnnotation; cast to the appropriate field type.
        var fieldToFill = pdfDocument.Form[existingField] as TextBoxField;
        if (fieldToFill != null)
        {
            // Use the Value property to set the field's content.
            fieldToFill.Value = "Sample value";
            Console.WriteLine($"Filled field '{existingField}' with a value.");
        }
        else
        {
            Console.WriteLine($"Field '{existingField}' not found – skipping fill operation.");
        }

        // -----------------------------------------------------------------
        // 3. Add a new text box field to page 1.
        //    Parameters: page, rectangle (llx, lly, urx, ury)
        // -----------------------------------------------------------------
        const string newFieldName = "NewField";
        // Define the rectangle where the field will appear.
        var rect = new Rectangle(100, 500, 250, 520);
        var newTextBox = new TextBoxField(pdfDocument.Pages[1], rect)
        {
            PartialName = newFieldName,
            Value = string.Empty // default (empty) value
        };
        pdfDocument.Form.Add(newTextBox);
        Console.WriteLine($"Added new text box field '{newFieldName}' on page 1.");

        // -----------------------------------------------------------------
        // 4. Export current form data to a JSON file.
        // -----------------------------------------------------------------
        // The Form class provides ExportToJson in recent versions.
        // If the method is unavailable, manually build JSON from the fields.
        try
        {
            pdfDocument.Form.ExportToJson(jsonPath);
            Console.WriteLine($"Form data exported to JSON: {jsonPath}");
        }
        catch (MissingMethodException)
        {
            // Fallback – create a simple JSON representation.
            using (var writer = new StreamWriter(jsonPath))
            {
                writer.WriteLine("{");
                bool first = true;
                foreach (var annotation in pdfDocument.Form)
                {
                    if (annotation is Field f)
                    {
                        if (!first) writer.WriteLine(",");
                        string value = f.Value?.ToString()?.Replace("\\", "\\\\").Replace("\"", "\\\"") ?? string.Empty;
                        writer.Write($"  \"{f.PartialName}\": \"{value}\"");
                        first = false;
                    }
                }
                writer.WriteLine();
                writer.WriteLine("}");
            }
            Console.WriteLine($"Form data exported (fallback) to JSON: {jsonPath}");
        }

        // -----------------------------------------------------------------
        // 5. Save the modified PDF.
        // -----------------------------------------------------------------
        pdfDocument.Save(outputPdf);
        Console.WriteLine($"Modified PDF saved as: {outputPdf}");
    }
}
