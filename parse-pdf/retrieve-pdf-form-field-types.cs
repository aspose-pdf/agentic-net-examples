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

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object associated with the document
            Form form = doc.Form;

            // If the document contains no form fields, report and exit
            if (form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Enumerate each field (WidgetAnnotation) in the form
            foreach (WidgetAnnotation field in form)
            {
                // Retrieve the runtime type of the field
                Type fieldRuntimeType = field.GetType();

                // Output the field's full name and its concrete type
                Console.WriteLine($"Field '{field.FullName}' is of type: {fieldRuntimeType.FullName}");
            }
        }
    }
}