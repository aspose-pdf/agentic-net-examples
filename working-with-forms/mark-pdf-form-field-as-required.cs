using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_required.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Specify the name of the field you want to mark as required
            const string fieldName = "TextField1";

            // Check if the field exists in the form
            if (form.HasField(fieldName))
            {
                // Retrieve the field. The Form indexer returns a WidgetAnnotation, so cast it to Field.
                Field? field = form[fieldName] as Field;

                if (field != null)
                {
                    // Mark the field as required; validation will fail if left empty
                    field.Required = true;
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' exists but could not be cast to a form Field object.");
                }
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found in the document.");
            }

            // Save the modified PDF (saving without explicit SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with required field saved to '{outputPath}'.");
    }
}
