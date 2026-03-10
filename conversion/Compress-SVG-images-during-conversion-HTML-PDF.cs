using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    // Compresses all *.svg files found in the specified folder into *.svgz (gzip) files.
    // The original *.svg files are left untouched; the compressed versions can be used
    // by the HTML document if it references them with the .svgz extension.
    static void CompressSvgFiles(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            return;

        foreach (string svgPath in Directory.GetFiles(folderPath, "*.svg", SearchOption.AllDirectories))
        {
            string svgzPath = Path.ChangeExtension(svgPath, ".svgz");

            // Skip if a compressed version already exists and is newer.
            if (File.Exists(svgzPath) && File.GetLastWriteTimeUtc(svgzPath) >= File.GetLastWriteTimeUtc(svgPath))
                continue;

            using (FileStream input = File.OpenRead(svgPath))
            using (FileStream output = File.Create(svgzPath))
            using (GZipStream gzip = new GZipStream(output, CompressionLevel.Optimal))
            {
                input.CopyTo(gzip);
            }
        }
    }

    static void Main()
    {
        const string htmlInputPath = "input.html";      // Path to the source HTML file.
        const string pdfOutputPath = "output.pdf";      // Desired PDF output path.
        const string resourcesFolder = "Resources";    // Folder that contains images/SVGs referenced by the HTML.

        // ---------------------------------------------------------------------
        // 1. Validate input file existence.
        // ---------------------------------------------------------------------
        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"Error: HTML source file '{htmlInputPath}' was not found.");
            return; // Gracefully exit – no unhandled exception.
        }

        // ---------------------------------------------------------------------
        // 2. (Optional) Compress any SVG graphics that the HTML may reference.
        // ---------------------------------------------------------------------
        CompressSvgFiles(resourcesFolder);

        // ---------------------------------------------------------------------
        // 3. Load the HTML document and convert it to PDF.
        // ---------------------------------------------------------------------
        try
        {
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlInputPath, loadOptions))
            {
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"HTML converted to PDF and saved as '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            // Provide a clear message while keeping the stack trace for debugging if needed.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
