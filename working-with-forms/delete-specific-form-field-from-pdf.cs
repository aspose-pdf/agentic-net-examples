using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Path to the source PDF
        const string outputPath = "output.pdf";         // Path for the modified PDF
        const string fieldName  = "MyField";            // Name of the form field to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Delete the form field by its name (Form.Delete(string) overload)
            doc.Form.Delete(fieldName);

            // Save the updated PDF (lifecycle rule: save within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" deleted. Saved to \"{outputPath}\".");
    }
}