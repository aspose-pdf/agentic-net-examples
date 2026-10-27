using System;
using System.IO;
using System.Linq; // Needed for Count() extension method
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

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the PDF contains a form
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine("Form fields and their runtime types:");

            // Iterate over each form field and retrieve its runtime type via GetType()
            foreach (Field field in doc.Form.Fields)
            {
                // field.Name gives the field's name; field.GetType() returns the concrete field class
                Type fieldType = field.GetType();

                // Output the field name and its type name for classification
                Console.WriteLine($"- Name: {field.Name}, Type: {fieldType.Name}");
            }
        }
    }
}
