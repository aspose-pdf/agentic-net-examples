using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for Border and BorderStyle

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form and the specific field exist
            if (doc.Form != null && doc.Form[fieldName] != null)
            {
                // Cast to a concrete field type (e.g., TextBoxField)
                var field = doc.Form[fieldName] as TextBoxField;
                if (field != null)
                {
                    // Set the border color on the field itself (Border class has no Color property)
                    field.Color = Aspose.Pdf.Color.Black;

                    // Apply a beveled (3‑D) border style using the correct Border class
                    field.Border = new Border(field)
                    {
                        Width = 1,               // optional border width
                        Style = BorderStyle.Beveled // beveled style for a 3‑D look
                    };
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with beveled border to '{outputPath}'.");
    }
}
