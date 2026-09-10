using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // Ensure a source PDF exists – the sandbox does not contain any
        // external files. If the file is missing we create a minimal one‑page
        // PDF that can be used for the demonstration.
        // ------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Load the (now guaranteed) PDF document
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count;

            // Add a footer to each page showing "Page X of Y"
            foreach (Page page in doc.Pages)
            {
                TextFragment footer = new TextFragment($"Page {page.Number} of {totalPages}");
                // Position the footer near the bottom of the page (Y measured from the bottom)
                footer.Position = new Position(0, 20); // X is ignored when using Center alignment
                footer.HorizontalAlignment = HorizontalAlignment.Center;
                // Optional styling (uncomment if needed)
                // footer.TextState.Font = FontRepository.FindFont("Helvetica");
                // footer.TextState.FontSize = 10;
                // footer.TextState.ForegroundColor = Color.Gray;

                page.Paragraphs.Add(footer);
            }

            // Save the modified PDF with footers
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom footers to '{outputPath}'.");
    }
}
