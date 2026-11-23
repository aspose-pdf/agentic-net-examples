using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Verify that the document has at least one page
                if (doc.Pages.Count > 0)
                {
                    // Access the first page (1‑based indexing)
                    Aspose.Pdf.Page page = doc.Pages[1];

                    // Verify that the page contains at least one paragraph (text fragment)
                    if (page.Paragraphs.Count > 0)
                    {
                        // Retrieve the first paragraph and cast it to TextFragment
                        Aspose.Pdf.Text.TextFragment tf = page.Paragraphs[1] as Aspose.Pdf.Text.TextFragment;

                        if (tf != null)
                        {
                            // Adjust the position of the text fragment (example coordinates)
                            tf.Position = new Aspose.Pdf.Text.Position(100, 500);
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}