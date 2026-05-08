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
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field by its name – the Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field before we can use Form methods.
            Field? field = doc.Form[fieldName] as Field;

            if (field != null)
            {
                // Remove the field using the Delete(Field) overload (or Remove if preferred).
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
