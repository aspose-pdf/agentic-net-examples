using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_required.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the form fields collection
                var form = doc.Form;

                // Retrieve the specific field (assumed to be a TextBoxField)
                var field = form[fieldName] as TextBoxField;
                if (field == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                }
                else
                {
                    // Mark the field as required; PDF viewers will block submission if left empty
                    field.Required = true;
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Saved PDF with required field to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}