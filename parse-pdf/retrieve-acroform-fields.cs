using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm object
            Form acroForm = doc.Form;

            // Check if any fields exist
            if (acroForm.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found.");
                return;
            }

            Console.WriteLine($"Found {acroForm.Count} AcroForm field(s):");

            // Iterate over the form fields using the collection enumerator
            foreach (WidgetAnnotation widget in acroForm)
            {
                // Cast to Field to access field-specific properties
                if (widget is Field field)
                {
                    // Output field name and its concrete type
                    Console.WriteLine($"- Name: {field.FullName}, Type: {field.GetType().Name}");
                }
            }

            // Alternative approach using the Fields array:
            // Field[] fieldsArray = acroForm.Fields;
            // foreach (Field f in fieldsArray)
            // {
            //     Console.WriteLine($"- Name: {f.FullName}, Type: {f.GetType().Name}");
            // }
        }
    }
}