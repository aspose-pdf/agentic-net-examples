using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "hello.pdf";

        try
        {
            // Document implements IDisposable – wrap in using for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
            {
                // Add a new page to the document
                Aspose.Pdf.Page page = doc.Pages.Add();

                // Create a text fragment containing the desired text
                Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment("Hello World");

                // Set optional text appearance (font size, color, etc.)
                fragment.TextState.FontSize = 14;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // fully qualified to avoid ambiguity

                // Add the text fragment to the page's paragraph collection
                page.Paragraphs.Add(fragment);

                // Save the PDF file
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF created successfully at: {Path.GetFullPath(outputPath)}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating PDF: {ex.Message}");
        }
    }
}