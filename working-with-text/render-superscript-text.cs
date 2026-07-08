using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SuperscriptExample
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // ----- Normal baseline text -----
            TextFragment normal = new TextFragment("E = mc");
            normal.TextState.FontSize = 12;               // regular size
            normal.TextState.Font = FontRepository.FindFont("Helvetica");
            page.Paragraphs.Add(normal);

            // ----- Superscript text ("2") -----
            TextFragment superscript = new TextFragment("2");
            superscript.TextState.FontSize = 8;            // smaller than baseline
            superscript.TextState.Font = FontRepository.FindFont("Helvetica");
            // Raise the text relative to the baseline by enabling superscript
            superscript.TextState.Superscript = true;
            // Alternatively, you could set a manual rise using the SetTextRise operator:
            // page.Operators.Add(new SetTextRise(4)); // positive rise value
            // page.Paragraphs.Add(superscript);
            page.Paragraphs.Add(superscript);

            // Save the PDF to disk
            doc.Save("superscript_output.pdf");
        }

        Console.WriteLine("PDF with superscript text created successfully.");
    }
}