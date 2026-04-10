using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Prepare the footer text (generation date)
            string footerText = DateTime.Now.ToString("yyyy-MM-dd");

            // Iterate through all pages (1‑based indexing per rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a FooterArtifact and configure its appearance
                FooterArtifact footer = new FooterArtifact();
                footer.Text = footerText;
                footer.TextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 9,
                    ForegroundColor = Aspose.Pdf.Color.Gray
                };
                // Position the footer 20 points from the bottom margin
                footer.BottomMargin = 20;

                // Add the footer artifact to the page
                page.Artifacts.Add(footer);
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer at '{outputPath}'.");
    }
}