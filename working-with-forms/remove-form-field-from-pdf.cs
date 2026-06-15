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
        const string fieldName = "myField"; // name of the form field to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                Field? field = doc.Form[fieldName] as Field;

                if (field != null)
                {
                    // Remove the field using the Delete overload that accepts a Field instance.
                    doc.Form.Delete(field);
                    Console.WriteLine($"Field '{fieldName}' removed.");
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' not found in the document.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
