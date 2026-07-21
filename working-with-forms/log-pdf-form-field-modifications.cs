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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the log file for appending audit entries
        using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Access the form object
                Form form = doc.Form;

                // Iterate over all fields in the form
                foreach (Field field in form)
                {
                    // Record the original value of the field
                    string originalValue = field.Value?.ToString() ?? string.Empty;
                    logWriter.WriteLine($"{DateTime.UtcNow:u} - Field '{field.FullName}' original value: '{originalValue}'");

                    // Example modification for a text box field
                    if (field is TextBoxField textBox)
                    {
                        string newValue = "Updated";
                        textBox.Value = newValue; // modify the field

                        // Log the modification
                        logWriter.WriteLine($"{DateTime.UtcNow:u} - Field '{field.FullName}' new value: '{newValue}'");
                    }
                    // Example modification for a check box field
                    else if (field is CheckboxField checkBox)
                    {
                        bool newState = !checkBox.Checked; // toggle the state
                        checkBox.Checked = newState;       // modify the field

                        // Log the modification
                        logWriter.WriteLine($"{DateTime.UtcNow:u} - Field '{field.FullName}' checked set to: {newState}");
                    }
                    // Additional field types can be handled here
                }

                // Save the modified PDF (lifecycle rule: save inside using)
                doc.Save(outputPdf);
                logWriter.WriteLine($"{DateTime.UtcNow:u} - Document saved to '{outputPdf}'");
            }
        }

        Console.WriteLine("Form field modifications have been logged.");
    }
}