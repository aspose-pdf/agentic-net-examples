using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "OldPhoneNumber";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form and the specific field exist before attempting deletion
            if (doc.Form != null && doc.Form[fieldName] != null)
            {
                // Delete the form field named 'OldPhoneNumber'
                doc.Form.Delete(fieldName);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field '{fieldName}' deleted. Saved to '{outputPath}'.");
    }
}