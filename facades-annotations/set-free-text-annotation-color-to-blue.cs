using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        var doc = new Document(inputPath);

        // Locate the first Free Text annotation in the document.
        int targetPage = -1;
        int targetIndex = -1;
        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
        {
            var annotations = doc.Pages[pageNum].Annotations;
            for (int idx = 0; idx < annotations.Count; idx++)
            {
                if (annotations[idx] is FreeTextAnnotation)
                {
                    targetPage = pageNum;
                    targetIndex = idx;
                    break;
                }
            }
            if (targetIndex != -1) break;
        }

        if (targetPage == -1)
        {
            Console.WriteLine("No Free Text annotation found.");
            return;
        }

        // Get the annotation we need to modify.
        var freeText = (FreeTextAnnotation)doc.Pages[targetPage].Annotations[targetIndex];

        // ---------------------------------------------------------------------
        // Change the text colour to blue while preserving the original font name
        // and font size.  The DefaultAppearance property of a FreeTextAnnotation is
        // a PDF DA string, e.g. "/Helv 12 Tf 0 0 0 rg".  We extract the font name
        // and size, then rebuild the string with the RGB values for blue (0 0 1).
        // ---------------------------------------------------------------------
        string oldDa = freeText.DefaultAppearance ?? string.Empty;
        // Simple regex to capture font name and size.
        var match = Regex.Match(oldDa, @"(?<font>/[^\s]+)\s+(?<size>\d+(?:\.\d+)?)\s+Tf", RegexOptions.IgnoreCase);
        string fontName = match.Success ? match.Groups["font"].Value : "/Helv"; // fallback to Helvetica
        string fontSize = match.Success ? match.Groups["size"].Value : "12";   // fallback size
        // Build new DA string – blue colour is "0 0 1 rg".
        string newDa = $"{fontName} {fontSize} Tf 0 0 1 rg";
        freeText.DefaultAppearance = newDa;

        // Save the modified document.
        doc.Save(outputPath);
        Console.WriteLine($"Free Text annotation colour changed to blue and saved to '{outputPath}'.");
    }
}
