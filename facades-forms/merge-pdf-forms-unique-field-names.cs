using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF form files that may contain duplicate field names
        string[] inputFiles = { "FormA.pdf", "FormB.pdf", "FormC.pdf" };
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

        // Initialize PdfFileEditor (does NOT implement IDisposable, so no using block)
        PdfFileEditor editor = new PdfFileEditor();

        // Enable automatic renaming of duplicate fields during concatenation
        editor.KeepFieldsUnique = true;               // true → add suffix to duplicate names
        editor.UniqueSuffix = "_%NUM%";                // suffix template; %NUM% will be replaced by 1,2,3...

        // Concatenate the PDFs; duplicate field names will be renamed with the suffix
        editor.Concatenate(inputFiles, outputFile);

        Console.WriteLine($"Merged PDF saved to '{outputFile}' with unique field names.");
    }
}