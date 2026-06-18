using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        // Initialize the content editor facade
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the document to the editor
            editor.BindPdf(doc);

            // Set the HideToolbar viewer preference
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HideToolbar set to true: {outputPath}");
    }
}