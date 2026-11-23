using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "UnderlinedConfidential.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Search for the word "Confidential"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Confidential");
            doc.Pages.Accept(absorber);

            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine("The word \"Confidential\" was not found in the document.");
                return;
            }

            // Get the first occurrence (Aspose collections are 1‑based)
            TextFragment fragment = absorber.TextFragments[1];
            int pageNumber = fragment.Page.Number; // correct way to obtain the page number

            // Rectangle that encloses the found text
            Aspose.Pdf.Rectangle rect = fragment.Rectangle;

            // Create an underline annotation on the appropriate page
            UnderlineAnnotation underline = new UnderlineAnnotation(doc.Pages[pageNumber], rect)
            {
                // Magenta colour (RGB 1,0,1)
                Color = Aspose.Pdf.Color.FromRgb(1, 0, 1),
                // Store author information in the Subject property (Author does not exist)
                Subject = "Bob"
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[pageNumber].Annotations.Add(underline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underlined PDF saved to '{outputPath}'.");
    }
}
