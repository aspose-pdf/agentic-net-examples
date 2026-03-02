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
        const string searchText = "hello world";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Find all occurrences of the specified text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);

            if (absorber.TextFragments.Count > 0)
            {
                // Retrieve the desired font and ensure it will be embedded
                Font font = FontRepository.FindFont("Arial");
                font.IsEmbedded = true;

                // Apply font properties to each found text fragment
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.TextState.Font = font;                         // Set new font
                    fragment.TextState.FontSize = 14;                       // Set font size
                    fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // Set font color
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Font properties applied and saved to '{outputPath}'.");
    }
}