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
        const string fieldName = "MyTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation. Use it only to check existence.
            var annotation = doc.Form[fieldName];
            if (annotation != null)
            {
                // Delete the field by name – this removes the underlying WidgetAnnotation
                // from the AcroForm collection.
                doc.Form.Delete(fieldName);
                Console.WriteLine($"Field '{fieldName}' removed.");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found; no removal performed.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
