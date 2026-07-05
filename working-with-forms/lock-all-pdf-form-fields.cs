using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "locked_output.pdf";

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

            // Iterate over all fields and set them to read‑only
            foreach (Field field in form)
            {
                field.ReadOnly = true;
            }

            // Optionally flatten the form to remove interactive elements completely
            // form.Flatten(); // Uncomment if you want to permanently embed values

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All fields locked and saved to '{outputPath}'.");
    }
}