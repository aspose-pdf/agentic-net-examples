using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form_input.pdf";
        const string outputPath = "form_locked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document that contains the interactive form
        using (Document doc = new Document(inputPath))
        {
            // Lock every form field by setting its ReadOnly property to true
            foreach (Field field in doc.Form.Fields)
            {
                field.ReadOnly = true;
            }

            // If you want to completely remove interactivity, uncomment the next line:
            // doc.Form.Flatten();

            // Save the updated PDF; since the extension is .pdf, no SaveOptions are required
            doc.Save(outputPath);
        }

        Console.WriteLine($"All form fields have been locked and saved to '{outputPath}'.");
    }
}
