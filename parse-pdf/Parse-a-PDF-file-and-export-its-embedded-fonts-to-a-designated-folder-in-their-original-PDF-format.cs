using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Folder where extracted fonts will be saved
        const string fontsOutputFolder = "ExtractedFonts";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(fontsOutputFolder);

        try
        {
            // Load the PDF document (lifecycle managed by using)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // The Fonts collection may be null if the page has no embedded fonts
                    if (page.Resources?.Fonts == null) continue;

                    // Iterate over each embedded font on the page
                    foreach (Font font in page.Resources.Fonts)
                    {
                        // Skip fonts that are not embedded
                        if (!font.IsEmbedded) continue;

                        // Build a safe file name using the font's decoded name
                        string safeFontName = MakeFileSystemSafeName(font.DecodedFontName ?? "font");
                        string fontFilePath = Path.Combine(
                            fontsOutputFolder,
                            $"{safeFontName}_{pageIndex}.ttf"); // Save as TTF (Aspose provides this format)

                        // Avoid overwriting an existing file with the same name
                        int duplicateCounter = 1;
                        while (File.Exists(fontFilePath))
                        {
                            fontFilePath = Path.Combine(
                                fontsOutputFolder,
                                $"{safeFontName}_{pageIndex}_{duplicateCounter}.ttf");
                            duplicateCounter++;
                        }

                        // Save the font stream to the file
                        using (FileStream fs = new FileStream(fontFilePath, FileMode.Create, FileAccess.Write))
                        {
                            font.Save(fs);
                        }

                        Console.WriteLine($"Extracted font: {font.DecodedFontName} -> {fontFilePath}");
                    }
                }
            }

            Console.WriteLine("Font extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during font extraction: {ex.Message}");
        }
    }

    // Helper to replace invalid filename characters
    private static string MakeFileSystemSafeName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}