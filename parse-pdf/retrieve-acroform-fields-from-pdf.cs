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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm of the document
            Form acroForm = doc.Form;

            // Output the total number of form fields
            Console.WriteLine($"AcroForm fields count: {acroForm.Count}");

            // Iterate over each widget annotation in the form
            foreach (WidgetAnnotation widget in acroForm)
            {
                // Cast to Field to access field‑specific properties
                if (widget is Field field)
                {
                    Console.WriteLine($"Field Full Name : {field.FullName}");
                    Console.WriteLine($"Partial Name    : {field.PartialName}");
                    Console.WriteLine($"Value           : {field.Value}");
                    Console.WriteLine($"ReadOnly        : {field.ReadOnly}");
                    Console.WriteLine();
                }
            }

            // Alternative way: use the Fields array
            // Field[] fieldsArray = acroForm.Fields;
            // foreach (Field f in fieldsArray) { /* process f */ }
        }
    }
}