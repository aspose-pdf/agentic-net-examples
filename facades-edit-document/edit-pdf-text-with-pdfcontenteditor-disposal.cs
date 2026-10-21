using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfContentEditor editor = null;
        try
        {
            // Create the PdfContentEditor instance (will be disposed in finally)
            editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Load the PDF document using the high‑level Document API
            Document doc = new Document(inputPath);

            // Create a text stamp to demonstrate editing
            TextStamp textStamp = new TextStamp("Confidential")
            {
                Background          = false,
                Opacity             = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                TextState = {
                    FontSize   = 24,
                    FontStyle  = FontStyles.Bold,
                    Font       = FontRepository.FindFont("Arial")
                }
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Ensure the editor is disposed to free native resources
            if (editor != null)
                editor.Dispose();
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
