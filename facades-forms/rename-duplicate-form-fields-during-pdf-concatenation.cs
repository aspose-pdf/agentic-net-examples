using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF files that may contain duplicate form field names
        string[] inputFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        string outputFile = "merged_unique.pdf";

        // Ensure all source files exist before processing
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // PdfFileEditor does NOT implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();

        // Enable automatic renaming of duplicate fields during concatenation
        editor.KeepFieldsUnique = true;
        // Define the suffix pattern; %NUM% will be replaced with an incremental index
        editor.UniqueSuffix = "_%NUM%";

        // Concatenate the PDFs; duplicate field names will be renamed using the suffix pattern
        editor.Concatenate(inputFiles, outputFile);

        Console.WriteLine($"Merged PDF saved to '{outputFile}' with unique field names.");
    }
}