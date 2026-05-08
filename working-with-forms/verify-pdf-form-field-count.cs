using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int expectedFieldCount = 5; // adjust to the expected number of form fields

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
        using (Document doc = new Document(inputPath))
        {
            // Verify the number of form fields using Document.Form.Count
            int actualCount = doc.Form.Count;
            Console.WriteLine($"Form fields found: {actualCount}");

            if (actualCount != expectedFieldCount)
            {
                Console.Error.WriteLine($"Unexpected number of form fields. Expected {expectedFieldCount}, but found {actualCount}.");
                return; // abort processing if the count does not match
            }

            // Example processing: set a value for a field named "Name" if it exists
            if (doc.Form.HasField("Name"))
            {
                // Retrieve the field and cast to the appropriate type (TextBoxField in this case)
                var field = doc.Form["Name"] as TextBoxField;
                if (field != null)
                {
                    field.Value = "John Doe";
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}