using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_none.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // The PageMode property controls the initial view mode of the PDF.
                // Setting it to UseNone hides all navigation panels when the document is opened.
                // This property exists in all supported Aspose.Pdf versions, unlike ViewerPreferences.
                doc.PageMode = PageMode.UseNone;

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with PageMode.UseNone to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
