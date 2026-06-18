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

        // Load the PDF document using a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = null;
            try
            {
                // Create the PdfContentEditor facade
                editor = new PdfContentEditor();

                // Bind the loaded document to the facade
                editor.BindPdf(doc);

                // Example operation: replace all occurrences of "Hello" with "Hi"
                editor.ReplaceText("Hello", "Hi");

                // Save the modified document via the facade
                editor.Save(outputPath);
            }
            finally
            {
                // Ensure the facade releases all resources
                if (editor != null)
                {
                    editor.Close(); // disposes the bound document and releases resources
                }
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}