using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // FontRepository, SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page22.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Register Georgia as a substitution font. This will be used whenever a font
            // referenced in the PDF cannot be resolved, effectively making Georgia the
            // default visual font for the rendered page.
            try
            {
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("Georgia", "Georgia"));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Warning: could not register Georgia font – {ex.Message}");
            }

            // Ensure the document contains at least 22 pages (1‑based indexing)
            if (doc.Pages.Count < 22)
            {
                Console.Error.WriteLine($"Document has only {doc.Pages.Count} pages; cannot render page 22.");
                return;
            }

            // Create a PNG device with 200 DPI resolution
            Resolution resolution = new Resolution(200);
            PngDevice pngDevice = new PngDevice(resolution);

            // Retrieve the 22nd page (Aspose.Pdf uses 1‑based page indexing)
            Page page = doc.Pages[22];

            // Render the page to a PNG file
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                pngDevice.Process(page, outStream);
            }

            Console.WriteLine($"Page 22 rendered to PNG at '{outputPath}'.");
        }
    }
}
