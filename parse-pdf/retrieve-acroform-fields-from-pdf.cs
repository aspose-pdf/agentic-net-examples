using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Contains the Field classes

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
            // Check if the document contains an AcroForm with fields
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Length == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
                return;
            }

            // Iterate over all fields in the form
            foreach (Field field in doc.Form.Fields)
            {
                // Output basic information about each field
                Console.WriteLine($"Field Name : {field.FullName}");
                Console.WriteLine($"  Type     : {field.GetType().Name}");
                Console.WriteLine($"  Value    : {field.Value}");
                Console.WriteLine($"  ReadOnly : {field.ReadOnly}");
                Console.WriteLine();
            }
        }
    }
}
