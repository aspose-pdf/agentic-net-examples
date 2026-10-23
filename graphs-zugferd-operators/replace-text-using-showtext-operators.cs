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

        const string searchText = "Old String";
        const string replaceText = "New String";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Locate the text fragments to replace
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);

                // In recent Aspose.Pdf versions TextSearchOptions requires a constructor argument.
                // The boolean argument enables searching in annotations; adjust as needed.
                TextSearchOptions searchOpts = new TextSearchOptions(true);
                absorber.TextSearchOptions = searchOpts;

                // Apply the absorber to all pages
                doc.Pages.Accept(absorber);

                // Replace each found fragment with the new string
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceText;
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
