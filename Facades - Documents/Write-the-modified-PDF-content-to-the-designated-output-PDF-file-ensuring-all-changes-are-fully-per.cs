using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "modified_output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a facade that allows page-level modifications
            PdfPageEditor editor = new PdfPageEditor();

            // Bind the existing PDF document to the editor
            editor.BindPdf(inputPath);

            // Example modification: set the zoom factor for all pages
            editor.Zoom = 0.75f; // 75% zoom

            // Persist the changes to the designated output file
            editor.Save(outputPath);

            // Release resources held by the facade
            editor.Close();

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}