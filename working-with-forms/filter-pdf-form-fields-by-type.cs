using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all form fields from the document
            Field[] allFields = doc.Form.Fields;

            // LINQ query to filter only TextBoxField instances
            var textBoxFields = allFields.OfType<TextBoxField>().ToList();

            // Example: output the full names of the filtered text box fields
            foreach (var tb in textBoxFields)
            {
                Console.WriteLine($"TextBox field: {tb.FullName}");
            }

            // Optional: set a sample value for each text box field
            foreach (var tb in textBoxFields)
            {
                tb.Value = "Sample text";
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}