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
        const string fieldName  = "CustomerName";
        const int    maxLength  = 50;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // FormEditor is a Facades class that also implements IDisposable
            using (FormEditor editor = new FormEditor())
            {
                // Bind the editor to the loaded document
                editor.BindPdf(doc);

                // Set a 50‑character limit on the 'CustomerName' text field
                editor.SetFieldLimit(fieldName, maxLength);

                // Save the modified document to a new file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPath}'.");
    }
}