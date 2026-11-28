using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "output049.pdf";
        const string fieldName  = "CustomerName";
        const int    targetPage = 2;
        const float  llx        = 200f; // lower‑left X coordinate
        const float  lly        = 300f; // lower‑left Y coordinate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor implements IDisposable – wrap in using for deterministic cleanup
            using (FormEditor formEditor = new FormEditor())
            {
                // Load the source PDF
                formEditor.BindPdf(inputPath);

                // Copy the *inner* field definition "CustomerName" to page 2 at (200,300)
                // The API requires a page name (can be empty) before the page number.
                formEditor.CopyInnerField(fieldName, string.Empty, targetPage, llx, lly);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} at ({llx},{lly}) and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
