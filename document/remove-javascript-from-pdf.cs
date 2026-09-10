using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ----- Remove document‑level JavaScript -----
            doc.OpenAction = null;

            // ----- Remove page‑level JavaScript -----
            foreach (Page page in doc.Pages)
            {
                page.Actions.OnOpen = null;
                page.Actions.OnClose = null;
            }

            // ----- Remove JavaScript from link annotations -----
            foreach (Page page in doc.Pages)
            {
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    if (page.Annotations[i] is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        link.Action = null;
                    }
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript removed. Saved to '{outputPath}'.");
    }
}
