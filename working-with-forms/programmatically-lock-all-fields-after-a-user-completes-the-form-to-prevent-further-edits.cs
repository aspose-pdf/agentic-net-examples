using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
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
            Form pdfForm = doc.Form;

            // Set each field to read‑only using the Form.Fields collection
            foreach (Field field in pdfForm.Fields)
            {
                field.ReadOnly = true;
            }

            // Optionally flatten the form so fields become part of the page content
            // pdfForm.Flatten();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All form fields locked and saved to '{outputPath}'.");
    }
}
