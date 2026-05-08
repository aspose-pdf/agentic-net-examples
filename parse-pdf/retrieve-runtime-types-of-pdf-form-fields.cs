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
            // Access the form object; it implements ICollection<Field>
            Form form = doc.Form;

            // If there are no form fields, inform the user
            if (form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over each field in the form
            foreach (Field field in form)
            {
                // Retrieve the runtime type of the field
                Type fieldRuntimeType = field.GetType();

                // Output the field's name (PartialName) and its type
                Console.WriteLine($"Field Name: {field.PartialName}");
                Console.WriteLine($"Field Type (runtime): {fieldRuntimeType.FullName}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
