using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output056.pdf";
        const string fieldName = "EmailAddress";
        const int targetPage = 4;
        const double llx = 270;
        const double lly = 370;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize FormEditor and bind the source PDF.
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPath);

                // Copy the outer field definition to page 4 at (270,370).
                // The overload requires a page name (string) before the page number.
                // An empty string is acceptable when the page name is not needed.
                formEditor.CopyOuterField(fieldName, string.Empty, targetPage, (float)llx, (float)lly);

                // Save the resulting PDF.
                formEditor.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
