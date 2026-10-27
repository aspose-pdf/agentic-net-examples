using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            var form = doc.Form;
            // Use LINQ Any() (or Count()) to safely check for the presence of fields
            if (form == null || form.Fields == null || !form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            Console.WriteLine("Form field names:");
            // Enumerate each field and output its name (FullyQualifiedName is not available)
            foreach (var field in form.Fields)
            {
                Console.WriteLine($"- {field.Name}");
            }
        }
    }
}
