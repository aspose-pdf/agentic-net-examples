using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string outputPath  = "output.pdf";
        const string searchText  = "OldString";
        const string replaceText = "NewString";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Locate text‑show operators that contain the target string
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
                doc.Pages.Accept(absorber);

                // Replace each found fragment with the new string
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replaceText;
                }

                // Save the modified PDF (PDF format is the default)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text replacement completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}