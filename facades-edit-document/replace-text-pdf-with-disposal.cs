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

        PdfContentEditor editor = null;
        try
        {
            // Create the PdfContentEditor facade
            editor = new PdfContentEditor();

            // Bind the source PDF file to the facade
            editor.BindPdf(inputPath);

            // Example operation: replace all occurrences of "Hello" with "Hi"
            editor.ReplaceText("Hello", "Hi");

            // Save the modified PDF to the output path
            editor.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Ensure the facade releases all resources
            if (editor != null)
            {
                editor.Close();   // Closes the bound document
                editor.Dispose(); // Disposes the facade itself
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}