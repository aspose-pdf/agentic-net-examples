using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Report the number of form fields before flattening
                int fieldsBefore = doc.Form.Count;
                Console.WriteLine($"Form fields before flattening: {fieldsBefore}");

                // Flatten the form – this removes all interactive fields and
                // places their current values directly onto the page content.
                doc.Form.Flatten();

                // Verify that the fields have been removed
                int fieldsAfter = doc.Form.Count;
                Console.WriteLine($"Form fields after flattening: {fieldsAfter}");

                // Save the resulting PDF. No SaveOptions are needed because the
                // output format is PDF.
                doc.Save(outputPath);
                Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}