using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "custom_lineheight.pdf";

        // Load existing PDF if it exists; otherwise create a new empty document
        using (Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment with multiple lines
            TextFragment fragment = new TextFragment("First line\nSecond line\nThird line");

            // Modify the existing TextState (read‑only property) to set custom formatting
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            // Use LineSpacing (or LineHeight if available) to define custom line height
            fragment.TextState.LineSpacing = 20; // custom line height in points

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom line height to '{outputPath}'.");
    }
}
