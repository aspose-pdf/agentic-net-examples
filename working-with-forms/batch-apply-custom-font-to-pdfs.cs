using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchFontApplier
{
    static void Main()
    {
        // Network folder containing the PDFs – may be unavailable in some environments.
        const string sourceFolder = @"\\NetworkShare\PdfFiles";
        // Preferred folder where the processed PDFs will be saved (can be the same as source).
        const string preferredOutputFolder = @"\\NetworkShare\PdfFiles\Processed";

        // Path to the custom TrueType font file.
        const string customFontPath = @"C:\Fonts\MyCustomFont.ttf";

        // Resolve a usable output folder – fall back to a local temporary folder if the network path is unavailable.
        string outputFolder = ResolveOutputFolder(preferredOutputFolder, sourceFolder);

        // Load the custom font (fallback to a system font if the file is missing).
        Font customFont = LoadCustomFont(customFontPath);

        // Verify that the source folder exists before trying to enumerate files.
        if (!Directory.Exists(sourceFolder))
        {
            Console.WriteLine($"Warning: Source folder '{sourceFolder}' not found. Falling back to the current directory.");
            // Use the current working directory as a safe fallback.
            string fallbackSource = Directory.GetCurrentDirectory();
            ProcessFolder(fallbackSource, outputFolder, customFont);
        }
        else
        {
            ProcessFolder(sourceFolder, outputFolder, customFont);
        }
    }

    private static void ProcessFolder(string sourceFolder, string outputFolder, Font customFont)
    {
        string[] pdfFiles;
        try
        {
            pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf");
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
        {
            Console.WriteLine($"Error accessing folder '{sourceFolder}': {ex.Message}");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Determine output file path (overwrite in place or use separate folder).
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                // Create an absorber that captures all text fragments on all pages.
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();

                // Perform the search on the whole document.
                absorber.Visit(doc);

                // Apply the custom font to every captured text fragment.
                absorber.ApplyForAllFragments(customFont);

                // Save the modified document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {pdfPath} -> {outputPath}");
        }
    }

    /// <summary>
    /// Tries to create the preferred output folder. If the UNC path cannot be accessed,
    /// falls back to a sub‑folder inside the source directory or, as a last resort,
    /// the system's temporary folder.
    /// </summary>
    private static string ResolveOutputFolder(string preferred, string source)
    {
        // First attempt: the preferred UNC path.
        try
        {
            Directory.CreateDirectory(preferred);
            return preferred;
        }
        catch (IOException)
        {
            // Network path not reachable – continue to fallback options.
        }
        catch (UnauthorizedAccessException)
        {
            // No permission – continue to fallback options.
        }

        // Second attempt: a "Processed" sub‑folder next to the source folder (still on the same share).
        string localFallback = Path.Combine(source, "Processed");
        try
        {
            Directory.CreateDirectory(localFallback);
            return localFallback;
        }
        catch
        {
            // If this also fails, use the system temporary directory.
        }

        // Final fallback: system temporary folder (guaranteed to exist).
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfBatchProcessed");
        Directory.CreateDirectory(tempFolder);
        return tempFolder;
    }

    /// <summary>
    /// Loads the custom TrueType font. If the file cannot be found or opened, a fallback
    /// system font (Arial) is returned and a simple substitution rule is added so that
    /// missing fonts are automatically replaced during PDF processing.
    /// </summary>
    private static Font LoadCustomFont(string fontPath)
    {
        // Verify that the font file exists before trying to open it.
        if (File.Exists(fontPath))
        {
            try
            {
                return FontRepository.OpenFont(fontPath);
            }
            catch (Aspose.Pdf.PdfException)
            {
                // The file exists but could not be opened – fall back to a system font.
                Console.WriteLine($"Warning: Unable to load font at '{fontPath}'. Falling back to Arial.");
            }
        }
        else
        {
            Console.WriteLine($"Warning: Font file not found at '{fontPath}'. Falling back to Arial.");
        }

        // Add a substitution rule so that any missing font name is replaced with Arial.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));
        // Return the Arial font (or any other installed font you prefer).
        return FontRepository.FindFont("Arial");
    }
}
