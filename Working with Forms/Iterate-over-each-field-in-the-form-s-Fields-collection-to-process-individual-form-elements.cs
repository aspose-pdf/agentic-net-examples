using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_processed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;

            // Iterate over each field in the form's Fields collection
            foreach (Field field in form.Fields)
            {
                // Example processing: output field name and current value
                Console.WriteLine($"Field: {field.FullName}, Value: {field.Value}");

                // Example: set a default value if the field is empty
                if (field.Value == null || string.IsNullOrEmpty(field.Value.ToString()))
                {
                    field.Value = "Default";
                }
            }

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}