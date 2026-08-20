using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfFileMend facade on the loaded document
            PdfFileMend mend = new PdfFileMend(doc);

            // Enable word wrapping and set the algorithm to wrap by whole words
            mend.IsWordWrap = true;
            mend.WrapMode = WordWrapMode.ByWords; // Aspose.Pdf.Facades.WordWrapMode

            // Long text that exceeds the desired width
            string longText = "The quick brown fox jumps over the lazy dog. " +
                              "This sentence is deliberately long to exceed the defined width and test word wrapping by words.";

            // Create FormattedText (color uses System.Drawing.Color)
            FormattedText formatted = new FormattedText(
                longText,
                System.Drawing.Color.Black,
                "Helvetica",
                EncodingType.Winansi,
                false,
                12);

            // Add the text to page 1 at position (100, 700)
            // Word wrapping will be applied according to the settings above
            mend.AddText(formatted, 1, 100, 700);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Word‑wrapped PDF saved to '{outputPdf}'.");
    }
}