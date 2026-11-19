using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string searchText = "oldText";
        const string replaceText = "newText";

        // Ensure the input PDF exists – create a minimal one‑page PDF if it does not.
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                // Add a paragraph containing the text we will later replace.
                placeholder.Pages[1].Paragraphs.Add(new TextFragment(searchText));
                placeholder.Save(inputPath);
            }
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ---------- Text Replacement ----------
            // Find all occurrences of the search text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);

            // Replace each found fragment with the new text
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replaceText;
            }

            // ---------- Page Rotation ----------
            // Rotate the first page 90 degrees clockwise using the core API
            Page firstPage = doc.Pages[1]; // Aspose.Pdf uses 1‑based page indexing
            firstPage.Rotate = Rotation.on90;

            // ---------- Save to MemoryStream ----------
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the modified document into the memory stream
                doc.Save(ms);

                // The stream now contains the PDF bytes. Example: write them to a file.
                File.WriteAllBytes("modified_output.pdf", ms.ToArray());

                // (Optional) Show the size of the generated PDF
                Console.WriteLine($"Modified PDF size: {ms.Length} bytes");
            }
        }

        Console.WriteLine("PDF modified, rotated, and saved to memory stream successfully.");
    }
}
