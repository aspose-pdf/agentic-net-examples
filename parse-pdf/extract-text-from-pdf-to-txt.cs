using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create a TextAbsorber to collect text from all pages
                TextAbsorber absorber = new TextAbsorber();

                // Accept the absorber for the entire Pages collection (1‑based indexing is handled internally)
                pdfDoc.Pages.Accept(absorber);

                // Retrieve the concatenated text
                string extractedText = absorber.Text ?? string.Empty;

                // Write the text to the output .txt file (overwrites if it exists)
                File.WriteAllText(outputTxtPath, extractedText);
            }

            Console.WriteLine($"Text extracted and saved to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}