using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "centered_text.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Centered Text on the Page");

            // Configure the existing TextState instance (the property is read‑only)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 24;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center; // Center the text horizontally

            // Use TextBuilder to place the fragment on the page.
            // TextBuilder respects TextState.HorizontalAlignment.
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
