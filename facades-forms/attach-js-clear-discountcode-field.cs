using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facade API for form editing

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF containing the form
        const string outputPath = "output.pdf";         // Resulting PDF with JavaScript

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // JavaScript that clears the field when it receives focus
                string jsCode = "event.target.value='';";

                // Attach the script to the field named "DiscountCode"
                // AddFieldScript works for any field; it adds the script as an additional action
                bool scriptAdded = formEditor.AddFieldScript("DiscountCode", jsCode);

                if (!scriptAdded)
                {
                    Console.Error.WriteLine("Failed to add JavaScript to the field 'DiscountCode'.");
                }

                // Save the modified document; FormEditor.Save handles the underlying Document.Save
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}