using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document sourceDoc = new Document(inputPath))
        {
            using (Document resultDoc = new Document())
            {
                // Remove the default blank page created by the constructor
                if (resultDoc.Pages.Count > 0)
                {
                    resultDoc.Pages.Delete(1);
                }

                for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                {
                    Page sourcePage = sourceDoc.Pages[i];

                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    sourcePage.Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;
                    int wordCount = 0;
                    if (pageText.Length > 0)
                    {
                        string[] words = pageText.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        wordCount = words.Length;
                    }

                    // Determine zoom factor based on word count
                    float zoomFactor = 1.0f;
                    if (wordCount < 100)
                    {
                        zoomFactor = 1.5f;
                    }
                    else if (wordCount < 300)
                    {
                        zoomFactor = 1.2f;
                    }
                    else
                    {
                        zoomFactor = 1.0f;
                    }

                    // Create a temporary single‑page PDF
                    string tempSinglePath = Path.GetTempFileName();
                    using (Document singleDoc = new Document())
                    {
                        singleDoc.Pages.Add(sourcePage);
                        singleDoc.Save(tempSinglePath);
                    }

                    // Apply zoom using PdfPageEditor
                    string tempZoomedPath = Path.GetTempFileName();
                    PdfPageEditor editor = new PdfPageEditor();
                    editor.BindPdf(tempSinglePath);
                    editor.Zoom = zoomFactor;
                    editor.Save(tempZoomedPath);
                    editor.Close();

                    // Load the zoomed page and add it to the result document
                    using (Document zoomedDoc = new Document(tempZoomedPath))
                    {
                        resultDoc.Pages.Add(zoomedDoc.Pages[1]);
                    }

                    // Clean up temporary files
                    File.Delete(tempSinglePath);
                    File.Delete(tempZoomedPath);
                }

                resultDoc.Save(outputPath);
            }
        }

        Console.WriteLine("Zoom‑adjusted PDF saved to '" + outputPath + "'.");
    }
}
