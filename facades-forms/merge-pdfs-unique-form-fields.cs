using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF form files to be processed in batch
        string[] inputFiles = new string[]
        {
            "Form1.pdf",
            "Form2.pdf",
            "Form3.pdf"
        };

        // Output PDF after merging with unique field names
        const string outputFile = "MergedUniqueFields.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Enable automatic renaming of duplicate field names
            editor.KeepFieldsUnique = true;
            // Suffix template: "_%NUM%" will become _1, _2, _3, etc.
            editor.UniqueSuffix = "_%NUM%";

            // Concatenate the PDFs; duplicate field names will be made unique
            editor.Concatenate(inputFiles, outputFile);

            Console.WriteLine($"Merged PDF saved to '{outputFile}' with unique field names.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}