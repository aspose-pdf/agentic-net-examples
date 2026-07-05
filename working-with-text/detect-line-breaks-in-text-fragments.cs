using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_processed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Absorb all text fragments on each page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                doc.Pages[pageNum].Accept(absorber);
            }

            // Iterate through each captured TextFragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The Text property contains the whole fragment text, including line‑break characters.
                // Split it into logical lines for custom processing.
                string[] lines = fragment.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                for (int i = 0; i < lines.Length; i++)
                {
                    string lineText = lines[i];
                    // A line is considered to end with a manual line break if there is another line after it.
                    bool hasLineBreak = i < lines.Length - 1;
                    Console.WriteLine($"Fragment Hash={fragment.GetHashCode()}, Line {i + 1}, HasLineBreak={hasLineBreak}");

                    // Custom layout logic could be placed here.
                    // For demonstration, we change the colour of the whole fragment when any of its lines ends with a line break.
                    if (hasLineBreak)
                    {
                        fragment.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // red
                    }
                }
            }

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}
