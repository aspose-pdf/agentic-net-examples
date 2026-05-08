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
            // Get the AcroForm object
            Form form = doc.Form;

            // Check if the document contains any form fields
            if (form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine("Form field names:");

            // Enumerate all fields and output their names
            foreach (Field field in form.Fields)
            {
                // Prefer the short Name; fall back to FullName if Name is empty
                string fieldName = !string.IsNullOrEmpty(field.Name) ? field.Name : field.FullName;
                Console.WriteLine($"- {fieldName}");
            }
        }
    }
}