using System;
using System.IO;
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

        using (Document doc = new Document(inputPath))
        {
            // Remove document‑level JavaScript (OpenAction)
            doc.OpenAction = null;

            // Remove page‑level JavaScript actions (OnOpen / OnClose)
            foreach (Page page in doc.Pages)
            {
                page.Actions.OnOpen = null;
                page.Actions.OnClose = null;
            }

            // Optional: remove JavaScript attached to annotations (e.g., LinkAnnotation)
            foreach (Page page in doc.Pages)
            {
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    if (page.Annotations[i] is LinkAnnotation link && link.Action is JavascriptAction)
                    {
                        page.Annotations.Delete(i);
                    }
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript actions removed. Saved to '{outputPath}'.");
    }
}
