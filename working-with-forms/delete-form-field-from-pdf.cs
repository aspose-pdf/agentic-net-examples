using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF, delete the specified form field, and save the result
        using (Document doc = new Document(inputPath))
        {
            // Check if the field exists before attempting deletion
            if (doc.Form.HasField(fieldName))
            {
                doc.Form.Delete(fieldName);
                Console.WriteLine($"Deleted field '{fieldName}'.");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}