using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace PdfTitleRotatorApp
{
    public static class PdfTitleRotator
    {
        /// <summary>
        /// Rotates all text fragments (commonly the title) in each PDF by 45 degrees and saves the result.
        /// </summary>
        /// <param name="pdfPaths">Array of input PDF file paths.</param>
        /// <param name="outputDirectory">Directory where rotated PDFs will be saved.</param>
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
                    // Absorb all text fragments from the document.
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    doc.Pages.Accept(absorber);

                    // Rotate each text fragment by 45 degrees.
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // TextState.Rotation expects a float angle in degrees.
                        fragment.TextState.Rotation = 45;
                    }

                    // Construct the output file path.
                    string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));

                    // Save the modified document.
                    doc.Save(outputPath);
                    Console.WriteLine($"Rotated PDF saved to: {outputPath}");
                }
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point required for a console application.
        /// Usage: PdfTitleRotatorApp <outputDirectory> <pdfPath1> [pdfPath2] ...
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfTitleRotatorApp <outputDirectory> <pdfPath1> [pdfPath2] ...");
                return;
            }

            string outputDirectory = args[0];
            string[] pdfPaths = args[1..]; // C# 8 range operator to get remaining arguments

            PdfTitleRotator.RotateTitleText(pdfPaths, outputDirectory);
        }
    }
}