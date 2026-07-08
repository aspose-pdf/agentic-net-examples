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
        const string fieldValue = "Acme Corporation";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a Form facade bound to the loaded document
            Form form = new Form(doc);

            // Fill the specified AcroForm text field
            bool success = form.FillField(fieldName, fieldValue);
            if (!success)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" not found or could not be filled.");
            }

            // Save the modified PDF to the output path
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" filled with \"{fieldValue}\" and saved to \"{outputPath}\".");
    }
}