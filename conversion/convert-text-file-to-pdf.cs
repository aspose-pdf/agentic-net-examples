using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for input text file and output PDF
        const string inputTextPath = "input.txt";
        const string outputPdfPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputTextPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputTextPath}");
            return;
        }

        // Read the entire text content from the file
        string fileContent = File.ReadAllText(inputTextPath);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a new blank page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a TextFragment with the file content
            TextFragment txtFragment = new TextFragment(fileContent);

            // Optionally set some text properties (font, size, color)
            txtFragment.TextState.Font = FontRepository.FindFont("Arial");
            txtFragment.TextState.FontSize = 12;
            txtFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the TextFragment to the page's paragraph collection
            page.Paragraphs.Add(txtFragment);

            // Save the PDF to the specified path
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created successfully at '{outputPdfPath}'.");
    }
}