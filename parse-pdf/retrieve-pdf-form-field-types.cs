using System;
using System.IO;
using System.Linq;
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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm object
            Form form = doc.Form;

            // If there are no form fields, inform the user
            if (form == null || form.Fields == null || form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine($"Found {form.Fields.Count()} form field(s):");

            // Enumerate each field via the Form.Fields collection
            foreach (Field field in form.Fields)
            {
                // Retrieve the runtime type of the field
                Type fieldType = field.GetType();

                // Output the field name and its type for classification
                // PartialName and FullName are properties; if they are methods in a specific version, add () accordingly.
                Console.WriteLine($"- Field Name: {field.PartialName}");
                Console.WriteLine($"  Full Name : {field.FullName}");
                Console.WriteLine($"  Type      : {fieldType.FullName}");
            }
        }
    }
}
