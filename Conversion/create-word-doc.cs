using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        try
        {
            // Define output path for the Word document.
            string outputPath = "output.docx";

            // Ensure the output directory exists.
            string outDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);

            // Create a DocumentFactory instance (creation rule).
            DocumentFactory factory = new DocumentFactory();

            // Create an empty PDF document.
            Document pdfDoc = factory.CreateDocument();

            // Add a page to the document.
            Page page = pdfDoc.Pages.Add();

            // Add some text to the page.
            TextFragment text = new TextFragment("Hello, this is a Word document generated via Aspose.Pdf.");
            page.Paragraphs.Add(text);

            // Configure save options to export as DOCX.
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };

            // Save the document.
            pdfDoc.Save(outputPath, saveOptions);

            Console.WriteLine($"Word document saved to: {Path.GetFullPath(outputPath)}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
