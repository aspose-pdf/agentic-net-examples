using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for Border and BorderStyle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "OptionalField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the form field by its name; cast to the appropriate type
                var field = doc.Form[fieldName] as TextBoxField;
                if (field == null)
                {
                    Console.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                }
                else
                {
                    // Set the border color via the annotation's Color property
                    field.Color = Aspose.Pdf.Color.Gray;

                    // Configure the border: width and dashed style
                    field.Border = new Border(field)
                    {
                        Width = 1,
                        Style = BorderStyle.Dashed
                    };
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
