using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "nested_list.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // ---------- Create the outer bullet list using TextFragment ----------
            // In the current Aspose.Pdf API there is no dedicated List class. A list can be
            // represented by plain text fragments where the bullet/number marker is part of the string.
            // Indentation is achieved by adding spaces before the marker.

            // First bullet item.
            page.Paragraphs.Add(new TextFragment("\u2022 Bullet Item 1")); // \u2022 = bullet character

            // Second bullet item – this one will contain a nested numbered list.
            page.Paragraphs.Add(new TextFragment("\u2022 Bullet Item 2 with sublist"));

            // Nested numbered list (indented with three spaces).
            page.Paragraphs.Add(new TextFragment("   1. Numbered Subitem 1"));
            page.Paragraphs.Add(new TextFragment("   2. Numbered Subitem 2"));

            // Third bullet item.
            page.Paragraphs.Add(new TextFragment("\u2022 Bullet Item 3"));

            // Save the document while the using block is still active.
            doc.Save(outputPath);
        }

        // ---------- Validation ----------
        // Re‑open the saved PDF to verify list structure.
        using (Document doc = new Document(outputPath))
        {
            // The list is represented by a series of TextFragment objects on the first page.
            var fragments = doc.Pages[1].Paragraphs;

            // Expected total number of fragments (outer bullets + inner numbered items).
            const int expectedFragmentCount = 5;
            if (fragments.Count != expectedFragmentCount)
            {
                Console.WriteLine($"Validation failed: Expected {expectedFragmentCount} fragments, found {fragments.Count}.");
                return;
            }

            // Helper to extract the raw text of a fragment safely.
            string GetText(BaseParagraph p) => p is TextFragment tf ? tf.Text : string.Empty;

            // Validate outer bullet items (they start with the bullet character).
            Console.WriteLine("Validating outer bullet items:");
            for (int i = 0; i < fragments.Count; i++)
            {
                string txt = GetText(fragments[i]);
                if (i == 0 || i == 4) // first and last are outer bullets
                {
                    if (!txt.StartsWith("\u2022"))
                    {
                        Console.WriteLine($"Validation failed: Fragment {i + 1} does not start with a bullet.");
                        return;
                    }
                    Console.WriteLine($"  Bullet {i + 1}: {txt}");
                }
                else if (i == 1) // second bullet (contains sublist)
                {
                    if (!txt.StartsWith("\u2022"))
                    {
                        Console.WriteLine($"Validation failed: Fragment {i + 1} (second bullet) does not start with a bullet.");
                        return;
                    }
                    Console.WriteLine($"  Bullet with sublist: {txt}");
                }
                else // inner numbered items (should start with a digit and a dot)
                {
                    if (!char.IsDigit(txt.TrimStart()[0]))
                    {
                        Console.WriteLine($"Validation failed: Fragment {i + 1} expected to be a numbered subitem.");
                        return;
                    }
                    Console.WriteLine($"  Numbered subitem: {txt.Trim()}");
                }
            }
        }

        Console.WriteLine($"PDF with nested list created at '{outputPath}'.");
    }
}
