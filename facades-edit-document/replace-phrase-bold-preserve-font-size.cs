using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facades API for PDF manipulation
using Aspose.Pdf.Text;            // TextAbsorber and TextFragment types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldPhrase  = "old phrase";
        const string newPhrase  = "new bold phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ---------- Replace the phrase using TextFragmentAbsorber (avoids signature mismatch) ----------
            // Find all occurrences of the old phrase
            TextFragmentAbsorber oldAbsorber = new TextFragmentAbsorber(oldPhrase);
            doc.Pages.Accept(oldAbsorber);

            // Replace each occurrence with the new phrase and apply bold style while preserving font size
            foreach (TextFragment fragment in oldAbsorber.TextFragments)
            {
                // FontSize property expects a float, so cast the double value explicitly
                float currentSize = (float)fragment.TextState.FontSize; // keep existing size
                fragment.Text = newPhrase;                               // replace text
                fragment.TextState.FontSize = currentSize;               // assign back as float
                fragment.TextState.FontStyle = FontStyles.Bold;          // apply bold style
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
