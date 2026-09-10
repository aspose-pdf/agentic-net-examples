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

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm associated with the document
            Form acroForm = doc.Form;

            // Retrieve all fields at the lowest hierarchy level
            Field[] fields = acroForm.Fields;

            Console.WriteLine($"Total AcroForm fields: {fields.Length}");

            // Enumerate each field and output basic information
            foreach (Field field in fields)
            {
                // FullName provides the qualified field name
                // Value holds the current field value (if any)
                Console.WriteLine($"Field Name: {field.FullName}, Type: {field.GetType().Name}, Value: {field.Value}");
            }
        }
    }
}