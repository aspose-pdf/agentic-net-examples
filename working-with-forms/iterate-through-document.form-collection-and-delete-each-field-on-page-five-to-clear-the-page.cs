using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cleared.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm associated with the document
            var form = doc.Form;

            // Page indices are zero‑based; page 5 => index 4
            var fieldsOnPage5 = form.Fields
                                   .Where(f => f.PageIndex == 4)
                                   .ToList(); // snapshot to allow safe deletion

            // Delete each field that belongs to page 5
            foreach (var field in fieldsOnPage5)
            {
                form.Delete(field);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All form fields on page 5 have been removed. Saved to '{outputPath}'.");
    }
}