using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfTitleRotator
{
    /// <summary>
    /// Rotates all occurrences of the document title text by 45 degrees
    /// and saves the modified PDFs to the specified output folder.
    /// </summary>
    /// <param name="pdfPaths">Array of input PDF file paths.</param>
    /// <param name="outputFolder">Folder where the rotated PDFs will be saved.</param>
    public static void RotateTitleInPdfs(string[] pdfPaths, string outputFolder)
    {
        if (pdfPaths == null) throw new ArgumentNullException(nameof(pdfPaths));
        if (string.IsNullOrWhiteSpace(outputFolder))
            throw new ArgumentException("Output folder must be specified.", nameof(outputFolder));

        Directory.CreateDirectory(outputFolder);

        foreach (string inputPath in pdfPaths)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Open the PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the document title from metadata.
                string title = doc.Info.Title;
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine($"No title metadata in '{Path.GetFileName(inputPath)}'; skipping rotation.");
                }
                else
                {
                    // Extract all text fragments from the document.
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    doc.Pages.Accept(absorber);

                    // Rotate each fragment whose text matches the title.
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // Trim to avoid whitespace mismatches.
                        if (fragment.Text.Trim().Equals(title, StringComparison.Ordinal))
                        {
                            // Set rotation angle to 45 degrees.
                            fragment.TextState.Rotation = 45;
                        }
                    }
                }

                // Build output path (same file name, different folder).
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                // Save the modified PDF.
                doc.Save(outputPath);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for compilation. Demonstrates how to call the rotator.
    /// Expected arguments: <outputFolder> <pdfPath1> [pdfPath2] ...
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <outputFolder> <pdfPath1> [pdfPath2] ...");
            return;
        }

        string outputFolder = args[0];
        // C# 8.0 range operator to get the remaining arguments as the PDF list.
        string[] pdfPaths = args[1..];

        PdfTitleRotator.RotateTitleInPdfs(pdfPaths, outputFolder);
    }
}
