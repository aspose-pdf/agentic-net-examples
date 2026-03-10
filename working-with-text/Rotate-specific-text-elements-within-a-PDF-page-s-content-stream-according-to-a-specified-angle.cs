using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateSpecificText
{
    /// <summary>
    /// Rotates all occurrences of the specified texts on a given page.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    /// <param name="pageNumber">1‑based page index on which the rotation should be applied.</param>
    /// <param name="textsToRotate">Array of exact text strings that need to be rotated.</param>
    /// <param name="angle">Rotation angle in degrees (clockwise).</param>
    public static void RotateTexts(string inputPdf, string outputPdf, int pageNumber, string[] textsToRotate, float angle)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the document inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Validate page number.
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Iterate over each target text, locate it and set the rotation.
            foreach (string target in textsToRotate)
            {
                // Create an absorber that searches for the exact phrase.
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(target);
                // Search only the specified page.
                page.Accept(absorber);

                // Apply rotation to every found fragment.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.TextState.Rotation = angle;
                }
            }

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rotated texts saved to '{outputPdf}'.");
    }

    // Example usage.
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_rotated.pdf";
        int pageToEdit = 1; // first page (1‑based)
        string[] texts = { "Important", "Note" }; // texts to rotate
        float rotationAngle = 45f; // rotate 45 degrees clockwise

        RotateTexts(inputPath, outputPath, pageToEdit, texts, rotationAngle);
    }
}