using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Footer"
            // The Form indexer returns a WidgetAnnotation; cast it to Field.
            Field field = doc.Form["Footer"] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("Field 'Footer' not found or is not a form field.");
                return;
            }

            // The Footer field is expected to be a text box (TextBoxField)
            if (field is TextBoxField textBox)
            {
                // Load an italic version of Arial using FontRepository overload that accepts FontStyles
                Aspose.Pdf.Text.Font italicFont = FontRepository.FindFont("Arial", FontStyles.Italic);

                // Apply the italic font via DefaultAppearance (size 12, black text color)
                // DefaultAppearance expects System.Drawing.Color for the text colour.
                textBox.DefaultAppearance = new DefaultAppearance(italicFont, 12, System.Drawing.Color.Black);

                // Set a light gray background – Characteristics.Background expects System.Drawing.Color
                textBox.Characteristics.Background = System.Drawing.Color.LightGray;
            }
            else
            {
                Console.Error.WriteLine("Field 'Footer' is not a text box field and cannot be styled as requested.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer field styled and saved to '{outputPath}'.");
    }
}
