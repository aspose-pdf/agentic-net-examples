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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the form object from the document
            Form form = doc.Form;

            // Iterate over all fields in the lowest level of the form hierarchy
            foreach (Field field in form.Fields)
            {
                // Output basic information about each field
                Console.WriteLine($"Partial Name: {field.PartialName}");
                Console.WriteLine($"Full Name   : {field.FullName}");
                Console.WriteLine($"Value       : {field.Value}");
                Console.WriteLine($"ReadOnly    : {field.ReadOnly}");
                Console.WriteLine(new string('-', 30));
            }

            // If you modify fields, you could save the document here:
            // doc.Save("output.pdf");
        }
    }
}