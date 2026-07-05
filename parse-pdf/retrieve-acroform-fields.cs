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
            // Access the AcroForm object
            Form acroForm = doc.Form;

            // Retrieve all fields using the Fields property (array of Field objects)
            Field[] fields = acroForm.Fields;

            Console.WriteLine($"Total AcroForm fields: {fields.Length}");

            foreach (Field field in fields)
            {
                // Example: print the full name and the current value of each field
                Console.WriteLine($"Field Name: {field.FullName}");
                Console.WriteLine($"  Value    : {field.Value}");
                Console.WriteLine($"  Type     : {field.GetType().Name}");
            }
        }
    }
}