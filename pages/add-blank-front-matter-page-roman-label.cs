using System;
using System.IO;
using Aspose.Pdf;

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

        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            Page blankPage = doc.Pages.Add();

            // NOTE: Page label functionality (PageLabel, PageLabelStyle, PageLabelCollection.Add)
            // is not available in the version of Aspose.Pdf referenced by this project.
            // To assign a custom page label such as a lower‑case Roman numeral "i",
            // upgrade to a newer Aspose.Pdf package that includes the PageLabel API,
            // or implement a visual workaround (e.g., add a visible text fragment).

            // Optional visual label example (adds the character "i" to the page content):
            // TextFragment tf = new TextFragment("i");
            // tf.TextState.FontSize = 12;
            // tf.TextState.FontStyle = FontStyles.Bold;
            // tf.Position = new Position(50, blankPage.PageInfo.Height - 50);
            // blankPage.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added. Saved to '{outputPath}'.");
    }
}
