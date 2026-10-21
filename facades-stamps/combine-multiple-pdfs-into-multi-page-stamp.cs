using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files that will be combined into a single multi‑page stamp
        string[] pdfFiles = { "stamp1.pdf", "stamp2.pdf", "stamp3.pdf" };
        const string outputPath = "combined_stamp.pdf";

        // Verify that all source files exist
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Merge the PDFs into one Document (target)
        // Use nested using blocks for deterministic disposal (lifecycle rule)
        using (Aspose.Pdf.Document target = new Aspose.Pdf.Document(pdfFiles[0]))
        {
            // Add remaining PDFs to the target document
            for (int i = 1; i < pdfFiles.Length; i++)
            {
                using (Aspose.Pdf.Document source = new Aspose.Pdf.Document(pdfFiles[i]))
                {
                    target.Pages.Add(source.Pages);
                }
            }

            // Create a header that will act as the stamp on every page
            Aspose.Pdf.HeaderFooter stampHeader = new Aspose.Pdf.HeaderFooter();
            // Add a text fragment to the header (you can customize font, size, color, etc.)
            Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment("Multi‑Page Stamp");
            tf.TextState.FontSize = 12;
            tf.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.2, 0.4, 0.8);
            stampHeader.Paragraphs.Add(tf);

            // Apply the header to every page in the merged document
            for (int pageIndex = 1; pageIndex <= target.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = target.Pages[pageIndex];
                page.Header = stampHeader;
            }

            // Save the final PDF containing the multi‑page stamp
            target.Save(outputPath);
        }

        Console.WriteLine($"Combined stamp PDF saved to '{outputPath}'.");
    }
}