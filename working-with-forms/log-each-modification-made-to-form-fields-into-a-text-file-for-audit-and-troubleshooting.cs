using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logPath = "form_modifications.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the log file once and keep it open for the whole operation
        using (StreamWriter log = new StreamWriter(logPath, append: true))
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPdf))
            {
                // -------------------------------------------------
                // Example 1: Modify an existing field's value
                // -------------------------------------------------
                const string existingFieldName = "CustomerName";
                if (doc.Form.HasField(existingFieldName))
                {
                    // Retrieve the field – the Form indexer returns a WidgetAnnotation,
                    // so we must cast it to Aspose.Pdf.Forms.Field.
                    Field field = doc.Form[existingFieldName] as Field;
                    if (field != null)
                    {
                        // Capture old value
                        string oldValue = field.Value?.ToString() ?? string.Empty;

                        // Define new value
                        string newValue = "John Doe";

                        // Apply the change
                        field.Value = newValue;

                        // Log the modification
                        log.WriteLine($"{DateTime.UtcNow:u} | Field '{existingFieldName}' | Value changed from '{oldValue}' to '{newValue}'");
                    }
                }

                // -------------------------------------------------
                // Example 2: Add a new text box field
                // -------------------------------------------------
                const string newFieldName = "NewComment";

                // Create a new TextBoxField instance
                TextBoxField newField = new TextBoxField(doc)
                {
                    PartialName = newFieldName,
                    Value = "Initial comment",
                    // Position the field on page 1 (left, bottom, right, top)
                    Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530)
                };

                // Add the field to the form on page 1
                doc.Form.Add(newField, 1);

                // Log the addition
                log.WriteLine($"{DateTime.UtcNow:u} | Field '{newFieldName}' | Added with initial value '{newField.Value}' on page 1");

                // -------------------------------------------------
                // Example 3: Change a property (ReadOnly) of an existing field
                // -------------------------------------------------
                const string readOnlyFieldName = "Agreement";
                if (doc.Form.HasField(readOnlyFieldName))
                {
                    Field roField = doc.Form[readOnlyFieldName] as Field;
                    if (roField != null)
                    {
                        bool oldReadOnly = roField.ReadOnly;
                        roField.ReadOnly = true;
                        log.WriteLine($"{DateTime.UtcNow:u} | Field '{readOnlyFieldName}' | ReadOnly changed from '{oldReadOnly}' to 'true'");
                    }
                }

                // -------------------------------------------------
                // Save the modified document (lifecycle rule: save inside using)
                // -------------------------------------------------
                doc.Save(outputPdf);
                log.WriteLine($"{DateTime.UtcNow:u} | Document saved to '{outputPdf}'");
            }
        }

        Console.WriteLine($"Form modifications completed. Log written to '{logPath}'.");
    }
}
