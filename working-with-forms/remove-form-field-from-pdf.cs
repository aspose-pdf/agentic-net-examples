using System;
using System.IO;
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

        using (Document doc = new Document(inputPath))
        {
            // Verify the field exists
            if (doc.Form.HasField(fieldName))
            {
                // Remove the field using the correct API method
                doc.Form.Delete(fieldName);
                // Optionally you can check that the field was removed
                // Console.WriteLine($"Deleted field: {fieldName}");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field removed and saved to '{outputPath}'.");
    }
}