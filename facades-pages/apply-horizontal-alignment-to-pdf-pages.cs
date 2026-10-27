using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added to resolve TextFragment and HorizontalAlignment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor inside a using block for deterministic disposal
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPath);

            // The bound document can be accessed via the Document property
            using (Document doc = editor.Document)
            {
                // Pages are 1‑based; set each page's text fragments to left alignment
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    foreach (var paragraph in page.Paragraphs)
                    {
                        // Only TextFragment (and derived types) expose HorizontalAlignment
                        if (paragraph is TextFragment tf)
                        {
                            tf.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                    }
                }

                // Save the modified document through the facade
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"All pages left‑justified and saved to '{outputPath}'.");
    }
}