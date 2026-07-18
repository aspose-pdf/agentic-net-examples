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

        // Load the PDF, lock all form fields, and save.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each field in the form and set it to read‑only.
            foreach (Field field in doc.Form)
            {
                field.ReadOnly = true;
            }

            // Alternatively, you could flatten the form to remove interactivity:
            // doc.Form.Flatten();

            doc.Save(outputPath);
        }

        Console.WriteLine($"All fields locked and saved to '{outputPath}'.");
    }
}