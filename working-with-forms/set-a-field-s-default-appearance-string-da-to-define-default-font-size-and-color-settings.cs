using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // DefaultAppearance class
using Aspose.Pdf.Forms;        // Access form fields

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myField"; // name of the field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form collection
            Form form = doc.Form;

            // Retrieve the field by name from the form's Fields collection
            Field field = null;
            foreach (Field f in form.Fields)
            {
                if (f.PartialName == fieldName)
                {
                    field = f;
                    break;
                }
            }

            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Set the default appearance (font, size, color) using System.Drawing.Color
            // DefaultAppearance(string fontName, double fontSize, System.Drawing.Color textColor)
            field.DefaultAppearance = new DefaultAppearance(
                "Helvetica",               // font name
                12,                         // font size
                System.Drawing.Color.Blue  // System.Drawing.Color required by the constructor
            );

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field default appearance updated and saved to '{outputPath}'.");
    }
}
