using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "MyTextField"; // name of the field to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation.  Use it only to test existence
            // and then delete the field by name (or cast to Field if you need field‑specific members).
            var annotation = doc.Form[fieldName];

            if (annotation != null)
            {
                // Remove the field from the form using its name
                doc.Form.Delete(fieldName);
                Console.WriteLine($"Field '{fieldName}' removed.");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
