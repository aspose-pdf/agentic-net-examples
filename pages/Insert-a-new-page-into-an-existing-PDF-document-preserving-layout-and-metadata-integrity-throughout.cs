using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int insertPosition = 2; // 1‑based index where the new page will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the desired position.
            // The API automatically selects the most common page size.
            Page newPage = doc.Pages.Insert(insertPosition);

            // (Optional) Add placeholder content if you need a visible page.
            // newPage.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            // Save the document. All original metadata (Info, TaggedContent, etc.) is preserved.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted a new page and saved to '{outputPath}'.");
    }
}