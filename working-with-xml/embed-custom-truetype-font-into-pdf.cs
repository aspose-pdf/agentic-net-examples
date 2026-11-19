using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";
        const string outputPdf = "output.pdf";
        const string fontPath  = "MyCustomFont.ttf";

        // Verify required files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"TrueType font not found: {fontPath}");
            return;
        }

        // Register the custom TrueType font using a substitution (AddFont was removed in recent versions)
        // The substitution maps the font name (without extension) to the actual TTF file.
        string fontName = Path.GetFileNameWithoutExtension(fontPath);
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontName, fontPath));

        // Load the XML content and convert it to a PDF document
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Retrieve the Font object that was registered above
            Font customFont = FontRepository.FindFont(fontName);
            if (customFont == null)
            {
                Console.Error.WriteLine($"Failed to locate registered font: {fontName}");
                return;
            }

            // Apply the custom font to every TextFragment in the document
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is TextFragment tf)
                    {
                        tf.TextState.Font = customFont;
                    }
                }
            }

            // Save the PDF; the used font will be embedded automatically
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with embedded font: {outputPdf}");
    }
}
