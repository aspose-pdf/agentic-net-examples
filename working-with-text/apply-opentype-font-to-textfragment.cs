using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string fontPath   = "customfont.otf";   // path to the OTF font file
        const string outputPath = "styled_text.pdf"; // result PDF file

        // Verify that the font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Register the OTF font with Aspose.Pdf using a substitution entry
        string fontFamily = Path.GetFileNameWithoutExtension(fontPath);
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontFamily, fontPath));

        // Retrieve the registered font
        Font customFont = FontRepository.FindFont(fontFamily);

        // If the font cannot be located, the default font will be used
        if (customFont == null)
        {
            Console.Error.WriteLine($"Font \"{fontFamily}\" not found in repository. Using default font.");
        }

        // Create the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf with OTF font!");

            // Apply the custom font and additional styling if the font was loaded successfully
            if (customFont != null)
            {
                fragment.TextState.Font = customFont;                     // set the OTF font
                fragment.TextState.FontSize = 24;                         // larger font size
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0, 0.5, 0.8); // optional color
            }

            // Add the styled TextFragment to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to \"{outputPath}\".");
    }
}
