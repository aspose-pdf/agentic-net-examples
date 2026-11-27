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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the interactive form (if any)
            Form form = doc.Form;

            // Check whether the document contains any form fields
            if (form == null || form.Fields == null || form.Fields.Length == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate over each field and output its name
            foreach (Field field in form.Fields)
            {
                // The Name property holds the identifier of the form field
                Console.WriteLine(field.Name);
            }
        }
    }
}
