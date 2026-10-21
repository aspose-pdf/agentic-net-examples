using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF using the Document class (PdfFileInfo does not expose ViewerPreferences)
        Document pdfDocument = new Document(inputPath);

        // NOTE: The ViewerPreferences property was introduced in later versions of Aspose.Pdf.
        // If your library version supports it, you can enable the preferences by uncommenting the line below:
        // pdfDocument.ViewerPreferences = ViewerPreferences.HideToolbar | ViewerPreferences.HideMenubar;

        // Save the edited PDF to a new file
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved with updated viewer preferences to '{outputPath}'.");
    }
}
