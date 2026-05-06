using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF form files to be processed in batch
        string[] inputFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        string outputFile = "merged_unique.pdf";

        // Ensure all input files exist before processing
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Create an instance of PdfFileEditor (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Enable automatic renaming of duplicate field names during concatenation
        editor.KeepFieldsUnique = true;
        // Define the suffix pattern; %NUM% will be replaced with incremental numbers
        editor.UniqueSuffix = "_%NUM%";

        // Concatenate the PDFs; duplicate field names will receive the defined suffix
        editor.Concatenate(inputFiles, outputFile);

        Console.WriteLine($"Merged PDF saved to '{outputFile}' with unique field names.");
    }
}