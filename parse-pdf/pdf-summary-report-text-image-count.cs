using System;
using System.IO;
using System.Collections;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // If no arguments are supplied, inform the user.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: Program <pdfFile1> [<pdfFile2> ...]");
            return;
        }

        foreach (string pdfPath in args)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            try
            {
                // Open the PDF document inside a using block for deterministic disposal.
                using (Document doc = new Document(pdfPath))
                {
                    // ---------- Text extraction ----------
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    string extractedText = absorber.Text ?? string.Empty;
                    int totalTextLength = extractedText.Length;

                    // ---------- Image counting ----------
                    int imageCount = 0;
                    // Pages are 1‑based in Aspose.Pdf.
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];
                        foreach (XImage img in page.Resources.Images)
                        {
                            imageCount++;
                        }
                    }

                    // ---------- Graphics (vector objects) counting ----------
                    // The Resources.XObjects collection may not exist in older versions of Aspose.Pdf.
                    // To keep the code compatible we use reflection to discover the property at runtime.
                    int graphicsCount = 0;
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];
                        var resources = page.Resources;
                        var xObjectsProp = resources.GetType().GetProperty("XObjects");
                        if (xObjectsProp != null)
                        {
                            var xObjects = xObjectsProp.GetValue(resources) as IEnumerable;
                            if (xObjects != null)
                            {
                                foreach (var _ in xObjects)
                                {
                                    graphicsCount++;
                                }
                            }
                        }
                    }

                    // ---------- Report ----------
                    Console.WriteLine($"PDF: {Path.GetFileName(pdfPath)}");
                    Console.WriteLine($"  Extracted graphics (XObjects) : {graphicsCount}");
                    Console.WriteLine($"  Total text length (characters): {totalTextLength}");
                    Console.WriteLine($"  Image count                    : {imageCount}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
