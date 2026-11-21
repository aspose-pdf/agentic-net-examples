using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Required for FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "RenderedImages";
        const string defaultFontPath = "arial.ttf"; // Path to the font used for missing glyphs

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Configure default font for missing glyphs using the modern substitution API
        if (File.Exists(defaultFontPath))
        {
            // Register a substitution that maps any missing font ("*") to the specified TTF file.
            // SimpleFontSubstitution takes the source font pattern and the target font name or file.
            // Using the full file name (without extension) works if the font is installed; using the
            // full path works for a private font file.
            string targetFontName = Path.GetFileNameWithoutExtension(defaultFontPath);
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", targetFontName));
        }
        else
        {
            Console.WriteLine($"Default font file not found: {defaultFontPath}. Using Aspose.Pdf built‑in fallback font.");
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a JPEG device with a desired resolution (e.g., 300 DPI) and quality (0‑100)
            var jpegDevice = new JpegDevice(new Resolution(300), 90);

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");

                // Render the current page to a JPEG file using a stream
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageIndex], outStream);
                }

                Console.WriteLine($"Page {pageIndex} saved as {outputPath}");
            }
        }
    }
}
