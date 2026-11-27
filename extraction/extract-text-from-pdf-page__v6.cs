using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string txtPath = "page5.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Verify that page 5 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Use TextAbsorber to extract text from page 5
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[5].Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted text to a .txt file
            File.WriteAllText(txtPath, extractedText);
        }

        Console.WriteLine($"Extracted text from page 5 saved to '{txtPath}'.");
    }
}