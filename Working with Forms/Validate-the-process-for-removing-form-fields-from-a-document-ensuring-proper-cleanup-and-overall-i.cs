using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Example: delete a specific field by name if it exists
            const string fieldToDelete = "CustomerName";
            if (form.HasField(fieldToDelete))
            {
                form.Delete(fieldToDelete);
                Console.WriteLine($"Deleted field: {fieldToDelete}");
            }

            // Example: delete all fields by flattening the form
            // This places field values directly on the page and removes the widgets
            form.Flatten();
            Console.WriteLine("Form flattened – all fields removed.");

            // Validate and repair the document structure after modifications
            bool repaired = doc.Check(doRepair: true);
            Console.WriteLine(repaired ? "Document repaired after check." : "No repairs needed.");

            // Save the cleaned PDF
            doc.Save(outputPath);
            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
    }
}