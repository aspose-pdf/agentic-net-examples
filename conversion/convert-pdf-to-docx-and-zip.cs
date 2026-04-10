using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string docxPath = "output.docx";
        const string zipPath = "output.zip";

        // Ensure the source PDF exists; create a simple one if it does not.
        if (!File.Exists(pdfPath))
        {
            CreateSamplePdf(pdfPath);
        }

        // Convert PDF to DOCX using Aspose.Pdf.
        using (Document pdfDocument = new Document(pdfPath))
        {
            var saveOptions = new DocSaveOptions
            {
                // Output format: DOCX
                Format = DocSaveOptions.DocFormat.DocX,
                // Optional: improve bullet detection.
                RecognizeBullets = true
            };

            pdfDocument.Save(docxPath, saveOptions);
        }

        // Compress the resulting DOCX file into a ZIP archive.
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            archive.CreateEntryFromFile(docxPath, Path.GetFileName(docxPath));
        }

        Console.WriteLine($"PDF converted to DOCX and compressed to ZIP:\nDOCX: {docxPath}\nZIP: {zipPath}");
    }

    private static void CreateSamplePdf(string path)
    {
        // Generates a minimal PDF so the conversion pipeline always has input.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("Sample PDF generated because 'input.pdf' was missing.");
            page.Paragraphs.Add(tf);
            doc.Save(path);
        }
    }
}