using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example: fill a form field named "Name" (if it exists)
            Form form = new Form(doc);
            if (Array.Exists(form.FieldNames, name => name == "Name"))
            {
                form.FillField("Name", "John Doe");
            }

            // Save the document incrementally.
            // The parameterless Save() method writes changes as an incremental update,
            // preserving previous revisions (including earlier form data).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with incremental updates to '{outputPath}'.");
    }
}