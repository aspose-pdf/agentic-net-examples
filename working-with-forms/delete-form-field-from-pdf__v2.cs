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

        // Open the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete the form field by its name
            doc.Form.Delete(fieldName);

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' deleted and saved to '{outputPath}'.");
    }
}