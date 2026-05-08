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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a FormEditor to modify form fields (FormEditor implements IDisposable)
            using (FormEditor editor = new FormEditor(doc))
            {
                // Set a maximum of 50 characters for the field named "CustomerName"
                editor.SetFieldLimit("CustomerName", 50);
            }

            // Save the modified document (save without explicit SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPath}'.");
    }
}