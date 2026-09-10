using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a minimal PDF in memory that contains the text we
        //    will replace. This makes the example self‑contained and
        //    eliminates the need for an external "input.pdf" file.
        // ------------------------------------------------------------
        Document doc = new Document();
        Page page = doc.Pages.Add();
        page.Paragraphs.Add(new TextFragment("old text"));

        // ------------------------------------------------------------
        // 2. Replace all occurrences of "old text" with "new text".
        // ------------------------------------------------------------
        TextFragmentAbsorber absorber = new TextFragmentAbsorber("old text");
        doc.Pages.Accept(absorber);
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            fragment.Text = "new text";
        }

        // ------------------------------------------------------------
        // 3. Rotate the first page 90 degrees clockwise using the
        //    Rotation enum (core Aspose.Pdf API).
        // ------------------------------------------------------------
        if (doc.Pages.Count >= 1)
        {
            doc.Pages[1].Rotate = Rotation.on90;
        }

        // ------------------------------------------------------------
        // 4. Save the modified PDF into a MemoryStream for downstream
        //    consumption. Reset the stream position so callers can read
        //    from the beginning.
        // ------------------------------------------------------------
        using (MemoryStream outputStream = new MemoryStream())
        {
            doc.Save(outputStream);
            outputStream.Position = 0; // rewind
            Console.WriteLine($"PDF saved to memory stream (size: {outputStream.Length} bytes).");
        }
    }
}
