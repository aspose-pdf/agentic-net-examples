using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the annotation editor facade to the document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                // Create a dummy page and rectangle required for TextAnnotation constructor
                Aspose.Pdf.Page dummyPage = doc.Pages[1];
                Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                // Prepare the annotation template with the properties to be applied
                TextAnnotation template = new TextAnnotation(dummyPage, dummyRect)
                {
                    Modified = DateTime.Now,
                    Title = "Updated Author",
                    Contents = "Updated contents via retry policy",
                    Color = Aspose.Pdf.Color.Red,
                    Subject = "Updated Subject",
                    Open = true
                };

                // Retry policy configuration
                const int maxAttempts = 5;
                int attempt = 0;
                int delayMs = 500; // initial backoff delay

                while (true)
                {
                    try
                    {
                        // Apply modifications to all pages
                        editor.ModifyAnnotations(1, doc.Pages.Count, template);

                        // Save the modified PDF
                        editor.Save(outputPath);
                        Console.WriteLine($"Annotations modified and saved to '{outputPath}'.");
                        break; // success
                    }
                    catch (IOException ex) when (IsTransient(ex))
                    {
                        attempt++;
                        if (attempt >= maxAttempts)
                        {
                            Console.Error.WriteLine($"Transient error persisted after {attempt} attempts: {ex.Message}");
                            throw;
                        }

                        Console.WriteLine($"Transient I/O error (attempt {attempt}). Retrying in {delayMs} ms...");
                        Thread.Sleep(delayMs);
                        delayMs *= 2; // exponential backoff
                    }
                }
            }
        }
    }

    // Determines whether an IOException is considered transient.
    // In a real scenario, inspect HResult or specific messages.
    static bool IsTransient(IOException ex) => true;
}