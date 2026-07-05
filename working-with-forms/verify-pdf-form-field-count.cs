using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // needed for Field type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int expectedFieldCount = 5; // set the expected number of form fields

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
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

                // Example processing: set a value for a known field (optional)
                if (doc.Form.HasField("Name"))
                {
                    // The indexer returns a Field (or a derived widget). Cast to Field to access the Value property.
                    Field field = doc.Form["Name"] as Field;
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
