using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;   // Form and Field classes are defined here

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
            // Access the AcroForm of the document
            Form form = doc.Form;

            // Output the total number of form fields
            Console.WriteLine($"Total form fields: {form.Count}");

            // Iterate over all fields in the lowest level of the hierarchy
            foreach (Field field in form.Fields)
            {
                // Basic properties of each field
                Console.WriteLine("-----");
                Console.WriteLine($"Name           : {field.Name}");
                Console.WriteLine($"FullName       : {field.FullName}");
                Console.WriteLine($"PartialName    : {field.PartialName}");
                Console.WriteLine($"Value          : {field.Value}");
                Console.WriteLine($"ReadOnly       : {field.ReadOnly}");
                Console.WriteLine($"Required       : {field.Required}");
                Console.WriteLine($"AlternateName  : {field.AlternateName}");
                Console.WriteLine($"MappingName    : {field.MappingName}");
                Console.WriteLine($"Exportable     : {field.Exportable}");
                Console.WriteLine($"TabOrder       : {field.TabOrder}");
                Console.WriteLine($"Rect (LLX,LLY,URX,URY): {field.Rect.LLX}, {field.Rect.LLY}, {field.Rect.URX}, {field.Rect.URY}");
            }

            // No modifications are made, so no need to save.
            // If you wanted to persist changes, you would call doc.Save("output.pdf");
        }
    }
}