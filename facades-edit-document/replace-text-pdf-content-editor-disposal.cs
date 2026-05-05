using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Instantiate the PdfContentEditor facade.
            PdfContentEditor editor = new PdfContentEditor();

            try
            {
                // Bind the loaded document to the editor.
                editor.BindPdf(doc);

                // Example edit: replace all occurrences of "Hello" with "Hi".
                editor.ReplaceText("Hello", "Hi");

                // Save the edited document to a new file.
                editor.Save(outputPath);
            }
            finally
            {
                // Ensure the facade releases all resources.
                editor.Close();
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}