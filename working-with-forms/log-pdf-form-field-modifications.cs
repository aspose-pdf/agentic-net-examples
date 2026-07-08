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
        const string logFile = "form_modifications.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            Form form = doc.Form;

            // Open the log file (append mode) and record each change
            using (StreamWriter log = new StreamWriter(logFile, append: true))
            {
                // 1. Modify the value of an existing field
                const string existingFieldName = "CustomerName";
                if (form.HasField(existingFieldName))
                {
                    // The Form indexer returns a WidgetAnnotation; cast to Field
                    Field? field = form[existingFieldName] as Field;
                    if (field != null)
                    {
                        string oldValue = field.Value?.ToString() ?? string.Empty;
                        string newValue = "John Doe";
                        field.Value = newValue;
                        log.WriteLine($"{DateTime.UtcNow:u} Set Value - Field: {existingFieldName}, Old: '{oldValue}', New: '{newValue}'");
                    }
                }

                // 2. Add a new text box field to page 1
                TextBoxField newField = new TextBoxField(doc)
                {
                    PartialName = "NewComment",
                    Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550),
                    Value = "Initial comment"
                };
                form.Add(newField, 1); // page numbers are 1‑based
                log.WriteLine($"{DateTime.UtcNow:u} Add Field - Name: {newField.PartialName}, Page: 1, Rect: {newField.Rect}");

                // 3. Delete an existing field
                const string fieldToDelete = "ObsoleteField";
                if (form.HasField(fieldToDelete))
                {
                    form.Delete(fieldToDelete);
                    log.WriteLine($"{DateTime.UtcNow:u} Delete Field - Name: {fieldToDelete}");
                }

                // 4. Change the ReadOnly flag of a field
                const string roFieldName = "Agreement";
                if (form.HasField(roFieldName))
                {
                    Field? roField = form[roFieldName] as Field;
                    if (roField != null)
                    {
                        bool oldFlag = roField.ReadOnly;
                        roField.ReadOnly = false;
                        log.WriteLine($"{DateTime.UtcNow:u} Set ReadOnly - Field: {roFieldName}, Old: {oldFlag}, New: false");
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine("Form modifications have been logged and the PDF saved.");
    }
}
