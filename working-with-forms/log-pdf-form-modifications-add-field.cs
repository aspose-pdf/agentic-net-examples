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
        const string logPath = "form_modifications.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the log file (append mode) to record each modification.
        using (StreamWriter log = new StreamWriter(logPath, append: true))
        {
            log.WriteLine($"--- Modification session started: {DateTime.Now} ---");

            // Load the PDF document (lifecycle rule: use using for disposal).
            using (Document doc = new Document(inputPath))
            {
                Form form = doc.Form;

                if (form == null)
                {
                    log.WriteLine("Document does not contain a form.");
                }
                else
                {
                    // Iterate over existing fields and modify their values.
                    foreach (Field field in form)
                    {
                        string oldValue = field.Value?.ToString() ?? string.Empty;
                        string newValue = oldValue + "_modified";

                        field.Value = newValue; // Apply the change.

                        // Log the modification details.
                        log.WriteLine($"Field '{field.FullName}' value changed from '{oldValue}' to '{newValue}'.");
                    }

                    // Add a new text box field on page 1.
                    // Fully qualified rectangle to avoid ambiguity.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

                    TextBoxField newField = new TextBoxField(doc)
                    {
                        Name = "NewTextBox",
                        PartialName = "NewTextBox",
                        Rect = rect,
                        Value = "Initial value"
                    };

                    // Add the field to the form on page 1 (page indexing is 1‑based).
                    form.Add(newField, 1);

                    // Log the addition of the new field.
                    log.WriteLine($"Added new TextBoxField '{newField.FullName}' on page 1 with initial value '{newField.Value}'.");
                }

                // Save the modified PDF (lifecycle rule: save inside using block).
                doc.Save(outputPath);
                log.WriteLine($"Document saved to '{outputPath}'.");
            }

            log.WriteLine($"--- Modification session ended: {DateTime.Now} ---");
        }

        Console.WriteLine("Form modifications completed.");
    }
}