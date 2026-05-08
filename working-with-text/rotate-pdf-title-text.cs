using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfTitleRotator
{
    /// <summary>
    /// Rotates all occurrences of the document title text by 45 degrees in each PDF file.
    /// The modified PDFs are saved to the specified output directory, preserving original file names.
    /// </summary>
    /// <param name="pdfPaths">Array of input PDF file paths.</param>
    /// <param name="outputDirectory">Directory where the rotated PDFs will be saved.</param>
    public static void RotateTitleText(string[] pdfPaths, string outputDirectory)
    {
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in pdfPaths)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the document title from metadata.
                string title = doc.Info.Title;
                if (string.IsNullOrWhiteSpace(title))
                {
                    // No title metadata; skip rotation for this file.
                    Console.WriteLine($"No title found in '{Path.GetFileName(inputPath)}'; skipping.");
                }
                else
                {
                    // Find all text fragments that match the title.
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(title);
                    // Search across all pages.
                    doc.Pages.Accept(absorber);

                    // Rotate each found fragment by 45 degrees.
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // TextState.Rotation expects a float angle in degrees.
                        fragment.TextState.Rotation = 45f;
                    }
                }

                // Determine the output file path.
                string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));

                // Save the modified document as PDF.
                doc.Save(outputPath);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console application.
    /// Usage: <outputDirectory> <pdfPath1> [pdfPath2] ...
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <outputDirectory> <pdfPath1> [pdfPath2] ...");
            return;
        }

        string outputDirectory = args[0];
        string[] pdfPaths = args.Skip(1).ToArray();

        PdfTitleRotator.RotateTitleText(pdfPaths, outputDirectory);
    }
}
