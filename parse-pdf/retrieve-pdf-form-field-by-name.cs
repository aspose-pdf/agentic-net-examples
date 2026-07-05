using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string fieldName = "MyTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle handled by using)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name using the Form indexer.
            // The indexer returns a WidgetAnnotation, so cast it to Aspose.Pdf.Forms.Field.
            Field? formField = doc.Form[fieldName] as Field;

            if (formField != null)
            {
                Console.WriteLine($"Field '{fieldName}' found. Current value: {formField.Value}");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found in the document.");
            }
        }
    }
}