using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfTitleRotator
{
    /// <summary>
    /// Rotates all text fragments in each PDF to 45 degrees and saves the files.
    /// </summary>
    /// <param name="pdfPaths">Array of full file paths to PDF documents.</param>
    public static void RotateTitleText(string[] pdfPaths)
    {
        if (pdfPaths == null) throw new ArgumentNullException(nameof(pdfPaths));

        foreach (string path in pdfPaths)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                continue; // skip invalid entries

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(path))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Absorb all text fragments on the current page
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    absorber.Visit(page);

                    // Rotate each text fragment by 45 degrees
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        fragment.TextState.Rotation = 45;
                    }
                }

                // Overwrite the original file with the rotated content
                doc.Save(path);
            }
        }
    }
}

public static class Program
{
    /// <summary>
    /// Entry point required for a console application.
    /// Pass PDF file paths as command‑line arguments.
    /// </summary>
    public static void Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("Usage: PdfTitleRotator <pdfPath1> <pdfPath2> ...");
            return;
        }

        try
        {
            PdfTitleRotator.RotateTitleText(args);
            Console.WriteLine("Processing completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}