using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - input PDF path
        // args[1] - output PDF path
        // args[2] - name of the radio button field
        // args[3] - value of the option to select
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <outputPdf> <radioFieldName> <optionValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fieldName = args[2];
        string optionValue = args[3];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                Field? field = doc.Form[fieldName] as Field;
                if (field == null)
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a form field.");
                    return;
                }

                // Ensure the field is a radio button
                if (field is not RadioButtonField radio)
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' is not a radio button.");
                    return;
                }

                // Set the desired option. Aspose.Pdf will ignore an invalid value, so we set it directly.
                radio.Value = optionValue;

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Radio button '{fieldName}' set to '{optionValue}'. Saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
