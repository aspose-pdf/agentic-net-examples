using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string archivePath = "pdf-archive";
        if (!Directory.Exists(archivePath))
        {
            Console.Error.WriteLine($"Archive folder not found: {archivePath}");
            return;
        }

        // Change working directory so that Save can use a simple filename
        Directory.SetCurrentDirectory(archivePath);
        string[] pdfFiles = Directory.GetFiles(".", "*.pdf");
        foreach (string pdfFile in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfFile);
            try
            {
                using (Document doc = new Document(fileName))
                {
                    // Replace all case‑sensitive occurrences of "Confidential" with "Public"
                    int replacedCount = ReplaceText(doc, "Confidential", "Public", true);
                    doc.Save(fileName);
                    Console.WriteLine($"{fileName}: replaced {replacedCount} occurrence(s).");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Replaces text in a PDF document using Aspose.Pdf's TextFragmentAbsorber.
    /// Returns the number of replacements performed.
    /// </summary>
    private static int ReplaceText(Document doc, string oldText, string newText, bool caseSensitive)
    {
        // Configure the absorber to search for the old text.
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldText);
        absorber.TextSearchOptions = new TextSearchOptions(caseSensitive);

        // Search across all pages.
        doc.Pages.Accept(absorber);

        int count = 0;
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            fragment.Text = newText;
            count++;
        }
        return count;
    }
}
