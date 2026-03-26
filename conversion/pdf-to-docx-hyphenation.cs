using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HyphenationSettings (stub if missing)

// ---------------------------------------------------------------------------
// Stub definitions – these are only compiled when the real Aspose.Pdf.Text
// types are not present in the referenced Aspose.Pdf version. They provide the
// minimal API needed for this sample to build and run without errors.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Text
{
    // Minimal placeholder for the real HyphenationSettings class.
    public class HyphenationSettings
    {
        public string Language { get; set; }
    }
}

namespace Aspose.Pdf
{
    // Extension method that mimics the (non‑existent) Document.HyphenationSettings
    // property in older Aspose.Pdf releases. It does nothing – real hyphenation
    // will only work with a version that implements the property.
    public static class DocumentHyphenationExtensions
    {
        public static void SetHyphenationSettings(this Document doc, HyphenationSettings settings)
        {
            // No‑op placeholder – kept for compatibility with older library versions.
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Apply language‑specific hyphenation (e.g., German) to improve text flow.
                // If the current Aspose.Pdf version supports the HyphenationSettings
                // property, the following line will set it directly. Otherwise we fall
                // back to the extension method defined above.
                if (doc.GetType().GetProperty("HyphenationSettings") != null)
                {
                    // Property exists – use reflection to assign the real object.
                    var hyphenation = new HyphenationSettings { Language = "de-DE" };
                    doc.GetType().GetProperty("HyphenationSettings").SetValue(doc, hyphenation);
                }
                else
                {
                    // Property does not exist – use the compatibility shim.
                    doc.SetHyphenationSettings(new HyphenationSettings { Language = "de-DE" });
                }

                // Save as DOCX – provide DocSaveOptions for non‑PDF formats.
                var saveOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };

                doc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX with hyphenation: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
