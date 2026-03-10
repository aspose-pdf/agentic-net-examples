using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int insertPosition = 2; // 1‑based index where the new page will be placed
        const double pageWidth = 595; // Width in points (A4 width)
        const double pageHeight = 842; // Height in points (A4 height)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the specified position
            Page newPage = doc.Pages.Insert(insertPosition);

            // Set the desired size for the inserted page
            newPage.SetPageSize(pageWidth, pageHeight);

            // Create a text fragment to place on the new page
            TextFragment tf = new TextFragment("This is an inserted page.");
            tf.TextState.FontSize = 14;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            // Position the text near the top‑left corner (Y coordinate measured from bottom)
            tf.Position = new Position(50, pageHeight - 50);

            // Add the text fragment to the page's paragraph collection
            newPage.Paragraphs.Add(tf);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted page saved to '{outputPath}'.");
    }
}