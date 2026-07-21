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
            // Ensure the document contains a form
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over each form field (Field) and retrieve its runtime type
            foreach (Field field in form)
            {
                // Get the concrete field type via GetType()
                Type fieldRuntimeType = field.GetType();

                // Output field name and its type for classification
                Console.WriteLine($"Field Name: {field.PartialName}, Type: {fieldRuntimeType.FullName}");
            }
        }
    }
}
