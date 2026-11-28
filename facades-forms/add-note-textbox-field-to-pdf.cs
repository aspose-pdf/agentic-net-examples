using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "NoteAdded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Get the first page (pages are 1‑based in Aspose.Pdf)
        Page page = doc.Pages[1];

        // Define the rectangle where the note field will be placed
        var rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

        // Create the text box (multiline) field
        TextBoxField noteField = new TextBoxField(page, rect)
        {
            PartialName = "NoteField",
            Value = "Sample note text",
            Multiline = true
        };

        // ----- Decoration (visual appearance) -----
        // Background and border colours are set via the Characteristics property
        noteField.Characteristics.Background = System.Drawing.Color.LightYellow;
        noteField.Characteristics.Border = System.Drawing.Color.Black;

        // Border style/width is set via a Border object that requires the field as its parent
        noteField.Border = new Border(noteField) { Width = 1, Style = BorderStyle.Solid };

        // Font, size and text colour are set via DefaultAppearance (expects System.Drawing.Color)
        noteField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
        // -------------------------------------------

        // Add the field to the document's form collection on page 1
        doc.Form.Add(noteField, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Note field added and saved to '{outputPath}'.");
    }
}
