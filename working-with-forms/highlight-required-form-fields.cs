using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm object
            Form form = doc.Form;

            // Iterate over all form fields
            foreach (Field field in form)
            {
                // Highlight only the fields that are marked as required
                if (field.Required)
                {
                    // Set the background (fill) color of the field
                    field.Color = Aspose.Pdf.Color.Yellow;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields highlighted and saved to '{outputPath}'.");
    }
}