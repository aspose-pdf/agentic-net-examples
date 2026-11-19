using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_subscript.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Create a TextFragment containing the subscript text
                TextFragment subscript = new TextFragment("H₂O");

                // Set font and size (optional, but ensures consistent appearance)
                subscript.TextState.Font = FontRepository.FindFont("Arial");
                subscript.TextState.FontSize = 12;

                // Render subscript by lowering the Y‑coordinate of the fragment.
                // Adjust the offset (e.g., -2 points) to achieve the desired visual effect.
                double subscriptOffset = 2; // points to move downwards
                subscript.Position = new Position(100, 700 - subscriptOffset);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(subscript);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Subscript PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
