using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerImagePath = "footer.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a footer artifact with the image and desired opacity to each page
            foreach (Page page in doc.Pages)
            {
                FooterArtifact pageFooter = new FooterArtifact();
                pageFooter.SetImage(footerImagePath);          // set the image source
                pageFooter.Opacity = 0.3;                      // 30% opacity
                pageFooter.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                pageFooter.ArtifactVerticalAlignment = VerticalAlignment.Bottom;
                pageFooter.BottomMargin = 10;                  // optional margin from bottom

                page.Artifacts.Add(pageFooter);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPath}'.");
    }
}
