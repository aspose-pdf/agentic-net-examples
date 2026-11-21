using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // Form field types and Field collection
using Aspose.Pdf.Annotations;   // DefaultAppearance

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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Iterate over all form fields and apply the custom font to text fields only
        foreach (Field field in doc.Form.Fields)
        {
            if (field is TextBoxField textField)
            {
                // Set the default appearance: font name (Arial Bold), size 12, black colour
                // The PDF font naming convention – "Arial,Bold" works for Arial Bold.
                textField.DefaultAppearance = new DefaultAppearance("Arial,Bold", 12, System.Drawing.Color.Black);
            }
        }

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Custom font applied and saved to '{outputPath}'.");
    }
}
