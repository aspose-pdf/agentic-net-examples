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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each form field using foreach (Form implements IEnumerable<Field>)
            foreach (Field field in doc.Form)
            {
                // Example processing: output field name and current value
                Console.WriteLine($"Field Name : {field.FullName ?? field.Name}");
                Console.WriteLine($"Field Value: {field.Value?.ToString() ?? "<empty>"}");

                // If the field is a text box, set a new value (demonstrates per‑field handling)
                if (field is TextBoxField txtField)
                {
                    txtField.Value = "Updated value";
                }
            }

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields processed and saved to '{outputPath}'.");
    }
}
