using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfApi
{
    public static class PdfTitleRotator
    {
        /// <summary>
        /// Rotates all title‑like text (font size >= 20 points) by 45 degrees in each PDF file.
        /// The modified PDFs are saved to the specified output folder preserving the original file name.
        /// </summary>
        /// <param name="pdfPaths">Array of full paths to source PDF files.</param>
        /// <param name="outputFolder">Folder where the rotated PDFs will be saved.</param>
        public static void RotateTitles(string[] pdfPaths, string outputFolder)
        {
            if (pdfPaths == null) throw new ArgumentNullException(nameof(pdfPaths));
            if (string.IsNullOrWhiteSpace(outputFolder)) throw new ArgumentException("Output folder must be provided.", nameof(outputFolder));

            Directory.CreateDirectory(outputFolder);

            foreach (string inputPath in pdfPaths)
            {
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found: {inputPath}");
                    continue;
                }

                try
                {
                    // Open the PDF inside a using block for deterministic disposal.
                    using (Document doc = new Document(inputPath))
                    {
                        // Absorb all text fragments in the document.
                        TextFragmentAbsorber absorber = new TextFragmentAbsorber();

                        // Accept the absorber for all pages (pages are 1‑based).
                        doc.Pages.Accept(absorber);

                        // Iterate over each captured text fragment.
                        foreach (TextFragment fragment in absorber.TextFragments)
                        {
                            // Heuristic: treat fragments with a large font size as titles.
                            // Adjust the threshold (20) as needed for your documents.
                            if (fragment.TextState.FontSize >= 20)
                            {
                                // Rotate the fragment by 45 degrees.
                                fragment.TextState.Rotation = 45;
                            }
                        }

                        // Build the output file path.
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                        // Save the modified document as PDF.
                        doc.Save(outputPath);
                        Console.WriteLine($"Rotated titles saved to: {outputPath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            //   args[0] – path to a folder that contains PDF files to process.
            //   args[1] – path to the output folder where rotated PDFs will be written.
            // If arguments are missing, fall back to simple demo folders.

            string inputFolder = args.Length > 0 ? args[0] : "InputPdfs";
            string outputFolder = args.Length > 1 ? args[1] : "OutputPdfs";

            // Validate input folder.
            if (!Directory.Exists(inputFolder))
            {
                Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            // Gather all PDF files from the input folder.
            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

            if (pdfFiles.Length == 0)
            {
                Console.WriteLine($"No PDF files found in folder: {inputFolder}");
                return;
            }

            PdfTitleRotator.RotateTitles(pdfFiles, outputFolder);
        }
    }
}
