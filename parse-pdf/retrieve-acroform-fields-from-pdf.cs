using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Provides access to AcroForm field types

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm object
            Form acroForm = doc.Form;

            // Iterate over all fields in the form
            foreach (Field field in acroForm.Fields)
            {
                // Output basic information about each field
                Console.WriteLine($"Field Name: {field.FullName}");
                Console.WriteLine($"Field Type: {field.GetType().Name}");
                Console.WriteLine($"Partial Name: {field.PartialName}");
                Console.WriteLine($"---");
            }
        }
    }
}