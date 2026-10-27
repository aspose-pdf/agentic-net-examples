using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "MyTextField"; // name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has an AcroForm (Form) object
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the document.");
                return;
            }

            // Retrieve the target field; cast to TextBoxField (or appropriate field type)
            TextBoxField field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a TextBoxField.");
                return;
            }

            // Set the Default Appearance (DA) for the field.
            // Fully qualify the System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color.
            DefaultAppearance da = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);
            field.DefaultAppearance = da;

            // Optionally, set the field's default value to see the appearance in action.
            field.Value = "Sample text";

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' updated and saved to '{outputPath}'.");
    }
}
