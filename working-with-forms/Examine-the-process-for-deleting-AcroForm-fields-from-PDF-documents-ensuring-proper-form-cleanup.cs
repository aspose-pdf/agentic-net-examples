using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete specific AcroForm fields by name
            // The Delete(string) overload removes the field from the form and its annotation from the page
            doc.Form.Delete("FirstName");
            doc.Form.Delete("LastName");
            doc.Form.Delete("Email");

            // Optionally, remove all remaining form fields and flatten their appearances onto the page
            // This ensures no stray form annotations remain
            doc.Form.Flatten();

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"AcroForm fields deleted and document saved to '{outputPath}'.");
    }
}