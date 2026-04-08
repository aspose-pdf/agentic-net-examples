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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object of the document
            Form form = doc.Form;

            // If there are no fields, inform the user
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over each form field via the Fields collection
            foreach (Field field in form.Fields)
            {
                // Retrieve the runtime .NET type of the field
                Type fieldRuntimeType = field.GetType();

                // Output the field's partial name and its type for classification
                Console.WriteLine($"Field Name: {field.PartialName}, Type: {fieldRuntimeType.FullName}");
            }
        }
    }
}
