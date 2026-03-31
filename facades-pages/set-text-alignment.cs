using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (document.Pages.Count == 0)
            {
                document.Pages.Add();
            }

            // Create a text fragment with sample content
            TextFragment fragment = new TextFragment("This text is centered on the page.");
            // Set horizontal alignment using the HorizontalAlignment enumeration (Left, Center, Right)
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            // Position the fragment; X coordinate is ignored for Center alignment
            fragment.Position = new Position(0, 500);

            // Add the fragment to the first page
            Page page = document.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            document.Save(outputPath);
        }

        Console.WriteLine($"Aligned PDF saved to '{outputPath}'.");
    }
}