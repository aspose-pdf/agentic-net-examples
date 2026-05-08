using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // PDF containing the form field
        const string outputPath = "output.pdf";  // PDF with dashed border on the field
        const string fieldName  = "OptionalField"; // name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name. Cast to the appropriate field type if needed.
            // Most form fields inherit from Annotation, so Border can be set directly.
            var field = doc.Form[fieldName] as Annotation;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
                return;
            }

            // Create a Border object for the field (Border requires the parent annotation)
            // Set the style to Dashed and define a width (e.g., 1 point).
            field.Border = new Border(field)
            {
                Style = BorderStyle.Dashed,
                Width = 1
                // Optional: customize dash pattern via field.Border.Dash = new Dash(3, 2);
            };

            // Save the modified PDF (lifecycle rule: save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with dashed border on field '{fieldName}' to '{outputPath}'.");
    }
}