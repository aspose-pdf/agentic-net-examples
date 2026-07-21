using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document containing form fields
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Iterate over all fields in the form
            foreach (Field field in form)
            {
                // Mark the field as required (optional, based on requirement)
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