using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "MyField"; // name of the form field to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field reference by name – the Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field (or use the safe 'as' operator).
            Field field = doc.Form[fieldName] as Field;

            if (field != null)
            {
                // Remove the field from the form using the Delete method that accepts a Field object
                doc.Form.Delete(field);
                Console.WriteLine($"Field '{fieldName}' removed.");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
