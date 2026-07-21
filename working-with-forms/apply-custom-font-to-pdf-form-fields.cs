using System;
using System.IO;
using System.Drawing; // Required for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for DefaultAppearance
using Aspose.Pdf.Forms;        // for Field handling

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
        using (Document doc = new Document(inputPath))
        {
            // Name of the custom font to apply (must be installed or available to Aspose.Pdf)
            string customFontName = "Arial";

            // Loop through all form fields and set their default appearance
            foreach (Field field in doc.Form.Fields)
            {
                // Apply the custom font, size 12, black color (System.Drawing.Color required)
                field.DefaultAppearance = new DefaultAppearance(customFontName, 12, System.Drawing.Color.Black);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom font applied to all form fields and saved as '{outputPath}'.");
    }
}
