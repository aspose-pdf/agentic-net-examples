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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Retrieve all fields in the lowest level of the hierarchical form
            Field[] allFields = doc.Form.Fields;

            // LINQ query to filter only TextBoxField instances
            var textBoxFields = allFields
                .OfType<TextBoxField>()               // filter by type
                .Select(f => new
                {
                    Name = f.Name,
                    FullName = f.FullName,
                    Value = f.Value
                })
                .ToList();

            // Output the filtered text box fields
            Console.WriteLine($"Found {textBoxFields.Count} text box field(s):");
            foreach (var tb in textBoxFields)
            {
                Console.WriteLine($"Name: {tb.Name}, FullName: {tb.FullName}, Value: {tb.Value}");
            }

            // (Optional) Save the document if any modifications were made
            // doc.Save("output.pdf"); // Uncomment if you need to save changes
        }
    }
}