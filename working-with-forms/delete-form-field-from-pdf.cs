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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, delete the specified form field, and save
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;
            // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
            Field? field = form[fieldName] as Field;
            if (field != null)
            {
                // Remove the field from the form
                form.Delete(fieldName);
                Console.WriteLine($"Form field '{fieldName}' deleted.");
            }
            else
            {
                Console.WriteLine($"Form field '{fieldName}' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
