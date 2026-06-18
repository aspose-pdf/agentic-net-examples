using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfApi
{
    /// <summary>
    /// Provides PDF manipulation utilities that work with streams.
    /// </summary>
    public static class PdfMemoryProcessor
    {
        /// <summary>
        /// Loads a PDF from <paramref name="inputStream"/>, adds a text fragment,
        /// and writes the modified PDF into a new <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="inputStream">Stream containing the source PDF (must be readable).</param>
        /// <param name="text">The text to add to the first page.</param>
        /// <param name="x">X coordinate (in points) where the text will start.</param>
        /// <param name="y">Y coordinate (in points) where the text will start.</param>
        /// <returns>A <see cref="MemoryStream"/> containing the updated PDF.</returns>
        public static MemoryStream AddTextToPdf(Stream inputStream, string text, double x = 100, double y = 700)
        {
            // Ensure the input stream is at the beginning
            if (inputStream.CanSeek)
                inputStream.Position = 0;

            // Load the PDF from the input stream
            using (Document doc = new Document(inputStream))
            {
                // Ensure there is at least one page; create one if the document is empty
                if (doc.Pages.Count == 0)
                    doc.Pages.Add();

                // Get the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Create a text fragment with the desired content
                TextFragment fragment = new TextFragment(text);

                // Position the text fragment on the page
                fragment.Position = new Position(x, y);

                // Optionally set appearance (font, size, color)
                fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(fragment);

                // Save the modified document into a memory stream
                MemoryStream outputStream = new MemoryStream();
                doc.Save(outputStream);

                // Reset the output stream position so it can be read from the beginning
                outputStream.Position = 0;
                return outputStream;
            }
        }
    }

    /// <summary>
    /// Minimal entry point required for a console‑type project.
    /// It demonstrates the usage of <see cref="PdfMemoryProcessor"/> when command‑line arguments are supplied.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // If two arguments are provided, treat them as input and output file paths.
            // An optional third argument can specify the text to add.
            if (args.Length >= 2)
            {
                string inputPath = args[0];
                string outputPath = args[1];
                string text = args.Length >= 3 ? args[2] : "Hello, Aspose.Pdf!";

                using (FileStream input = File.OpenRead(inputPath))
                {
                    MemoryStream result = PdfMemoryProcessor.AddTextToPdf(input, text);
                    using (FileStream output = File.Create(outputPath))
                    {
                        result.CopyTo(output);
                    }
                }
            }
            else
            {
                // No arguments – nothing to do. The method can be called from other code.
                Console.WriteLine("Usage: AsposePdfApi <input.pdf> <output.pdf> [text]");
            }
        }
    }
}
