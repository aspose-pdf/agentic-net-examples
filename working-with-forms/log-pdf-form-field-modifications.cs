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
        const string logPath = "field_modifications.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Start with a clean log file
        File.WriteAllText(logPath, string.Empty);

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPdf))
        {
            // Access the form object
            Form form = doc.Form;

            // Iterate over each field in the form
            foreach (Field field in form)
            {
                // Log the original value of the field
                string originalValue = field.Value?.ToString() ?? "(null)";
                Log(logPath, $"Field '{field.FullName}' original value: {originalValue}");

                // Example modification: if the field is a text box, set a new value
                if (field is TextBoxField txtField)
                {
                    txtField.Value = "Updated value";
                    Log(logPath, $"Field '{field.FullName}' new value set to: {txtField.Value}");
                }

                // Example modification: make the field read‑only
                bool originalReadOnly = field.ReadOnly;
                field.ReadOnly = true;
                Log(logPath, $"Field '{field.FullName}' ReadOnly changed from {originalReadOnly} to {field.ReadOnly}");
            }

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modifications logged to '{logPath}'. PDF saved to '{outputPdf}'.");
    }

    // Helper method to append a timestamped entry to the log file
    static void Log(string filePath, string message)
    {
        string line = $"{DateTime.UtcNow:O} - {message}{Environment.NewLine}";
        File.AppendAllText(filePath, line);
    }
}