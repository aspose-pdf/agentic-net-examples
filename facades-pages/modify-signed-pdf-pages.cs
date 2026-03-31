using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_input.pdf";
        const string outputPath = "modified_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the signed PDF to the page editor
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(inputPath);

        // Determine which pages to modify (here we modify all pages)
        using (Document tempDoc = new Document(inputPath))
        {
            int pageCount = tempDoc.Pages.Count;
            int[] allPages = Enumerable.Range(1, pageCount).ToArray();
            pageEditor.ProcessPages = allPages;
        }

        // Apply a zoom factor (e.g., reduce size to 90% of original)
        pageEditor.Zoom = 0.9f;

        // Save the edited PDF; existing signatures remain valid
        pageEditor.Save(outputPath);
        pageEditor.Close();

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}