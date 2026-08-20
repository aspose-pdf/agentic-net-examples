using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // Needed for FieldFlag if used in future extensions

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_version.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the FormEditor facade with the loaded document
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // Add a hidden numeric field named "Version" with initial value "2"
                // Placed at (0,0)-(0,0) so it is not visible on the page
                // FieldType.Text is used for a simple numeric field
                bool added = formEditor.AddField(FieldType.Text, "Version", "2", 1, 0, 0, 0, 0);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the Version field.");
                }
                else
                {
                    // Mark the field as hidden. In recent Aspose.PDF versions the
                    // PropertyFlag enum does not contain a "Hidden" member. The
                    // closest equivalent that prevents the field from appearing in
                    // the UI is the NoExport flag, which hides the field from the
                    // viewer while still keeping it in the form data.
                    formEditor.SetFieldAttribute("Version", PropertyFlag.NoExport);
                }

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with hidden Version field: {outputPath}");
    }
}
