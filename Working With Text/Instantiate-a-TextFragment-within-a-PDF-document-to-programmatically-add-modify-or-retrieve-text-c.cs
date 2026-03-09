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

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // 1. Add a new TextFragment to the page
            // -------------------------------------------------
            TextFragment newFragment = new TextFragment("Added by Aspose.Pdf");
            // Set the position where the text will appear
            newFragment.Position = new Position(100, 700);
            // Define visual appearance
            newFragment.TextState.Font = FontRepository.FindFont("Arial");
            newFragment.TextState.FontSize = 12;
            newFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the fragment using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(newFragment);

            // -------------------------------------------------
            // 2. Modify existing text (replace first occurrence of "hello")
            // -------------------------------------------------
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("hello");
            // Search on the first page
            page.Accept(absorber);

            if (absorber.TextFragments.Count > 0)
            {
                // The collection is zero‑based; modify the first found fragment
                TextFragment found = absorber.TextFragments[0];
                found.Text = "hi";
                // Optionally change its appearance
                found.TextState.Font = FontRepository.FindFont("TimesNewRoman");
                found.TextState.FontSize = 14;
                found.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            }

            // -------------------------------------------------
            // 3. Retrieve and display text of all found fragments
            // -------------------------------------------------
            foreach (TextFragment tf in absorber.TextFragments)
            {
                Console.WriteLine($"Found fragment text: '{tf.Text}' at position ({tf.Position.XIndent}, {tf.Position.YIndent})");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}