using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Intermediate DOCX file path
        const string docxPath = "output.docx";

        // Final ZIP archive path
        const string zipPath = "output.zip";

        // -----------------------------------------------------------------
        // Ensure the source PDF exists – create a minimal one if it does not.
        // -----------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            using (Document sample = new Document())
            {
                // Add a single page with a simple text fragment.
                Page page = sample.Pages.Add();
                TextFragment tf = new TextFragment("Sample PDF generated because 'input.pdf' was missing.")
                {
                    Position = new Position(100, 700),
                    TextState = { FontSize = 12 }
                };
                page.Paragraphs.Add(tf);
                sample.Save(pdfPath);
                Console.WriteLine($"Created placeholder PDF at '{pdfPath}'.");
            }
        }

        // -----------------------------------------------------------------
        // Convert PDF to DOCX using Aspose.Pdf
        // -----------------------------------------------------------------
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Export as DOCX (not the older DOC format)
                Format = DocSaveOptions.DocFormat.DocX,

                // Use the Flow recognition mode for maximum editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Enable bullet recognition (optional, improves results)
                RecognizeBullets = true
            };

            // Save the converted document
            pdfDocument.Save(docxPath, saveOptions);
        }

        // -----------------------------------------------------------------
        // Compress the resulting DOCX into a ZIP archive
        // -----------------------------------------------------------------
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Add the DOCX file to the archive
                archive.CreateEntryFromFile(
                    docxPath,
                    Path.GetFileName(docxPath),
                    CompressionLevel.Optimal);
            }
        }

        // Optional: clean up the intermediate DOCX file if no longer needed
        // File.Delete(docxPath);

        Console.WriteLine($"PDF converted to DOCX and compressed into '{zipPath}'.");
    }
}
