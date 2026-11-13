using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains a form with fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Length == 0)
            {
                Console.WriteLine("No form fields found.");
                return;
            }

            // Iterate over each form field and output its concrete type
            foreach (Field field in doc.Form.Fields)
            {
                // GetType() returns the specific field class (e.g., TextBoxField, CheckBoxField)
                Type fieldType = field.GetType();

                // Field.Name may be null; provide a fallback name
                string fieldName = field.Name ?? "(unnamed)";

                Console.WriteLine($"Field '{fieldName}' is of type '{fieldType.FullName}'.");
            }
        }
    }
}
