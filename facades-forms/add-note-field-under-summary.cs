using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "NoteUnderSummary.pdf";
        const string noteText = "This is the note placed under the Summary section.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (core API)
        Document doc = new Document(inputPath);

        // Find the first occurrence of the word "Summary"
        TextFragmentAbsorber absorber = new TextFragmentAbsorber("Summary");
        doc.Pages.Accept(absorber);
        if (absorber.TextFragments == null || absorber.TextFragments.Count == 0)
        {
            Console.Error.WriteLine("The text \"Summary\" was not found in the document.");
            return;
        }

        // Use the rectangle of the first match as a reference point
        TextFragment fragment = absorber.TextFragments[0];
        Aspose.Pdf.Rectangle summaryRect = fragment.Rectangle;

        // Define the rectangle for the note field (just below the found text)
        double fieldHeight = 50; // height of the note field
        double fieldWidth = summaryRect.URX - summaryRect.LLX; // match width of the found text
        double llx = summaryRect.LLX;
        double lly = summaryRect.LLY - fieldHeight - 10; // 10 units gap below the text
        double urx = llx + fieldWidth;
        double ury = lly + fieldHeight;
        Aspose.Pdf.Rectangle noteRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // Create a multiline TextBox field (acts as a note)
        TextBoxField noteField = new TextBoxField(doc.Pages[1], noteRect)
        {
            PartialName = "NoteUnderSummary",
            Value = noteText,
            Multiline = true
        };

        // Set border and background colour
        noteField.Border = new Border(noteField) { Width = 1 };
        noteField.Color = Color.LightGray;

        // Add the field to the document (page number is 1‑based)
        doc.Form.Add(noteField, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Note field added and saved to '{outputPath}'.");
    }
}
