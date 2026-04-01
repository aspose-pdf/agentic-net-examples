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
        const string imagePath = "logo.png";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            foreach (Page page in document.Pages)
            {
                WatermarkArtifact artifact = new WatermarkArtifact();
                artifact.SetImage(imagePath);

                TextState textState = new TextState();
                textState.Font = FontRepository.FindFont("Helvetica");
                textState.FontSize = 48;
                textState.ForegroundColor = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0);

                artifact.SetTextAndState(watermarkText, textState);
                artifact.Opacity = 0.5f;
                artifact.IsBackground = false;

                page.Artifacts.Add(artifact);
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}