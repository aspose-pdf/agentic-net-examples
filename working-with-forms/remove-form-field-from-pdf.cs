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
        const string fieldName = "myTextBox"; // name of the form field to remove

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
                // Check whether the form and the requested field exist
                bool fieldExists = doc.Form?.Fields?.Any(f => f.Name == fieldName) ?? false;

                if (fieldExists)
                {
                    // Remove the form field by its name
                    doc.Form.Delete(fieldName);
                    Console.WriteLine($"Form field '{fieldName}' removed.");
                }
                else
                {
                    Console.WriteLine($"Form field '{fieldName}' not found.");
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
