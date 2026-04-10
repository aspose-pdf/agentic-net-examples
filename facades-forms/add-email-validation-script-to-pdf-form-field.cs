using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the FormEditor facade on the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // JavaScript that validates the Email field using a simple regex.
                // It runs when the field loses focus; if the value does not match,
                // an alert is shown and the field entry is rejected.
                string emailValidationScript = @"
if (event.value.match(/^\\S+@\\S+\\.\\S+$/) == null) {
    app.alert('Invalid email address');
    event.rc = false;
}";

                // Attach the script to the field named "Email"
                formEditor.AddFieldScript("Email", emailValidationScript);

                // Save the updated PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Email field validation added. Saved to '{outputPath}'.");
    }
}