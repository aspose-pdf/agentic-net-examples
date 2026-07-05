using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string checkboxFieldName = "AgreeTerms";
        bool shouldCheck = true;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the field safely – the Form indexer returns a WidgetAnnotation,
            // so we cast it to Aspose.Pdf.Forms.Field (or null if the cast fails).
            Field field = form[checkboxFieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{checkboxFieldName}' not found or is not a form field.");
                return;
            }

            // Ensure the field is a checkbox
            if (field is CheckboxField checkbox)
            {
                // Set the checkbox state (Checked property)
                checkbox.Checked = shouldCheck; // true => checked, false => unchecked
            }
            else
            {
                Console.Error.WriteLine($"Field '{checkboxFieldName}' is not a checkbox.");
                return;
            }

            // Save the modified PDF (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Checkbox field '{checkboxFieldName}' set to {(shouldCheck ? "checked" : "unchecked")} and saved to '{outputPdfPath}'.");
    }
}
