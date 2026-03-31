using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a FooterArtifact that will act as the visual representation of the "Footer" field
            FooterArtifact footerArtifact = new FooterArtifact();

            // Configure text appearance: italic style, black text
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Helvetica");
            textState.FontSize = 12;
            textState.FontStyle = FontStyles.Italic;
            textState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Set the text and its state
            footerArtifact.SetTextAndState("Footer", textState);

            // Place the artifact behind page content and give it a light‑gray background rectangle
            footerArtifact.IsBackground = true;
            footerArtifact.TextState.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add the artifact to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.Artifacts.Add(footerArtifact);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer field decorated and saved to '{outputPath}'.");
    }
}