using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfTitleRotator
{
    /// <summary>
    /// Rotates all occurrences of the document title text by 45 degrees and saves the PDFs.
    /// </summary>
    /// <param name="pdfPaths">Array of PDF file paths to process.</param>
    public static void RotateTitleText(string[] pdfPaths)
    {
        if (pdfPaths == null) throw new ArgumentNullException(nameof(pdfPaths));

        foreach (string inputPath in pdfPaths)
        {
            if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
                continue; // skip invalid entries

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the document title from metadata; if not set, skip this file
                string title = doc.Info.Title;
                if (string.IsNullOrWhiteSpace(title))
                {
                    // No title metadata – still save to preserve original formatting
                    doc.Save(inputPath);
                    continue;
                }

                // Find all occurrences of the title text in the document
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(title);
                doc.Pages.Accept(absorber);

                // Rotate each found title fragment by 45 degrees
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // TextState.Rotation accepts any angle (in degrees)
                    fragment.TextState.Rotation = 45;
                }

                // Save the modified document (overwrites the original file)
                doc.Save(inputPath);
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Example usage: pass PDF file paths as command‑line arguments
        PdfTitleRotator.RotateTitleText(args);
    }
}
