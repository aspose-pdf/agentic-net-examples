using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled_flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assign values to form fields by their names using the Field abstraction
            if (doc.Form.HasField("Name"))
            {
                // The Form indexer returns a WidgetAnnotation; cast to Field to access Value
                if (doc.Form["Name"] is Field nameField)
                {
                    nameField.Value = "John Doe"; // Text field expects a string
                }
            }

            if (doc.Form.HasField("Agree"))
            {
                if (doc.Form["Agree"] is Field agreeField)
                {
                    // Checkbox field also uses a string value (e.g., "true", "On", or the export value defined in the PDF)
                    agreeField.Value = "true";
                }
            }

            // Flatten the form so that fields become part of the page content and are no longer editable
            doc.Form.Flatten();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
