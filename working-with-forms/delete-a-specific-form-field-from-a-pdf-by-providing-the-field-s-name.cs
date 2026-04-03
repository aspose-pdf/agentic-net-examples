using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "MyField"; // name of the form field to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Delete the form field by its name
            doc.Form.Delete(fieldName);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' deleted. Saved to '{outputPath}'.");
    }
}