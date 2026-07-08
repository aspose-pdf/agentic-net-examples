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
            // Access the AcroForm of the document
            Form form = doc.Form;

            // If there are no form fields, inform the user
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over each field (derived from Aspose.Pdf.Forms.Field) in the form
            foreach (Field field in form)
            {
                // Retrieve the runtime type of the field
                Type fieldRuntimeType = field.GetType();

                // Output the field's partial name and its concrete type
                Console.WriteLine($"Field '{field.PartialName}' is of type: {fieldRuntimeType.FullName}");
            }
        }
    }
}
