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

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: using ensures disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a PdfContentEditor facade to edit the PDF content
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Bind the loaded document to the editor
                    editor.BindPdf(doc);

                    // Example operation: replace all occurrences of "Aspose" with "Aspose.PDF"
                    editor.ReplaceText("Aspose", "Aspose.PDF");

                    // Save the modified document (lifecycle: Save inside using)
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"PDF edited and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}