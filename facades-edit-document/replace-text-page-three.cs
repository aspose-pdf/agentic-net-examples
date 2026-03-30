using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "old text";
        const string replaceText = "new text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);
                // Replace only on page 3 (1‑based indexing)
                editor.ReplaceText(searchText, 3, replaceText);
                doc.Save(outputPath);
            }
            Console.WriteLine($"Text replaced on page 3 and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}