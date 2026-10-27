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
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Check if the form field exists using the Fields collection
                bool fieldExists = doc.Form?.Fields?.Any(f => f.Name == fieldName) ?? false;

                if (fieldExists)
                {
                    // Delete the form field
                    doc.Form.Delete(fieldName);
                    Console.WriteLine($"Form field '{fieldName}' deleted.");
                }
                else
                {
                    Console.WriteLine($"Form field '{fieldName}' not found.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}