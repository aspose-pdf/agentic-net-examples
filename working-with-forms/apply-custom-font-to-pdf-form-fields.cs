using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Apply a custom font (Arial, size 12, black color) to the field.
                // DefaultAppearance is set via its constructor because the Font property is read‑only.
                field.DefaultAppearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom font applied to all form fields. Saved as '{outputPath}'.");
    }
}