using System;
using System.IO;
using Aspose.Pdf;

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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Remove all AcroForm fields by flattening them onto the page
                doc.Form.Flatten();

                // If you need to delete a specific field by name, uncomment the line below:
                // doc.Form.Delete("FieldName");

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"AcroForm fields removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}