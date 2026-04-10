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
        const string fieldName  = "DiscountCode";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor facade on the loaded document
            using (FormEditor editor = new FormEditor(doc))
            {
                // JavaScript to clear the field when it receives focus
                string js = "event.target.value='';";

                // Attach the script to the specified field
                editor.SetFieldScript(fieldName, js);

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}