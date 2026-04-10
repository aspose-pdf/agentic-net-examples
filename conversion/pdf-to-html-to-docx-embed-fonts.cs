using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Determine a data directory relative to the executable.
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        // Ensure the directory exists.
        Directory.CreateDirectory(dataDir);

        // Define file paths.
        string pdfPath   = Path.Combine(dataDir, "input.pdf");
        string htmlPath  = Path.Combine(dataDir, "intermediate.html");
        string docxPath  = Path.Combine(dataDir, "final.docx");

        // Verify the source PDF exists before attempting conversion.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // ---------- Step 1: PDF → HTML ----------
        using (Document pdfDoc = new Document(pdfPath))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                FontSavingMode   = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF,
                DefaultFontName  = "Arial" // fallback if a font is missing
            };
            pdfDoc.Save(htmlPath, htmlOpts);
        }

        // ---------- Step 2: HTML → DOCX with embedded fonts ----------
        HtmlLoadOptions htmlLoadOpts = new HtmlLoadOptions
        {
            IsEmbedFonts = true
        };

        using (Document htmlDoc = new Document(htmlPath, htmlLoadOpts))
        {
            DocSaveOptions docOpts = new DocSaveOptions
            {
                Format            = DocSaveOptions.DocFormat.DocX,
                Mode              = DocSaveOptions.RecognitionMode.Flow,
                ReSaveFonts       = true,   // forces embedding of used fonts
                ConvertType3Fonts = true    // converts Type3 fonts to TrueType for embedding
            };
            htmlDoc.Save(docxPath, docOpts);
        }

        Console.WriteLine("PDF successfully converted to HTML and then to DOCX with embedded fonts.");
    }
}
