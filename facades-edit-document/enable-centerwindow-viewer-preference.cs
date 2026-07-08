using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor facade to modify viewer preferences
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the loaded document to the editor
            editor.BindPdf(doc);

            // Set the CenterWindow flag to true
            editor.ChangeViewerPreference(ViewerPreference.CenterWindow);

            // Save the modified PDF
            editor.Save(outputPath);

            // Close the editor (optional, releases resources)
            editor.Close();
        }

        Console.WriteLine($"PDF saved with CenterWindow enabled: {outputPath}");
    }
}