using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, delete the specified form field, and save the result
        using (Document doc = new Document(inputPath))
        {
            // Delete the form field named "OldPhoneNumber"
            doc.Form.Delete("OldPhoneNumber");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field 'OldPhoneNumber' deleted. Saved to '{outputPath}'.");
    }
}