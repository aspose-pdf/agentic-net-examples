using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public static class PdfTitleRotator
{
    /// <summary>
    /// Rotates all text fragments (treated as title text) in each PDF by 45 degrees and saves the result.
    /// </summary>
    /// <param name="pdfPaths">Array of input PDF file paths.</param>
    /// <param name="outputDirectory">Directory where rotated PDFs will be saved.</param>
    public static void RotateTitleText(string[] pdfPaths, string outputDirectory)
    {
        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in pdfPaths)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Absorb all text fragments from the document
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                doc.Pages.Accept(absorber);

                // Rotate each text fragment by 45 degrees
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Set rotation (in degrees) on the fragment's TextState
                    fragment.TextState.Rotation = 45;
                }

                // Build output file path preserving original file name
                string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));

                // Save the modified document (PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Rotated PDF saved to: {Path.Combine(outputDirectory, Path.GetFileName(inputPath))}");
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console application.
    /// Expected arguments: <outputDirectory> <pdfPath1> [pdfPath2] ...
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <outputDirectory> <pdfPath1> [pdfPath2] ...");
            return;
        }

        string outputDirectory = args[0];
        // C# 8 range operator to get the remaining arguments as the PDF paths array
        string[] pdfPaths = args[1..];

        PdfTitleRotator.RotateTitleText(pdfPaths, outputDirectory);
    }
}
