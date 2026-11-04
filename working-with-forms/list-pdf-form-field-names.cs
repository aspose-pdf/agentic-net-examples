using System;
using System.IO;
using System.Linq; // Needed for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Provides the Field class

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

        // Document must be wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check whether the PDF contains any form fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine("Form field names:");
            // Enumerate all fields and output their names
            foreach (Field field in doc.Form.Fields)
            {
                // Field.Name holds the name of the form field
                Console.WriteLine($"- {field.Name}");
            }
        }
    }
}
