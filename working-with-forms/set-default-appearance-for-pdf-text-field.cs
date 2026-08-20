using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "MyTextField"; // name of the field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the field by name (cast to the appropriate field type)
            TextBoxField field = form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                return;
            }

            // Create a DefaultAppearance instance (font name, size, and color)
            // DefaultAppearance expects a System.Drawing.Color, so we use the fully‑qualified type to avoid ambiguity with Aspose.Pdf.Color.
            DefaultAppearance appearance = new DefaultAppearance(
                "Helvetica",   // font name
                12,             // font size
                System.Drawing.Color.Blue); // text color (System.Drawing)

            // Assign the appearance to the field
            field.DefaultAppearance = appearance;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field default appearance updated and saved to '{outputPath}'.");
    }
}
