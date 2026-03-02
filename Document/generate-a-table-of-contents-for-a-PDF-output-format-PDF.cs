using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Create a new document that will contain the TOC page followed by the original pages
            Document doc = new Document();

            // Add a new page for the Table of Contents
            Page tocPage = doc.Pages.Add();

            // Initialize a TextBuilder for the TOC page
            TextBuilder builder = new TextBuilder(tocPage);

            // Title for the TOC
            TextFragment title = new TextFragment("Table of Contents");
            title.TextState.FontSize = 16;
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.Position = new Position(50, 800);
            builder.AppendText(title);

            // Simple placeholder entries – in a real scenario you would extract headings from srcDoc.TaggedContent
            for (int i = 1; i <= srcDoc.Pages.Count; i++)
            {
                TextFragment entry = new TextFragment($"Page {i} .................................. {i}");
                entry.TextState.FontSize = 12;
                entry.TextState.Font = FontRepository.FindFont("Helvetica");
                entry.Position = new Position(50, 800 - 30 * i);
                builder.AppendText(entry);
            }

            // Append the original pages after the TOC page
            doc.Pages.Add(srcDoc.Pages);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPath}'.");
    }
}