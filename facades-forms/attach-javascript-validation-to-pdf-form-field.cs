using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the FormEditor facade on the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // JavaScript to display a warning if the entered age is less than 18
            string jsCode = "if (event.value < 18) app.alert('Age must be at least 18 years old');";

            // Attach the JavaScript to the field named "Age"
            // Note: SetFieldScript works for push‑button fields, but it can also be used to
            // attach a script to a generic field in this context.
            formEditor.SetFieldScript("Age", jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPath}");
    }
}