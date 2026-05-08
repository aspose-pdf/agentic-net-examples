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
            // Initialize the FormEditor facade with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // JavaScript that shows a warning if the entered age is less than 18
                string js = "if (event.value < 18) app.alert('You must be at least 18 years old');";

                // Attach the script to the field named "Age"
                bool attached = formEditor.SetFieldScript("Age", js);
                if (!attached)
                {
                    Console.Error.WriteLine("Failed to attach JavaScript to the 'Age' field.");
                }

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPath}'.");
    }
}