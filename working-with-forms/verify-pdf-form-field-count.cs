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
        const int expectedFieldCount = 5; // expected number of form fields

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Verify the number of form fields
                int actualCount = doc.Form.Count;
                Console.WriteLine($"Form fields found: {actualCount}");

                if (actualCount != expectedFieldCount)
                {
                    Console.Error.WriteLine($"Unexpected number of form fields. Expected {expectedFieldCount}, but found {actualCount}.");
                    return;
                }

                // Example processing: set a value for a field named "Name" if it exists
                if (doc.Form.HasField("Name"))
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field to access the Value property.
                    Field? field = doc.Form["Name"] as Field;
                    if (field != null)
                    {
                        field.Value = "John Doe";
                    }
                }

                // Save the processed document
                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
