using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // PdfPageEditor lives in this namespace in newer versions

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Detect pages that contain any text
        List<int> pagesWithText = new List<int>();
        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[i].Accept(absorber);

                if (!string.IsNullOrWhiteSpace(absorber.Text))
                {
                    pagesWithText.Add(i);
                }
            }
        }

        if (pagesWithText.Count == 0)
        {
            Console.WriteLine("No pages contain text – nothing to resize.");
            // Copy original file when nothing to modify
            File.Copy(inputPath, outputPath, overwrite: true);
            return;
        }

        // Resize only the pages that contain text
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = pagesWithText.ToArray();
            editor.Zoom = 1.2f; // 20 % increase of the page contents
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
