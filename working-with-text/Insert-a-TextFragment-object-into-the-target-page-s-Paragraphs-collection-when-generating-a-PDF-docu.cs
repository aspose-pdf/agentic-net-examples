using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Define output PDF path
        const string outputPath = "output.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a new page (first page is index 1)
                Page page = doc.Pages.Add();

                // Create a TextFragment with the desired text
                TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
                // Set the position where the text will appear on the page
                tf.Position = new Position(100, 600);

                // Customize text appearance – avoid System.Drawing‑dependent APIs
                tf.TextState.FontSize = 14;
                // Font assignment removed because FontRepository may trigger GDI+ initialization on non‑Windows platforms
                // tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                tf.TextState.BackgroundColor = Aspose.Pdf.Color.LightGray;

                // Insert the TextFragment into the page's Paragraphs collection
                page.Paragraphs.Add(tf);

                // Save the document as PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF created successfully at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating PDF: {ex.Message}");
        }
    }
}
