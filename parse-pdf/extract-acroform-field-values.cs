using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file that contains an AcroForm.
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into a Document object.
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Form facade on the loaded document.
            using (Form form = new Form(doc))
            {
                Console.WriteLine("AcroForm fields and their values:");

                // Iterate over all field names present in the form.
                foreach (string fieldName in form.FieldNames)
                {
                    // Retrieve the value of the current field.
                    object fieldValue = form.GetField(fieldName);

                    // Output the field name and its value (null is displayed as empty).
                    Console.WriteLine($"{fieldName}: {fieldValue ?? string.Empty}");
                }
            }
        }
    }
}