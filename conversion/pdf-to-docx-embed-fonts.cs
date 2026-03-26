using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";
        const string customFontPath = "customfont.ttf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {customFontPath}");
            return;
        }

        // Register the custom font using the current Aspose.PDF API.
        // "CustomFont" is an arbitrary alias; the actual font file is supplied as the second argument.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("CustomFont", customFontPath));

        using (Document pdfDoc = new Document(inputPdf))
        {
            var saveOpts = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
                // Fonts present in the FontRepository are embedded automatically.
            };

            pdfDoc.Save(outputDocx, saveOpts);
        }

        Console.WriteLine($"PDF converted to DOCX with embedded fonts: {outputDocx}");
    }
}
