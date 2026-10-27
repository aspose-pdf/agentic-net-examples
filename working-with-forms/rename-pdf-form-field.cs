using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF containing the form
        const string outputPath = "renamed_form.pdf";   // destination PDF
        const string oldFieldName = "CustomerID";       // existing field name
        const string newFieldName = "ClientID";         // new name per naming convention

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field to access field members
            Field? field = doc.Form[oldFieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field \"{oldFieldName}\" not found or is not a form field.");
            }
            else
            {
                // Rename the field – PartialName is the mutable name property
                field.PartialName = newFieldName;
                Console.WriteLine($"Field \"{oldFieldName}\" renamed to \"{newFieldName}\".");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to \"{outputPath}\".");
    }
}