using System;
using System.IO;
using Aspose.Pdf;

namespace RotatePdfExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input and output file names (must be in the same folder as the executable)
            const string inputFileName = "input.pdf";
            const string outputFileName = "output.pdf";

            // Resolve full paths relative to the executable location
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string inputPath = Path.Combine(baseDir, inputFileName);
            string outputPath = Path.Combine(baseDir, outputFileName);

            Document doc;

            // Try to load the existing PDF. If the file does not exist or is not a valid PDF,
            // create a simple one‑page placeholder document instead.
            if (File.Exists(inputPath))
            {
                try
                {
                    doc = new Document(inputPath);
                }
                catch (InvalidPdfFileFormatException)
                {
                    Console.WriteLine($"File at '{inputPath}' is not a valid PDF. A placeholder PDF will be created.");
                    doc = CreatePlaceholderPdf();
                    doc.Save(inputPath);
                }
            }
            else
            {
                Console.WriteLine($"Input file not found at '{inputPath}'. A placeholder PDF will be created.");
                doc = CreatePlaceholderPdf();
                doc.Save(inputPath);
            }

            // Rotate every page 180° (invert orientation)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Rotate = Rotation.on180; // correct enum value
            }

            // Save the rotated document
            doc.Save(outputPath);
            Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
        }

        /// <summary>
        /// Creates a minimal one‑page PDF (A4 size) used as a fallback when the input file is missing or corrupt.
        /// </summary>
        private static Document CreatePlaceholderPdf()
        {
            Document placeholder = new Document();
            placeholder.Pages.Add(); // adds a blank A4 page by default
            return placeholder;
        }
    }
}
