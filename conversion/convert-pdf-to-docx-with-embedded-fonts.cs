using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string docxPath = "output.docx";
        // Path to the custom TrueType/OpenType font you want to embed
        const string customFontPath = "MyCustomFont.ttf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Font not found: {customFontPath}");
            return;
        }

        try
        {
            // Register the custom font via substitution so Aspose.Pdf can use it during conversion
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("MyCustomFont", customFontPath));

            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure DOCX save options – only the format needs to be set; fonts are embedded automatically
                var saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Perform the conversion and save the DOCX with embedded fonts
                pdfDoc.Save(docxPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{docxPath}' (fonts embedded).");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
