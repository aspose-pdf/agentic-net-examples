using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example of configuring PDF/A conversion options (optional, not applied here)
            // The RemoveFontsStrategy property does not exist in the current API version,
            // so we only set the options that are available.
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
            {
                OptimizeFileSize = true
                // RemoveFontsStrategy assignment removed – not a valid property in this version
            };
            // If conversion were required, uncomment the following line:
            // doc.Convert(conversionOptions);

            // Extract all fonts using the FontUtilities class provided by the Document object
            Font[] fonts = doc.FontUtilities.GetAllFonts();

            Console.WriteLine($"Found {fonts.Length} font(s) in the document:");
            foreach (Font font in fonts)
            {
                Console.WriteLine($"- FontName: {font.FontName}, BaseFont: {font.BaseFont}, Embedded: {font.IsEmbedded}, Subset: {font.IsSubset}");
            }

            // Save the document (no modifications) to demonstrate proper lifecycle handling
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}
