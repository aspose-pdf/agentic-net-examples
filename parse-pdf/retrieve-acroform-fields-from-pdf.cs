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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm object of the document
            Form acroForm = doc.Form;

            // If there are no fields, inform the user
            if (acroForm.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found.");
                return;
            }

            Console.WriteLine($"Found {acroForm.Count} AcroForm field(s):");

            // Iterate over the form fields using the collection enumerator
            foreach (Field field in acroForm)
            {
                // Use FullName (qualified name) if available, otherwise fallback to Name
                string fieldName = field.FullName ?? field.Name;
                Console.WriteLine($"- Field: {fieldName}");
            }

            // Alternative way: use the Fields property which returns an array of Field objects
            Field[] fields = acroForm.Fields;
            foreach (Field field in fields)
            {
                // Print each field's full name and its current value
                Console.WriteLine($"Field '{field.FullName}' has value: {field.Value}");
            }
        }
    }
}
