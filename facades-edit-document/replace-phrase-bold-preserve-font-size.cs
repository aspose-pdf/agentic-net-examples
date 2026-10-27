using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldPhrase  = "old phrase";
        const string newPhrase  = "new phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate all occurrences of the phrase to be replaced
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldPhrase);
            doc.Pages.Accept(absorber);

            // Iterate over each found fragment, replace text and apply bold style
            foreach (TextFragment tf in absorber.TextFragments)
            {
                tf.Text = newPhrase;                                 // replace the phrase
                tf.TextState.FontStyle = FontStyles.Bold;            // apply bold while keeping other style unchanged
                // Font size is left as is; no assignment needed
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}