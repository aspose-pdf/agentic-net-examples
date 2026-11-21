using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Demonstrates how to split a PDF document into individual page files using Aspose.Pdf.
    /// The implementation replaces the non‑existent <c>PdfFileEditor.Split</c> method with the correct
    /// <c>PdfFileEditor.SplitToPages</c> method and documents each step with XML comments for API reference generation.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry point of the console application.
        /// Loads the source PDF, splits it into separate pages, and writes each page to a distinct PDF file.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        public static void Main(string[] args)
        {
            const string inputPath = "input.pdf";
            const string outputFolder = "SplitPages";

            // Validate input file existence.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Error: Input file '{inputPath}' not found.");
                return;
            }

            // Ensure the output directory exists.
            Directory.CreateDirectory(outputFolder);

            try
            {
                // Split the PDF into individual page streams.
                SplitPdfToPages(inputPath, outputFolder);
                Console.WriteLine("PDF splitting completed successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during PDF splitting: {ex.Message}");
            }
        }

        /// <summary>
        /// Splits a PDF file into separate page PDFs.
        /// </summary>
        /// <param name="sourcePdfPath">Full path to the source PDF document.</param>
        /// <param name="destinationFolder">Folder where the individual page PDFs will be saved.</param>
        /// <remarks>
        /// This method uses <see cref="Aspose.Pdf.Facades.PdfFileEditor.SplitToPages(string)"/>
        /// which returns an array of <see cref="System.IO.MemoryStream"/> objects, each containing a single page.
        /// Each stream is written to a new file named <c>Page_{index}.pdf</c>.
        /// </remarks>
        private static void SplitPdfToPages(string sourcePdfPath, string destinationFolder)
        {
            // Create an instance of PdfFileEditor (does not implement IDisposable, so no using block).
            PdfFileEditor editor = new PdfFileEditor();

            // Split the source PDF into an array of MemoryStream objects, one per page.
            // The method name is SplitToPages – the previously used Split method does not exist.
            MemoryStream[] pageStreams = editor.SplitToPages(sourcePdfPath);

            // Iterate over the returned streams and save each as an individual PDF file.
            for (int i = 0; i < pageStreams.Length; i++)
            {
                // Construct the output file name using a 1‑based page index for readability.
                string outputPath = Path.Combine(destinationFolder, $"Page_{i + 1}.pdf");

                // Ensure the stream position is at the beginning before writing.
                pageStreams[i].Position = 0;

                // Write the stream contents to the output file.
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pageStreams[i].CopyTo(fileStream);
                }

                // Optionally dispose the individual MemoryStream now that it has been saved.
                pageStreams[i].Dispose();

                Console.WriteLine($"Saved page {i + 1} to '{outputPath}'.");
            }
        }
    }
}