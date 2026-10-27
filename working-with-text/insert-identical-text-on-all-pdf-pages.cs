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
        const string textToInsert = "Sample plain text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Aspose.Pdf.Page page = doc.Pages[i];

                    // Create a TextFragment containing the plain text
                    Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(textToInsert);

                    // Optional: set position, font, size, etc.
                    // tf.Position = new Aspose.Pdf.Text.Position(100, 700);
                    // tf.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial");
                    // tf.TextState.FontSize = 12;

                    // Add the fragment to the page's Paragraphs collection
                    page.Paragraphs.Add(tf);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text inserted on all pages. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}