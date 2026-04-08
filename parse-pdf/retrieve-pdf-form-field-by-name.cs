using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "MyField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name using the Form indexer.
            // The indexer returns a WidgetAnnotation, so cast it to Aspose.Pdf.Forms.Field.
            Field? field = doc.Form[fieldName] as Field;

            if (field == null)
            {
                Console.WriteLine($"Field '{fieldName}' not found or is not a form field in the document.");
                return;
            }

            // Display basic information about the field
            Console.WriteLine($"Field Name   : {field.PartialName}");
            Console.WriteLine($"Full Name    : {field.FullName}");
            Console.WriteLine($"Field Value  : {field.Value}");
        }
    }
}