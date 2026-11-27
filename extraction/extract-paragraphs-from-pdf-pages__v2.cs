using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // TextAbsorber extracts text (including paragraph breaks) from the specified pages.
            // It is the core API replacement for ParagraphAbsorber when the latter is unavailable.
            TextAbsorber absorber = new TextAbsorber();

            // Extract text from page 36
            absorber.Visit(pdfDoc.Pages[36]);
            // Extract text from page 37
            absorber.Visit(pdfDoc.Pages[37]);

            // The absorber.Text property contains the merged text of all visited pages.
            string mergedText = absorber.Text;

            // Write the merged text to a plain text file
            File.WriteAllText(outputTxtPath, mergedText);
        }

        Console.WriteLine($"Paragraphs from pages 36-37 saved to '{outputTxtPath}'.");
    }
}
