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

        // Ensure the Document is disposed properly
        using (Document doc = new Document(inputPath))
        {
            // Delete the specified form field
            doc.Form.Delete(fieldName);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field '{fieldName}' deleted. Saved to '{outputPath}'.");
    }
}