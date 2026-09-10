using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // for DefaultAppearance if needed

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_audited.pdf";
        const string logPath   = "field_modifications.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        // Open the log file inside a using block
        using (StreamWriter logWriter = new StreamWriter(logPath, append: false))
        {
            // Ensure the document actually contains a form
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                doc.Save(outputPdf); // still save the unchanged document
                return;
            }

            // Example modification: set a new value for each text field
            foreach (Field field in form)
            {
                // Store original value for logging
                string originalValue = field.Value?.ToString() ?? "(null)";

                // Perform a sample modification based on field type
                // Here we simply set the value to "Test" for text fields
                // and toggle the ReadOnly flag for all fields.
                try
                {
                    // Set a new value (if the field supports it)
                    field.Value = "Test";

                    // Toggle the ReadOnly flag
                    field.ReadOnly = !field.ReadOnly;

                    // Log the modification
                    logWriter.WriteLine($"{DateTime.UtcNow:u} | Field: {field.FullName} | OriginalValue: {originalValue} | NewValue: {field.Value} | ReadOnly: {field.ReadOnly}");
                }
                catch (Exception ex)
                {
                    // Log any errors that occur while modifying a field
                    logWriter.WriteLine($"{DateTime.UtcNow:u} | Field: {field.FullName} | Error: {ex.Message}");
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modifications logged to '{logPath}'. Modified PDF saved as '{outputPdf}'.");
    }
}