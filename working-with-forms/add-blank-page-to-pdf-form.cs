using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_with_blank_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (which may contain form fields)
        using (Document doc = new Document(inputPath))
        {
            // Insert a new blank page at the end of the document.
            // This preserves all existing pages and form fields.
            doc.Pages.Add();

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added. Saved to '{outputPath}'.");
    }
}