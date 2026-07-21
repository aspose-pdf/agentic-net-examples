using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string jsonPath      = "data.json";

        // Verify that the source PDF and JSON files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Error: JSON file not found – {jsonPath}");
            return;
        }

        // Use the Form facade to work with AcroForm fields.
        // The Form class implements IDisposable, so wrap it in a using block.
        using (Form form = new Form())
        {
            // Bind the existing PDF document.
            form.BindPdf(inputPdfPath);

            // Import field values from the JSON stream.
            // Missing fields in the PDF are ignored automatically.
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportJson(jsonStream);
            }

            // Optional: inspect the import result for each field.
            // This can be useful for logging which fields were updated or skipped.
            var importResults = form.ImportResult;
            if (importResults != null)
            {
                foreach (var result in importResults)
                {
                    // result is of type FormImportResult (contains FieldName and Status).
                    Console.WriteLine($"{result.FieldName}: {result.Status}");
                }
            }

            // Save the updated PDF to the specified output path.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
    }
}