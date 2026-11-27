using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // TextAbsorber extracts text; it works on a per‑page basis
            TextAbsorber absorber = new TextAbsorber();

            // Extract text from pages 1 through 3 (1‑based indexing)
            for (int pageNum = 1; pageNum <= 3 && pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                pdfDoc.Pages[pageNum].Accept(absorber);
            }

            // Concatenated text from the selected pages
            string extractedText = absorber.Text;

            // Save the concatenated text to a .txt file using standard I/O
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Text from pages 1‑3 saved to '{outputTxtPath}'.");
    }
}