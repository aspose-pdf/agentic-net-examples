using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF containing the form
        const string outputPath = "output_centered.pdf"; // Resulting PDF
        const string fieldName = "myTextField";        // Name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field (or a derived type).
            Field? field = doc.Form[fieldName] as Field;
            if (field is TextBoxField textBox)
            {
                // Set horizontal alignment to center
                textBox.TextHorizontalAlignment = HorizontalAlignment.Center;

                // Optionally, also center vertically if desired
                // textBox.TextVerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                Console.Error.WriteLine($"Text box field '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved centered text field PDF to '{outputPath}'.");
    }
}
