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
        const string headerText = "My Document Header";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Loop through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Create a HeaderFooter object for the page
                    HeaderFooter header = new HeaderFooter();

                    // Create a TextFragment that contains the header text and styling
                    TextFragment tf = new TextFragment(headerText);
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.FontSize = 12;
                    tf.TextState.ForegroundColor = Color.DarkGray;

                    // Add the TextFragment to the header's paragraph collection
                    header.Paragraphs.Add(tf);

                    // Assign the prepared header to the page
                    page.Header = header;
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Header added to all pages. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
