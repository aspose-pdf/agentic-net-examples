using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(inputPath);

            // Access the form fields collection
            var form = doc.Form;

            // Get the source field (assumed to be a text box)
            TextBoxField sourceField = form["SourceNotes"] as TextBoxField;
            if (sourceField == null)
            {
                Console.Error.WriteLine("Source field 'SourceNotes' not found or is not a text box field.");
                return;
            }

            // Get the target field (assumed to be a text box)
            TextBoxField targetField = form["TargetNotes"] as TextBoxField;
            if (targetField == null)
            {
                Console.Error.WriteLine("Target field 'TargetNotes' not found or is not a text box field.");
                return;
            }

            // Copy the inner content/value
            targetField.Value = sourceField.Value;

            // Save the updated PDF
            doc.Save(outputPath);

            Console.WriteLine($"Content copied from 'SourceNotes' to 'TargetNotes'. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}