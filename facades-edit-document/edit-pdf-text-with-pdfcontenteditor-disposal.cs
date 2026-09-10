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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = null;
            try
            {
                // Create the facade and bind the loaded document
                editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // Example operation: replace all occurrences of "old" with "new"
                editor.ReplaceText("old", "new");

                // Save the edited document
                editor.Save(outputPath);
            }
            finally
            {
                // Ensure the facade releases all resources, even if an exception occurs
                if (editor != null)
                {
                    editor.Close(); // Disposes the bound Document within the facade
                }
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}