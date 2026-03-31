using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            // Build an array of odd‑numbered page indexes (1‑based as required by Aspose)
            int[] oddPages = Enumerable.Range(1, pageCount)
                                       .Where(p => p % 2 == 1)
                                       .ToArray();

            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                editor.ProcessPages = oddPages;               // <-- int[] required
                editor.TransitionType = PdfPageEditor.BLINDV;
                editor.TransitionDuration = 2;
                editor.ApplyChanges();
                editor.Save(outputPath);
            }
        }

        Console.WriteLine("Transition applied to odd pages and saved to " + outputPath);
    }
}
