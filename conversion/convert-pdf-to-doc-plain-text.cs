using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.doc"; // .doc extension for plain‑text output

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Extract plain text from the PDF using TextAbsorber (the modern replacement for DocSaveOptions.SaveMode.Text)
                TextAbsorber absorber = new TextAbsorber();
                pdfDoc.Pages.Accept(absorber);
                string plainText = absorber.Text;

                // Write the extracted text to a .doc file (plain‑text DOC)
                File.WriteAllText(outputPath, plainText);
            }

            Console.WriteLine($"PDF successfully converted to plain‑text DOC at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
