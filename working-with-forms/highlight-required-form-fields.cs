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
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields
            foreach (Field field in doc.Form)
            {
                // Mark the field as required (optional)
                field.Required = true;

                // Change the field's background/border color to highlight it
                field.Color = Aspose.Pdf.Color.Yellow;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields highlighted and saved to '{outputPath}'.");
    }
}