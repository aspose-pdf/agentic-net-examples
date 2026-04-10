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

        // Load the PDF document (using block ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create PdfPageEditor and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Example edit: rotate every page by 90 degrees
                editor.Rotation = 90;

                // Apply the modifications to the document
                editor.ApplyChanges();

                // Save the edited PDF (overwrites or creates the output file)
                editor.Save(outputPath);
            } // PdfPageEditor.Dispose() is called here, releasing file handles and unmanaged resources
        } // Document.Dispose() is called here

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}