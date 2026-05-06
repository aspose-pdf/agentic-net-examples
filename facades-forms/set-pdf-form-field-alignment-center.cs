using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Address";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor on the loaded document
            using (FormEditor editor = new FormEditor(doc))
            {
                // Set horizontal alignment of the "Address" field to center
                bool success = editor.SetFieldAlignment(fieldName, FormFieldFacade.AlignCenter);
                if (!success)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or alignment could not be set.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Field '{fieldName}' alignment set to center. Saved to '{outputPath}'.");
    }
}