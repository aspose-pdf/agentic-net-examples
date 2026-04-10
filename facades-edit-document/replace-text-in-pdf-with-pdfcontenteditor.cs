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

        // Load the PDF document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = null;
            try
            {
                // Initialize the editor with the loaded document
                editor = new PdfContentEditor(doc);

                // Example operation: replace all occurrences of "Hello" with "Hi"
                editor.ReplaceText("Hello", "Hi");

                // Save the modified document (PDF format)
                doc.Save(outputPath);
            }
            finally
            {
                // Ensure the editor releases its resources even if an exception occurs
                if (editor != null)
                {
                    editor.Close(); // Closes the bound document and releases facade resources
                }
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}