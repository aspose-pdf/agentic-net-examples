using System;
using System.IO;
using System.Text;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // StringBuilder to accumulate extracted superscript/subscript text
        StringBuilder sb = new StringBuilder();

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Use TextFragmentAbsorber to get access to TextFragments collection
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                // Use Pure formatting mode so that superscript/subscript flags are preserved
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                // Accept the absorber on the current page
                page.Accept(absorber);

                // Iterate all text fragments on the page
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    if (IsSuperscriptOrSubscript(fragment))
                    {
                        sb.AppendLine(fragment.Text);
                    }
                }
            }
        }

        // Write the collected text to a single .txt file
        File.WriteAllText(outputTxtPath, sb.ToString(), Encoding.UTF8);
        Console.WriteLine($"Extraction complete. Output saved to '{outputTxtPath}'.");
    }

    /// <summary>
    /// Determines whether the given TextFragment is marked as superscript or subscript.
    /// The Aspose.Pdf.TextFragmentState class does not expose IsSuperscript/IsSubscript
    /// in older versions, so we use reflection to query those members if they exist.
    /// </summary>
    private static bool IsSuperscriptOrSubscript(TextFragment fragment)
    {
        if (fragment == null) return false;
        var state = fragment.TextState; // TextFragmentState instance
        if (state == null) return false;

        // Try to read IsSuperscript property via reflection
        bool isSup = false;
        bool isSub = false;
        PropertyInfo propSup = state.GetType().GetProperty("IsSuperscript", BindingFlags.Public | BindingFlags.Instance);
        PropertyInfo propSub = state.GetType().GetProperty("IsSubscript", BindingFlags.Public | BindingFlags.Instance);
        if (propSup != null && propSup.PropertyType == typeof(bool))
        {
            isSup = (bool)propSup.GetValue(state);
        }
        if (propSub != null && propSub.PropertyType == typeof(bool))
        {
            isSub = (bool)propSub.GetValue(state);
        }
        return isSup || isSub;
    }
}
