using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "page2_text.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // TextAbsorber extracts text; apply it only to page 2 (1‑based indexing)
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[2].Accept(absorber);

            // Save extracted text to a .txt file
            File.WriteAllText(outputTxtPath, absorber.Text);
        }

        Console.WriteLine($"Text from page 2 saved to '{outputTxtPath}'.");
    }
}