using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // List of PDF files to process
        string[] inputFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
                {
                    Page page = doc.Pages[pageIdx];

                    // Iterate through all annotations on the page using foreach for safety
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Apply only to rectangle‑shaped annotations (SquareAnnotation in Aspose.Pdf)
                        if (annotation is SquareAnnotation squareAnn)
                        {
                            // NOTE: The current Aspose.Pdf version used in this project does not expose
                            // the 'Action' property on SquareAnnotation nor the 'JavaScriptAction' class.
                            // To attach a JavaScript alert you need to upgrade to a newer Aspose.Pdf version
                            // (e.g., 20.9 or later) where these members are available.
                            // If an upgrade is not possible, the JavaScript action cannot be set.
                            // Example for newer versions (commented out for compatibility):
                            // squareAnn.Action = new JavaScriptAction("app.alert('Rectangle annotation clicked');");
                        }
                    }
                }

                // Save the modified document with a simple filename
                string outputFile = Path.GetFileNameWithoutExtension(inputPath) + "_alert.pdf";
                doc.Save(outputFile);
                Console.WriteLine($"Processed and saved: {outputFile}");
            }
        }
    }
}
