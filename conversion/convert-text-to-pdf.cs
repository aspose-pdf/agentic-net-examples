using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string txtPath = "input.txt";
        const string pdfPath = "output.pdf";

        // Verify the input text file exists
        if (!File.Exists(txtPath))
        {
            Console.Error.WriteLine($"Text file not found: {txtPath}");
            return;
        }

        // Load the entire plain‑text file into a string
        string content = File.ReadAllText(txtPath);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new blank page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment containing the loaded text
            TextFragment fragment = new TextFragment(content);

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the document as a PDF file
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
    }
}