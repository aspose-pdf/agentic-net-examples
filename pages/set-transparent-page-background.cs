using System;
using System.IO;
using Aspose.Pdf;

public class SetTransparentPageBackground
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Load the PDF if it exists; otherwise create a new one‑page document.
        using (Document document = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Ensure there is at least one page when we created a new document.
            if (!File.Exists(inputPath) && document.Pages.Count == 0)
            {
                document.Pages.Add();
            }

            // Set each page's background color to transparent.
            foreach (Page page in document.Pages)
            {
                page.Background = Color.Transparent;
            }

            // Save the result.
            document.Save(outputPath);
        }
    }
}
